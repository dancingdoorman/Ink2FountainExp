using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using FountainExponential.LanguageStructures;
using FountainExponential.LanguageStructures.Lexical;
using FountainExponential.LanguageStructures.Lexical.AutomaticFlow;
using FountainExponential.LanguageStructures.Lexical.InteractiveFlow;
using FountainExponential.LanguageStructures.Lexical.MetaData;
using FountainExponential.LanguageStructures.Lexical.Sections;
using FountainExponential.LanguageStructures.Syntactical;
using FountainExponential.LanguageStructures.Syntactical.AutomaticFlow;
using FountainExponential.LanguageStructures.Syntactical.Code;
using FountainExponential.LanguageStructures.Syntactical.Data;
using FountainExponential.LanguageStructures.Syntactical.FountainElement;
using FountainExponential.LanguageStructures.Syntactical.InteractiveFlow;
using FountainExponential.LanguageStructures.Syntactical.MetaData;
using FountainExponential.LanguageStructures.Syntactical.Sections;

namespace Ink.Ink2FountainExp.Adapting
{
    public class FountainExponentialAdapter : IFountainExponentialAdaptable
    {

        #region Properties

        /// <summary>Gets or sets the file system interactor.</summary>
        /// <value>The file system interactor.</value>
        public FountainRenderer FountainRenderer { get; set; } = new FountainRenderer();

        public bool MoveLabeledGatherToSection { get; set; } = false;
        public bool MoveLabeledChoiceToSection { get; set; } = false;

        #endregion Properties

        public string ConvertToFountainExponential(Ink.Parsed.Fiction parsedFiction, string inputFileName)
        {
            // The optional Title Page is always the first thing in a Fountain document. 
            // Information is encoding in the format key: value. 
            // Keys can have spaces (e. g. Draft date), but must end with a colon.
            // Excample:
            // Title:
            //    _** BRICK &STEEL * *_
            //    _** FULL RETIRED** _
            //Credit: Written by
            //Author: Stu Maschwitz
            //Source: Story by KTM
            //Draft date: 1 / 20 / 2012
            //Contact:
            //    Next Level Productions
            //    1588 Mission Dr.
            //    Solvang, CA 93463

            var fountainPlay = CreateFountainPlay(parsedFiction, inputFileName);

            var builder = new StringBuilder();
            FountainRenderer.Write(builder, fountainPlay);
            var fountainContent = builder.ToString();
            return fountainContent;
        }

        public FountainPlay CreateFountainPlay(Ink.Parsed.Fiction parsedFiction, string inputFileName)
        {
            var fountainPlay = new FountainPlay();
            var mainFile = fountainPlay.MainFile;

            AddMetaData(inputFileName, mainFile);

            AddGlobalFunctions(mainFile, parsedFiction);


            var actI = new Act() { SectionName = "ActI", ActStartToken = new ActToken(), SpaceToken = new SpaceToken(), EndLine = new EndLine() };
            mainFile.Acts.Add(actI);
            actI.SyntacticalElements.Add(new BlankLine());

            var sequence1 = new Sequence() { SectionName = "Sequence1", SequenceStartToken = new SequenceToken(), SpaceToken = new SpaceToken(), EndLine = new EndLine() };
            sequence1.SyntacticalElements.Add(new BlankLine());
            actI.Sequences.Add(sequence1);


            //bool choiceStarted = false;
            AddScenes(sequence1, parsedFiction);

            return fountainPlay;
        }

        public void AddMetaData(string inputFileName, FountainFile mainFile)
        {
            mainFile.TitlePage.TitlePageBreakToken = new TitlePageBreakToken();
            var title = new Title() { Key = new KeyValuePairKeyToken() { Keyword = "Title" }, AssignmentToken = new KeyValuePairAssignmentToken(), EndLine = new EndLine() };
            title.ValueLineList.Add(new ValueLine() { Value = Path.GetFileNameWithoutExtension(inputFileName), IndentToken = new KeyValuePairIndentToken(), EndLine = new EndLine() });
            mainFile.TitlePage.KeyInformationList.Add(title);


            mainFile.TitlePage.TitlePageBreakToken = new TitlePageBreakToken();
        }

        public void AddScenes(Sequence sequence1, Parsed.Fiction parsedFiction)
        {
            foreach (var parsedObject in parsedFiction.content)
            {
                if (parsedObject.typeName == "Knot")
                {
                    var flowBase = parsedObject as Ink.Parsed.FlowBase;
                    if (flowBase != null)
                    {
                        var scene = new Scene() { SectionName = flowBase.name, SceneStartToken = new SceneToken(), SpaceToken = new SpaceToken(), EndLine = new EndLine() };
                        scene.SyntacticalElements.Add(new BlankLine());
                        sequence1.Scenes.Add(scene);

                        AddSceneElements(scene, flowBase);
                    }
                }
            }
        }

        public void AddSceneElements(Scene scene, Parsed.FlowBase flowBase)
        {
            for (int i = 0; i < flowBase.content.Count; i++)
            {
                Ink.Parsed.Object flowBaseContent = flowBase.content[i];

                var flowBaseWeave = flowBaseContent as Ink.Parsed.Weave;
                ProcesWeave(new ContentArea() { Scene = scene, IndentLevel = 0 }, flowBaseWeave);

                var flowBaseStitch = flowBaseContent as Ink.Parsed.Stitch;
                AddMoments(scene, flowBaseStitch);
            }
        }

        public void AddMoments(Scene scene, Parsed.Stitch flowBaseStitch)
        {
            if (flowBaseStitch == null)
                return;

            var moment = new Moment() { SectionName = flowBaseStitch.name, MomentStartToken = new MomentToken(), SpaceToken = new SpaceToken(), EndLine = new EndLine() };
            scene.Moments.Add(moment);

            moment.SyntacticalElements.Add(new BlankLine());

            foreach (var stitchContent in flowBaseStitch.content)
            {
                var flowBaseWeave = stitchContent as Ink.Parsed.Weave;
                ProcesWeave(new ContentArea() { Moment = moment, IndentLevel = 0 }, flowBaseWeave);
            }
        }

        private static void ProcesWeave(Moment moment, Parsed.Stitch flowBaseStitch)
        {
            if (flowBaseStitch == null)
                return;
        }
        private ContentArea GetCurrentContentArea(ContentArea contentArea, Stack<ContentArea> contentAreaStack)
        {
            if (contentAreaStack != null && contentAreaStack.Count > 0)
            {
                return contentAreaStack.Peek();
            }
            return contentArea;
        }
        private ContentArea GetBelowCurrentContentArea(ContentArea contentArea, Stack<ContentArea> contentAreaStack)
        {
            if (contentAreaStack == null)
                return contentArea;

            if (contentAreaStack.Count == 1)
                return contentAreaStack.Peek();

            if (contentAreaStack.Count > 1)
            {
                var top = contentAreaStack.Pop();
                var second = contentAreaStack.Peek();
                contentAreaStack.Push(top);

                return second;
            }

            return contentArea;
        }
        private ContentArea PopCurrentContentArea(ContentArea contentArea, Stack<ContentArea> contentAreaStack)
        {
            if (contentAreaStack != null && contentAreaStack.Count > 0)
            {
                return contentAreaStack.Pop();
            }
            return contentArea;
        }

        private ContentArea PushGatherContentArea(ContentArea contentArea, Stack<ContentArea> contentAreaStack, string labelName)
        {
            // We only push a new content area when the name is set of the Gather.
            if (!MoveLabeledGatherToSection ||  string.IsNullOrEmpty(labelName))
                return null;

            // if the weave has a name, it is labeled and we want to make it a seperate section.
            var currentContentArea = GetCurrentContentArea(contentArea, contentAreaStack);
            var newContentArea = currentContentArea.CreateSubContentArea(labelName);
            newContentArea.IndentLevel = 0;
            contentAreaStack.Push(newContentArea);


            var detour = new SeparatedDetour()
            {
                FlowTargetToken = new FlowTargetToken()
                {
                    Label = newContentArea.GetSectionName()
                },
                IndentLevel = new IndentLevel(),
                SpaceToken = new SpaceToken(),
                SeparatedDetourToken = new SeparatedDetourToken(),
                EndLine = new EndLine()
            };

            currentContentArea.AddSyntacticalElement(detour);

            return newContentArea;
        }

        private ContentArea ResetGatherContentArea(ContentArea contentArea, Stack<ContentArea> contentAreaStack)
        {
            contentAreaStack.Clear();
            var newContentArea = new ContentArea(contentArea);
            contentAreaStack.Push(newContentArea);
            return newContentArea;
        }

        private ContentArea PushChoiceContentArea(ContentArea contentArea, Stack<ContentArea> contentAreaStack, string labelName, MenuChoice menuChoice)
        {
            if (contentAreaStack == null)
                return null;

            var currentContentArea = GetCurrentContentArea(contentArea, contentAreaStack);

            // We only push a new content area when the name is set of the Gather.
            if (!MoveLabeledChoiceToSection || string.IsNullOrEmpty(labelName))
            {
                var newContentArea = new ContentArea();
                newContentArea.MenuChoice = menuChoice;
                newContentArea.IndentLevel = 1 + currentContentArea.IndentLevel;
                contentAreaStack.Push(newContentArea);
                return newContentArea;
            }
            else
            {
                // if the weave has a name, it is labeled and we want to make it a seperate section.
                var newContentArea = currentContentArea.CreateSubContentArea(labelName);
                newContentArea.IndentLevel = 0;
                contentAreaStack.Push(newContentArea);


                var detour = new SeparatedDetour()
                {
                    FlowTargetToken = new FlowTargetToken()
                    {
                        Label = newContentArea.GetSectionName()
                    },
                    IndentLevel = new IndentLevel(),
                    SpaceToken = new SpaceToken(),
                    SeparatedDetourToken = new SeparatedDetourToken(),
                    EndLine = new EndLine()
                };

                currentContentArea.AddSyntacticalElement(detour);

                return newContentArea;
            }
        }

        public void ProcesWeave(ContentArea contentArea, Parsed.Weave flowBaseWeave)
        {
            if (contentArea == null || flowBaseWeave == null)
                return;

            Ink.Parsed.Object weaveContent = null;

            var contentAreaStack = new Stack<ContentArea>();
            ResetGatherContentArea(contentArea, contentAreaStack);
            
            var menuStack = new Stack<Menu>();
            var menuChoiceStack = new Stack<MenuChoice>();
            for (int i = 0; i < flowBaseWeave.content.Count; i++)
            {
                weaveContent = flowBaseWeave.content[i];

                var weaveText = weaveContent as Ink.Parsed.Text;
                if (weaveText != null)
                {
                    var actionDescription = new ActionDescription() { TextContent = weaveText.text, IndentLevel = new IndentLevel() };

                    var actionContentArea = GetCurrentContentArea(contentArea, contentAreaStack);
                    actionContentArea.AddSyntacticalElement(actionDescription);
                }
                var weaveDivert = weaveContent as Ink.Parsed.Divert;
                if (weaveDivert != null)
                {
                    //var target = weaveDivert.target;
                    //var targetContent = weaveDivert.targetContent;
                    var deviation = new SeparatedDeviation() 
                    { 
                        FlowTargetToken = new FlowTargetToken() 
                        {
                            Label = weaveDivert.target.dotSeparatedComponents 
                        }, 
                        IndentLevel = new IndentLevel(), 
                        SpaceToken = new SpaceToken(), 
                        SeparatedDeviationToken = new SeparatedDeviationToken(), 
                        EndLine = new EndLine() 
                    };

                    var divertContentArea = GetCurrentContentArea(contentArea, contentAreaStack);
                    divertContentArea.AddSyntacticalElement(deviation);
                }
                var weaveContentList = weaveContent as Ink.Parsed.ContentList;
                if (weaveContentList != null)
                {

                }
                var weaveGather = weaveContent as Ink.Parsed.Gather;
                if (weaveGather != null)
                {
                    // A gather will always end a menu and drop the indent to 0
                    menuChoiceStack.Clear();
                    menuStack.Clear();
                    ResetGatherContentArea(contentArea, contentAreaStack);

                    PushGatherContentArea(contentArea, contentAreaStack, weaveGather.name);
                }
                var weaveChoice = weaveContent as Ink.Parsed.Choice;
                if (weaveChoice != null)
                {

                    var baseChoiceContentArea = GetCurrentContentArea(contentArea, contentAreaStack);
                    var menuChoice = new MenuChoice()
                    {
                        MenuChoiceToken = new StickyMenuChoiceToken(),
                        SpaceToken = new SpaceToken(),
                        EndLine = new EndLine(),
                        IndentLevel = new IndentLevel()
                    };

                    ContentArea newContentArea = baseChoiceContentArea;
                    Menu menu = null;
                    if (menuStack.Count > 0)
                    {
                        menu = menuStack.Peek();
                    }
                    if (menu == null)
                    {
                        var containerBlock = new ContainerBlock()
                        {
                            StartMenuChoiceToken = new ContainerBlockToken(),
                            StartEndLine = new EndLine(),
                            CloseMenuChoiceToken = new ContainerBlockToken(),
                            CloseEndLine = new EndLine(),
                            IndentLevel = new IndentLevel() { Level = contentArea.IndentLevel }
                        };
                        baseChoiceContentArea.AddSyntacticalElement(containerBlock);
                        menuChoice.IndentLevel.Level = baseChoiceContentArea.IndentLevel;

                        menu = new Menu();
                        containerBlock.SyntacticalElements.Add(menu);
                        menuStack.Push(menu);

                        // increase the indent level of this content area.
                        newContentArea = PushChoiceContentArea(contentArea, contentAreaStack, weaveChoice.name, menuChoice);
                    }
                    else
                    {
                        PopCurrentContentArea(contentArea, contentAreaStack);
                        baseChoiceContentArea = GetCurrentContentArea(contentArea, contentAreaStack);
                        menuChoice.IndentLevel.Level = baseChoiceContentArea.IndentLevel;
                        newContentArea = PushChoiceContentArea(contentArea, contentAreaStack, weaveChoice.name, menuChoice);
                    }


                    menu.Choices.Add(menuChoice);
                    menuChoiceStack.Push(menuChoice);

                    HandleChoiceContent(weaveChoice, newContentArea, menuChoice);
                }
                var subWeaveContentList = weaveContent as Ink.Parsed.Weave;
                if (subWeaveContentList != null)
                {
                    var subWeaveContentArea = GetCurrentContentArea(contentArea, contentAreaStack);
                    ProcesWeave(subWeaveContentArea, subWeaveContentList);
                }
            }            
        }

        private static void HandleChoiceContent(Parsed.Choice weaveChoice, ContentArea contentArea, MenuChoice menuChoice)
        {
            string baseContent = string.Empty;
            // Determine the base content for the choice
            var firstChoiceContentList = weaveChoice.content[0] as Ink.Parsed.ContentList;
            if (firstChoiceContentList != null)
            {
                var firstChoiceText = firstChoiceContentList.content[0] as Ink.Parsed.Text;
                if (firstChoiceText != null)
                {
                    baseContent = firstChoiceText.text;
                }
            }

            int choiceCount = weaveChoice.content.Count;
            if (choiceCount == 1)
            {
                // Shows only choice to the user but no reply
                menuChoice.Description = baseContent;
            }
            else if (choiceCount == 2)
            {
                menuChoice.Description = baseContent;

                // This has no added text for the choice
                var secondChoiceContentList = weaveChoice.content[1] as Ink.Parsed.ContentList;
                if (secondChoiceContentList != null)
                {
                    var responseText = secondChoiceContentList.content[0] as Ink.Parsed.Text;
                    if (responseText != null)
                    {
                        contentArea.AddSyntacticalElement(new ActionDescription() { TextContent = baseContent + responseText.text, EndLine = new EndLine(), IndentLevel = new IndentLevel() { Level = 1 + contentArea.IndentLevel } });
                    }
                }
            }
            else if (choiceCount == 3)
            {
                // check the last content first because it may change the meaning of the second content.
                var thirdChoiceBinaryExpression = weaveChoice.content[2] as Ink.Parsed.BinaryExpression;
                if (thirdChoiceBinaryExpression != null)
                {
                    menuChoice.Description = baseContent;

                    var secondChoiceContentList = weaveChoice.content[1] as Ink.Parsed.ContentList;
                    if (secondChoiceContentList != null)
                    {
                        var secondChoiceText = secondChoiceContentList.content[0] as Ink.Parsed.Text;
                        if (secondChoiceContentList != null)
                        {
                            contentArea.AddSyntacticalElement(new ActionDescription() { TextContent = baseContent + secondChoiceText.text, EndLine = new EndLine(), IndentLevel = new IndentLevel() { Level = 1 + contentArea.IndentLevel } });
                        }
                    }


                    var thirdChoiceText = thirdChoiceBinaryExpression.content[0] as Ink.Parsed.Text;
                    if (thirdChoiceText != null)
                    {
                        contentArea.AddSyntacticalElement(new AttributeSpan() { });
                    }
                }
                else
                {
                    // This does have added text for the choice
                    var secondChoiceContentList = weaveChoice.content[1] as Ink.Parsed.ContentList;
                    if (secondChoiceContentList != null)
                    {
                        var secondChoiceText = secondChoiceContentList.content[0] as Ink.Parsed.Text;
                        if (secondChoiceContentList != null)
                        {
                            menuChoice.Description = baseContent + secondChoiceText.text;
                        }
                    }

                    var thirdChoiceContentList = weaveChoice.content[2] as Ink.Parsed.ContentList;
                    if (thirdChoiceContentList != null)
                    {
                        var thirdChoiceText = thirdChoiceContentList.content[0] as Ink.Parsed.Text;
                        if (thirdChoiceText != null)
                        {
                            contentArea.AddSyntacticalElement(new ActionDescription() { TextContent = baseContent + thirdChoiceText.text, EndLine = new EndLine(), IndentLevel = new IndentLevel() { Level = 1 + contentArea.IndentLevel } });
                        }
                    }
                }
            }
            else if (choiceCount == 4)
            {
                // same as 3 but with force attribute on text
                // This does have added text for the choice
                var secondChoiceContentList = weaveChoice.content[1] as Ink.Parsed.ContentList;
                var secondChoiceText = secondChoiceContentList.content[0] as Ink.Parsed.Text;
                menuChoice.Description = baseContent + secondChoiceText;

                var thirdChoiceContentList = weaveChoice.content[2] as Ink.Parsed.ContentList;
                var thirdChoiceText = thirdChoiceContentList.content[0] as Ink.Parsed.Text;
                contentArea.AddSyntacticalElement(new ActionDescription() { TextContent = baseContent + thirdChoiceText.text, EndLine = new EndLine(), IndentLevel = new IndentLevel() { Level = 1 + contentArea.IndentLevel } });

                contentArea.AddSyntacticalElement(new AttributeSpan() { });
            }
            else
            {
                // wrong?
                var y = 1;
            }
        }

        public void AddGlobalFunctions(FountainFile file, Parsed.Fiction parsedFiction)
        {
            foreach (var parsedObject in parsedFiction.content)
            {
                if (parsedObject.typeName == "Function")
                {
                    var function = parsedObject as Ink.Parsed.Knot;
                    if (function != null)
                    {
                        var definingCodeBlock = new DefiningCodeBlock();
                        definingCodeBlock.TextContent = CreateCodeContainerContent(function);
                        file.SyntacticalElements.Add(definingCodeBlock);
                    }
                }
            }
        }

        public string CreateCodeContainerContent(Parsed.Knot function)
        {
            var builder = new StringBuilder();
            builder.Append("function ");
            builder.Append(function.name);
            builder.Append("(");
            bool firstArgument = true;
            foreach (var arg in function.arguments)
            {
                if (firstArgument == false)
                    builder.Append(", ");

                builder.Append(arg.name);

                firstArgument = false;
            }
            builder.Append(")");
            builder.Append(" {\r\n");

            foreach (var functionContent in function.content)
            {
                var functionWeave = functionContent as Ink.Parsed.Weave;
                if (functionWeave != null)
                {
                    foreach (var weaveContent in functionWeave.content)
                    {
                        var variableAssignment = weaveContent as Ink.Parsed.VariableAssignment;
                        if (variableAssignment != null)
                        {
                            builder.Append("\t");
                            builder.Append(variableAssignment.variableName);
                            builder.Append(" = ");
                            builder.Append(variableAssignment.expression.ToString());

                            //foreach (var variableAssignmentContent in variableAssignment.content)
                            //{
                            //    var binaryExpression = variableAssignmentContent as Ink.Parsed.BinaryExpression;
                            //    if (binaryExpression != null)
                            //    {
                            //        builder.Append(binaryExpression.leftExpression);
                            //        builder.Append(" = ");
                            //        builder.Append(binaryExpression.rightExpression);
                            //    }
                            //}
                        }
                    }
                }
            }
            builder.Append("\r\n}");
            return builder.ToString();
        }
    }
}
