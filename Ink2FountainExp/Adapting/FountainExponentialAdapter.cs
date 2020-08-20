using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                ProcesWeave(scene.SyntacticalElements, flowBaseWeave, 0);
                
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

            foreach(var stitchContent in flowBaseStitch.content)
            {
                var flowBaseWeave = stitchContent as Ink.Parsed.Weave;
                ProcesWeave(moment.SyntacticalElements, flowBaseWeave, 0);
            }
        }

        private static void ProcesWeave(Moment moment, Parsed.Stitch flowBaseStitch)
        {
            if (flowBaseStitch == null)
                return;
        }

        public void ProcesWeave(List<ISyntacticalElementable> syntacticalElements, Parsed.Weave flowBaseWeave, int indentLevel)
        {
            if (syntacticalElements == null || flowBaseWeave == null)
                return;

            Ink.Parsed.Object previousWeaveContent = null;
            Ink.Parsed.Object weaveContent = null;
            Ink.Parsed.Object nextWeaveContent = null;
            var menuStack = new Stack<Menu>();
            var menuChoiceStack = new Stack<MenuChoice>();
            for (int i = 0; i < flowBaseWeave.content.Count; i++)
            {
                previousWeaveContent = weaveContent;
                weaveContent = flowBaseWeave.content[i];
                if (i == flowBaseWeave.content.Count - 1)
                    nextWeaveContent = null;
                else
                    nextWeaveContent = flowBaseWeave.content[1 + i];

                var weaveText = weaveContent as Ink.Parsed.Text;
                if (weaveText != null)
                {
                    var actionDescription = new ActionDescription() { TextContent = weaveText.text, IndentLevel = new IndentLevel() };

                    MenuChoice menuChoice = null;
                    if (menuChoiceStack.Count > 0)
                    {
                        menuChoice = menuChoiceStack.Peek();
                    }
                    if (menuChoice != null)
                    {
                        menuChoice.SyntacticalElements.Add(actionDescription);
                        actionDescription.IndentLevel.Level = 1 + indentLevel;
                    }
                    else
                    {
                        syntacticalElements.Add(actionDescription);
                        actionDescription.IndentLevel.Level = indentLevel;
                    }
                }
                var weaveDivert = weaveContent as Ink.Parsed.Divert;
                if (weaveDivert != null)
                {
                    //var target = weaveDivert.target;
                    //var targetContent = weaveDivert.targetContent;
                    var deviation = new SeperatedDeviation() 
                    { 
                        FlowTargetToken = new FlowTargetToken() 
                        {
                            Label = weaveDivert.target.dotSeparatedComponents 
                        }, 
                        IndentLevel = new IndentLevel(), 
                        SpaceToken = new SpaceToken(), 
                        SeperatedDeviationToken = new SeperatedDeviationToken(), 
                        EndLine = new EndLine() 
                    };
                    MenuChoice menuChoice = null;
                    if (menuChoiceStack.Count > 0)
                    {
                        menuChoice = menuChoiceStack.Peek();
                    }
                    if (menuChoice != null)
                    {
                        menuChoice.SyntacticalElements.Add(deviation);
                        deviation.IndentLevel.Level = 1 + indentLevel;
                    }
                    else
                    {
                        syntacticalElements.Add(deviation);
                        deviation.IndentLevel.Level = indentLevel;
                    }
                }
                var weaveContentList = weaveContent as Ink.Parsed.ContentList;
                if (weaveContentList != null)
                {
                }
                var weaveGather = weaveContent as Ink.Parsed.Gather;
                if (weaveGather != null)
                {
                    // A gather will always end a menu
                    //containerBlockStack.Pop();
                    menuChoiceStack.Clear();
                    menuStack.Clear();
                    syntacticalElements.Add(new BlankLine());
                }
                var weaveChoice = weaveContent as Ink.Parsed.Choice;
                if (weaveChoice != null)
                {
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
                            IndentLevel = new IndentLevel() { Level = indentLevel }
                        };
                        syntacticalElements.Add(containerBlock);

                        menu = new Menu();
                        containerBlock.SyntacticalElements.Add(menu);
                        menuStack.Push(menu);
                    }

                    var menuChoice = new MenuChoice() { MenuChoiceToken = new StickyMenuChoiceToken(), SpaceToken = new SpaceToken(), EndLine = new EndLine(), IndentLevel = new IndentLevel() { Level = indentLevel } };
                    menu.Choices.Add(menuChoice);
                    menuChoiceStack.Push(menuChoice);

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
                                menuChoice.SyntacticalElements.Add(new ActionDescription() { TextContent = baseContent + responseText.text, EndLine = new EndLine(), IndentLevel = new IndentLevel() { Level = 1 + indentLevel } });
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
                                    menuChoice.SyntacticalElements.Add(new ActionDescription() { TextContent = baseContent + secondChoiceText.text, EndLine = new EndLine(), IndentLevel = new IndentLevel() { Level = 1 + indentLevel } });
                                }
                            }


                            var thirdChoiceText = thirdChoiceBinaryExpression.content[0] as Ink.Parsed.Text;
                            if (thirdChoiceText != null)
                            {
                                menuChoice.SyntacticalElements.Add(new AttributeSpan() { });
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
                                    menuChoice.SyntacticalElements.Add(new ActionDescription() { TextContent = baseContent + thirdChoiceText.text, EndLine = new EndLine(), IndentLevel = new IndentLevel() { Level = 1 + indentLevel } });
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
                        menuChoice.SyntacticalElements.Add(new ActionDescription() { TextContent = baseContent + thirdChoiceText.text, EndLine = new EndLine(), IndentLevel = new IndentLevel() { Level = 1 + indentLevel } });

                        menuChoice.SyntacticalElements.Add(new AttributeSpan() { });
                    }
                    else
                    {
                        // wrong?
                        var y = 1;
                    }
                }
                var subWeaveContentList = weaveContent as Ink.Parsed.Weave;
                if (subWeaveContentList != null)
                {
                    MenuChoice menuChoice = null;
                    if (menuChoiceStack.Count > 0)
                    {
                        menuChoice = menuChoiceStack.Peek();
                    }
                    if (menuChoice != null)
                    {
                        ProcesWeave(menuChoice.SyntacticalElements, subWeaveContentList, 1 + indentLevel);
                    }
                    else
                    {
                        ProcesWeave(syntacticalElements, subWeaveContentList, 1 + indentLevel);
                    }
                }
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
