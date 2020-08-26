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
using FountainExponential.LanguageStructures.Lexical.Code;
using FountainExponential.LanguageStructures.Lexical.InteractiveFlow;
using FountainExponential.LanguageStructures.Lexical.MetaData;
using FountainExponential.LanguageStructures.Lexical.Sections;
using FountainExponential.LanguageStructures.Syntactical;
using FountainExponential.LanguageStructures.Syntactical.AutomaticFlow;
using FountainExponential.LanguageStructures.Syntactical.Code;
using FountainExponential.LanguageStructures.Syntactical.Conditional;
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

        public bool MoveLabeledGatherToSection { get; set; } = false; // true
        public bool MoveLabeledChoiceToSection { get; set; } = false; // false

        #endregion Properties

        public string ConvertToFountainExponential(Ink.Parsed.Fiction parsedFiction, string inputFileName)
        {
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



            var actI = new Act() { SectionName = "ActI", ActStartToken = new ActToken(), SpaceToken = new SpaceToken(), EndLine = new EndLine() };
            mainFile.Acts.Add(actI);
            actI.SyntacticalElements.Add(new BlankLine());

            AddSequences(actI, parsedFiction);

            MergeCodeBlocks(mainFile, actI);

            return fountainPlay;
        }

        public void AddSequences(Act act, Parsed.Fiction parsedFiction)
        {
            foreach (var parsedObject in parsedFiction.content)
            {
                //var flowBase = parsedObject as Parsed.FlowBase;
                //if (flowBase != null && flowBase.isFunction)
                //    continue;

                var contentArea = new ContentArea() { Act = act, IndentLevel = 0 };
                var contentAreaManager = new ContentAreaManager(contentArea);
                // some non function, knot or stitch object can be handled.
                HandleParsedObject(contentAreaManager, parsedObject);
            }
        }

        /// <summary>Handles the parsed object. Did not function overload the handle function for the base object handling to avoid loops when the specific function is not defined.</summary>
        /// <param name="contentAreaManager">The content area manager.</param>
        /// <param name="parsedObject">The parsed object.</param>
        /// <returns></returns>
        public bool HandleParsedObject(ContentAreaManager contentAreaManager, Parsed.Object parsedObject)
        {
            bool handled = false;

            var flowBase = parsedObject as Parsed.FlowBase;
            if (Handle(contentAreaManager, flowBase))
                return true;

            var gather = parsedObject as Parsed.Gather;
            if (Handle(contentAreaManager, gather))
                return true;

            var choice = parsedObject as Parsed.Choice;
            if (Handle(contentAreaManager, choice))
                return true;

            var weave = parsedObject as Parsed.Weave;
            if (Handle(contentAreaManager, weave))
                return true;

            var text = parsedObject as Parsed.Text;
            if (Handle(contentAreaManager, text))
                return true;

            var divert = parsedObject as Parsed.Divert;
            if (Handle(contentAreaManager, divert))
                return true;

            var contentList = parsedObject as Parsed.ContentList;
            if (Handle(contentAreaManager, contentList))
                return true;

            var authorWarning = parsedObject as Parsed.AuthorWarning;
            if (Handle(contentAreaManager, authorWarning))
                return true;

            var conditional = parsedObject as Parsed.Conditional;
            if (Handle(contentAreaManager, conditional))
                return true;

            var listDefinition = parsedObject as Parsed.ListDefinition;
            if (Handle(contentAreaManager, listDefinition))
                return true;

            var sequence = parsedObject as Parsed.Sequence;
            if (Handle(contentAreaManager, sequence))
                return true;

            var tunnelOnwards = parsedObject as Parsed.TunnelOnwards;
            if (Handle(contentAreaManager, tunnelOnwards))
                return true;

            //var Wrap = parsedObject as Parsed.Wrap;


            var conditionalSingleBranch = parsedObject as Parsed.ConditionalSingleBranch;
            if (Handle(contentAreaManager, conditionalSingleBranch))
                return true;

            var expression = parsedObject as Parsed.Expression;
            if (Handle(contentAreaManager, expression))
                return true;

            var variableAssignment = parsedObject as Parsed.VariableAssignment;
            if (Handle(contentAreaManager, variableAssignment))
                return true;

            var constantDeclaration = parsedObject as Parsed.ConstantDeclaration;
            if (Handle(contentAreaManager, constantDeclaration))
                return true;

            var externalDeclaration = parsedObject as Parsed.ExternalDeclaration;
            if (Handle(contentAreaManager, externalDeclaration))
                return true;

            var includedFile = parsedObject as Parsed.IncludedFile;
            if (Handle(contentAreaManager, includedFile))
                return true;

            var theReturn = parsedObject as Parsed.Return;
            if (Handle(contentAreaManager, theReturn))
                return true;

            return handled;
        }

        #region Map Automatic Flow

        private bool Handle(ContentAreaManager contentAreaManager, Parsed.Divert divert)
        {
            if (divert == null)
                return false;

            if (divert.isTunnel)
            {
                var detour = new SeparatedDetour()
                {
                    FlowTargetToken = new AutomaticFlowTargetToken()
                    {
                        Label = divert.target.dotSeparatedComponents
                    },
                    IndentLevel = new IndentLevel(),
                    SpaceToken = new SpaceToken(),
                    SeparatedDetourToken = new SeparatedDetourToken(),
                    EndLine = new EndLine()
                };

                var detouredContentArea = contentAreaManager.CurrentContentArea;
                detouredContentArea.AddSyntacticalElement(detour);
            }
            else
            {
                var deviation = new SeparatedDeviation()
                {
                    FlowTargetToken = new AutomaticFlowTargetToken()
                    {
                        Label = divert.target.dotSeparatedComponents
                    },
                    IndentLevel = new IndentLevel(),
                    SpaceToken = new SpaceToken(),
                    SeparatedDeviationToken = new SeparatedDeviationToken(),
                    EndLine = new EndLine()
                };

                var divertContentArea = contentAreaManager.CurrentContentArea;
                divertContentArea.AddSyntacticalElement(deviation);
            }
            return true;
        }

        private bool Handle(ContentAreaManager contentAreaManager, Parsed.TunnelOnwards parsedTunnelOnwards)
        {
            if (parsedTunnelOnwards == null)
                return false;

            // The ->-> is a return to if a tunnel was used. A tunnel being -> crossing_the_date_line ->
            // The Detour returns automatically after the section is done so we do not need to do anything here.


            return true;
        }

        #endregion Map Automatic Flow

        #region Map Code
        public void AddGlobalFunctions(FountainFile file, Parsed.Fiction parsedFiction)
        {
            foreach (var parsedObject in parsedFiction.content)
            {
                var knot = parsedObject as Parsed.Knot;
                if (knot != null && knot.isFunction)
                {
                    var contentArea = new ContentArea() { File = file, IndentLevel = 0 };
                    var contentAreaManager = new ContentAreaManager(contentArea);
                    Handle(contentAreaManager, knot);
                }
            }
        }

        private static void MergeCodeBlocks(FountainFile mainFile, Act actI)
        {
            var codeBlocks = actI.SyntacticalElements.OfType<DefiningCodeBlock>().ToList();
            var builder = new StringBuilder();

            bool first = true;
            bool previousIsConst = false;
            bool previousIsVar = false;
            foreach (var block in codeBlocks)
            {
                actI.SyntacticalElements.Remove(block);


                bool currentIsConst = block.TextContent.StartsWith("const");
                bool currentIsVar = block.TextContent.StartsWith("var");
                bool currentIsFunction = block.TextContent.StartsWith("function"); 
                if (!first && (previousIsConst != currentIsConst || previousIsVar != currentIsVar || currentIsFunction))
                {
                    builder.AppendLine();
                }
                previousIsConst = currentIsConst;
                previousIsVar = currentIsVar;

                builder.AppendLine(block.TextContent);
                first = false;
            }
            var mergedDefiningCodeBlock = new DefiningCodeBlock()
            {
                CodeBlockStartToken = new CodeBlockStartToken(),
                TextContent = builder.ToString(),
                CodeBlockEndToken = new CodeBlockEndToken(),
                DefiningCodeBlockToken = new DefiningCodeBlockToken(),
            };
            mainFile.SyntacticalElements.Add(mergedDefiningCodeBlock);
        }


        private bool Handle(ContentAreaManager contentAreaManager, Parsed.Conditional parsedConditional)
        {
            if (parsedConditional == null)
                return false;

            var condition = new BinaryCondition()
            {
                TextContent = parsedConditional.ToString()
            };

            var conditionContentArea = contentAreaManager.CurrentContentArea;
            conditionContentArea.AddSyntacticalElement(condition);

            return true;
        }

        private bool Handle(ContentAreaManager contentAreaManager, Parsed.ConditionalSingleBranch parsedConditionalSingleBranch)
        {
            if (parsedConditionalSingleBranch == null)
                return false;

            var condition = new SingularCondition()
            {
                TextContent = parsedConditionalSingleBranch.ToString()
            };

            var conditionContentArea = contentAreaManager.CurrentContentArea;
            conditionContentArea.AddSyntacticalElement(condition);

            return true;
        }

        private bool Handle(ContentAreaManager contentAreaManager, Parsed.Expression parsedExpression)
        {
            if (parsedExpression == null)
                return false;


            var binaryExpression = parsedExpression as Parsed.BinaryExpression;
            if (Handle(contentAreaManager, binaryExpression))
                return true;

            var divertTarget = parsedExpression as Parsed.DivertTarget;
            if (Handle(contentAreaManager, divertTarget))
                return true;

            var functionCall = parsedExpression as Parsed.FunctionCall;
            if (Handle(contentAreaManager, functionCall))
                return true;

            var incDecExpression = parsedExpression as Parsed.IncDecExpression;
            if (Handle(contentAreaManager, incDecExpression))
                return true;

            var multipleConditionExpression = parsedExpression as Parsed.MultipleConditionExpression;
            if (Handle(contentAreaManager, multipleConditionExpression))
                return true;

            var unaryExpression = parsedExpression as Parsed.UnaryExpression;
            if (Handle(contentAreaManager, unaryExpression))
                return true;

            var variableReference = parsedExpression as Parsed.VariableReference;
            if (Handle(contentAreaManager, variableReference))
                return true;

            // Expression is abstract so we shouldn't get here, unless we missed a derivative.

            return true;
        }

        private bool Handle(ContentAreaManager contentAreaManager, Parsed.BinaryExpression binaryExpression)
        {
            if (binaryExpression == null)
                return false;



            return true;
        }

        private bool Handle(ContentAreaManager contentAreaManager, Parsed.DivertTarget divertTarget)
        {
            if (divertTarget == null)
                return false;



            return true;
        }

        private bool Handle(ContentAreaManager contentAreaManager, Parsed.FunctionCall functionCall)
        {
            if (functionCall == null)
                return false;



            return true;
        }

        private bool Handle(ContentAreaManager contentAreaManager, Parsed.IncDecExpression incDecExpression)
        {
            if (incDecExpression == null)
                return false;



            return true;
        }

        private bool Handle(ContentAreaManager contentAreaManager, Parsed.MultipleConditionExpression multipleConditionExpression)
        {
            if (multipleConditionExpression == null)
                return false;



            return true;
        }

        private bool Handle(ContentAreaManager contentAreaManager, Parsed.UnaryExpression unaryExpression)
        {
            if (unaryExpression == null)
                return false;



            return true;
        }

        private bool Handle(ContentAreaManager contentAreaManager, Parsed.VariableReference variableReference)
        {
            if (variableReference == null)
                return false;



            return true;
        }

        private bool Handle(ContentAreaManager contentAreaManager, Parsed.ConstantDeclaration parsedConstantDeclaration)
        {
            if (parsedConstantDeclaration == null)
                return false;

            var definingCodeBlock = new DefiningCodeBlock()
            {
                DefiningCodeBlockToken = new DefiningCodeBlockToken(),
                CodeBlockStartToken = new CodeBlockStartToken(),
                CodeBlockEndToken = new CodeBlockEndToken(),
            };
            var builder = new StringBuilder();
            builder.Append("const ");
            builder.Append(parsedConstantDeclaration.constantName);
            builder.Append(" = ");
            builder.Append(parsedConstantDeclaration.expression.ToString());
            builder.Append(";");
            definingCodeBlock.TextContent = builder.ToString();
            var currentContentArea = contentAreaManager.CurrentContentArea;
            if (currentContentArea != null)
                currentContentArea.AddSyntacticalElement(definingCodeBlock);

            return true;
        }

        private bool Handle(ContentAreaManager contentAreaManager, Parsed.VariableAssignment parsedVariableAssignment)
        {
            if (parsedVariableAssignment == null)
                return false;

            var definingCodeBlock = new DefiningCodeBlock()
            {
                DefiningCodeBlockToken = new DefiningCodeBlockToken(),
                CodeBlockStartToken = new CodeBlockStartToken(),
                CodeBlockEndToken = new CodeBlockEndToken(),
            };
            var builder = new StringBuilder();
            builder.Append("var ");
            builder.Append(parsedVariableAssignment.variableName);
            builder.Append(" = ");
            builder.Append(parsedVariableAssignment.expression.ToString());
            builder.Append(";");
            definingCodeBlock.TextContent = builder.ToString();
            var currentContentArea = contentAreaManager.CurrentContentArea;
            if (currentContentArea != null)
                currentContentArea.AddSyntacticalElement(definingCodeBlock);

            return true;
        }

        private bool Handle(ContentAreaManager contentAreaManager, Parsed.ExternalDeclaration parsedExternalDeclaration)
        {
            if (parsedExternalDeclaration == null)
                return false;

            var definingCodeBlock = new DefiningCodeBlock()
            {
                DefiningCodeBlockToken = new DefiningCodeBlockToken(),
                CodeBlockStartToken = new CodeBlockStartToken(),
                CodeBlockEndToken = new CodeBlockEndToken(),
            };
            definingCodeBlock.TextContent = parsedExternalDeclaration.ToString();
            var currentContentArea = contentAreaManager.CurrentContentArea;
            if (currentContentArea != null)
                currentContentArea.AddSyntacticalElement(definingCodeBlock);

            return true;
        }

        private bool Handle(ContentAreaManager contentAreaManager, Parsed.IncludedFile parsedIncludedFile)
        {
            if (parsedIncludedFile == null)
                return false;

            var definingCodeBlock = new DefiningCodeBlock()
            {
                DefiningCodeBlockToken = new DefiningCodeBlockToken(),
                CodeBlockStartToken = new CodeBlockStartToken(),
                CodeBlockEndToken = new CodeBlockEndToken(),
            };
            definingCodeBlock.TextContent = parsedIncludedFile.ToString();
            var currentContentArea = contentAreaManager.CurrentContentArea;
            if (currentContentArea != null)
                currentContentArea.AddSyntacticalElement(definingCodeBlock);

            return true;
        }

        private bool Handle(ContentAreaManager contentAreaManager, Parsed.Return parsedReturn)
        {
            if (parsedReturn == null)
                return false;

            var actionDescription = new ActionDescription() { TextContent = parsedReturn.returnedExpression.ToString(), IndentLevel = new IndentLevel() };

            var actionContentArea = contentAreaManager.CurrentContentArea;
            actionContentArea.AddSyntacticalElement(actionDescription);

            return true;
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

        #endregion Map Code

        #region Map Comment 

        #endregion Map Comment

        #region Map Data

        #endregion Map Data

        #region Map Emphasis

        #endregion Map Emphasis

        #region Map FountainElement

        private bool Handle(ContentAreaManager contentAreaManager, Parsed.Text parsedText)
        {
            if (parsedText == null)
                return false;

            var actionDescription = new ActionDescription() { TextContent = parsedText.text, IndentLevel = new IndentLevel() };

            var actionContentArea = contentAreaManager.CurrentContentArea;
            actionContentArea.AddSyntacticalElement(actionDescription);

            return true;
        }

        #endregion Map FountainElement

        #region Map Interactive Flow

        public bool Handle(ContentAreaManager contentAreaManager, Parsed.Weave weave)
        {
            if (contentAreaManager == null || weave == null)
                return false;

            var menuStack = new Stack<Menu>();
            var menuChoiceStack = new Stack<MenuChoice>();
            ContentArea choiceContentArea = null;
            foreach (var weaveContent in weave.content)
            {
                var weaveGather = weaveContent as Ink.Parsed.Gather;
                if (ProcessGather(contentAreaManager, weaveGather, menuStack, menuChoiceStack))
                    continue;

                var weaveChoice = weaveContent as Ink.Parsed.Choice;
                ContentArea newChoiceContentArea = null;
                if (ProcessWeaveChoice(contentAreaManager, weaveChoice, menuStack, menuChoiceStack, out newChoiceContentArea))
                {
                    choiceContentArea = newChoiceContentArea;
                    continue;
                }

                var subWeave = weaveContent as Ink.Parsed.Weave;
                if (Handle(contentAreaManager, subWeave))
                    continue;

                // Do the non indent and flow handling.
                HandleParsedObject(contentAreaManager, weaveContent);
            }

            // if a choice content area was created we have to remove it from the stack if we go out of the weave.
            if (choiceContentArea != null)
            {
                if (choiceContentArea == contentAreaManager.CurrentContentArea)
                {
                    contentAreaManager.PopCurrentContentArea();
                }
            }

            return true;
        }

        private bool Handle(ContentAreaManager contentAreaManager, Parsed.Choice parsedChoice)
        {
            if (parsedChoice == null)
                return false;


            return true;
        }

        private bool ProcessWeaveChoice(ContentAreaManager contentAreaManager, Parsed.Choice weaveChoice, Stack<Menu> menuStack, Stack<MenuChoice> menuChoiceStack, out ContentArea newChoiceContentArea)
        {
            newChoiceContentArea = null;

            if (contentAreaManager == null || weaveChoice == null)
                return false;

            MenuChoice menuChoice = null;
            if (!weaveChoice.onceOnly)
            {
                menuChoice = new PersistentMenuChoice()
                {
                    MenuChoiceToken = new PersistentMenuChoiceToken(),
                    SpaceToken = new SpaceToken(),
                    EndLine = new EndLine(),
                    IndentLevel = new IndentLevel()
                };
            }
            else
            {
                menuChoice = new ConsumableMenuChoice()
                {
                    MenuChoiceToken = new ConsumableMenuChoiceToken(),
                    SpaceToken = new SpaceToken(),
                    EndLine = new EndLine(),
                    IndentLevel = new IndentLevel()
                };
            }

            var baseContentArea = contentAreaManager.CurrentContentArea;
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
                    IndentLevel = new IndentLevel() { Level = baseContentArea.IndentLevel }
                };
                baseContentArea.AddSyntacticalElement(containerBlock);
                // The menu choice is always on the same level as the container block.
                menuChoice.IndentLevel.Level = baseContentArea.IndentLevel;

                menu = new Menu();
                containerBlock.SyntacticalElements.Add(menu);
                menuStack.Push(menu);
            }
            else
            {
                // The previous menu is done so we pop it.
                contentAreaManager.PopCurrentContentArea();

                var belowContentArea = contentAreaManager.CurrentContentArea;
                menuChoice.IndentLevel.Level = belowContentArea.IndentLevel;
            }
            newChoiceContentArea = PushChoiceContentArea(contentAreaManager, weaveChoice.name, menuChoice);


            menu.Choices.Add(menuChoice);
            menuChoiceStack.Push(menuChoice);

            HandleChoiceContent(weaveChoice, contentAreaManager.CurrentContentArea, menuChoice);

            return true;
        }

        private ContentArea PushChoiceContentArea(ContentAreaManager contentAreaManager, string labelName, MenuChoice menuChoice)
        {
            if (contentAreaManager == null)
                return null;

            var originalContentArea = contentAreaManager.CurrentContentArea;

            // We only push a new content area when the name is set of the Gather.
            if (!MoveLabeledChoiceToSection || string.IsNullOrEmpty(labelName))
            {
                var indentedContentArea = contentAreaManager.CreateIndentedContentArea();
                indentedContentArea.MenuChoice = menuChoice;
                return indentedContentArea;
            }
            else
            {
                var indentedContentArea = contentAreaManager.CreateIndentedContentArea();
                indentedContentArea.MenuChoice = menuChoice;


                var detour = new SeparatedDetour()
                {
                    FlowTargetToken = new AutomaticFlowTargetToken(),
                    SpaceToken = new SpaceToken(),
                    SeparatedDetourToken = new SeparatedDetourToken(),
                    EndLine = new EndLine()
                };

                indentedContentArea.AddSyntacticalElement(detour);

                contentAreaManager.PopCurrentContentArea();


                // if the weave has a name, it is labeled and we want to make it a separate section.
                var newContentArea = contentAreaManager.CreateSubsectionContentArea(labelName);
                detour.FlowTargetToken.Label = newContentArea.GetCurrentSectionName();

                return newContentArea;
            }
        }

        private bool Handle(ContentAreaManager contentAreaManager, Parsed.Gather gather)
        {
            if (gather == null)
                return false;


            return true;
        }

        private bool ProcessGather(ContentAreaManager contentAreaManager, Parsed.Gather weaveGather, Stack<Menu> menuStack, Stack<MenuChoice> menuChoiceStack)
        {
            if (weaveGather == null)
                return false;

            // A gather will always end a menu and drop the indent to 0
            menuChoiceStack.Clear();
            menuStack.Clear();
            contentAreaManager.ResetContentAreaStack();

            PushGatherContentArea(contentAreaManager, weaveGather.name);

            return true;
        }

        private ContentArea PushGatherContentArea(ContentAreaManager contentAreaManager, string labelName)
        {
            // We only push a new content area when the name is set of the Gather.
            if (!MoveLabeledGatherToSection || string.IsNullOrEmpty(labelName))
                return null;

            var originalContentArea = contentAreaManager.CurrentContentArea;

            // if the weave has a name, it is labeled and we want to make it a separate section.
            var subsectionContentArea = contentAreaManager.CreateSubsectionContentArea(labelName);


            var detour = new SeparatedDetour()
            {
                FlowTargetToken = new AutomaticFlowTargetToken()
                {
                    Label = subsectionContentArea.GetCurrentSectionName()
                },
                IndentLevel = new IndentLevel(),
                SpaceToken = new SpaceToken(),
                SeparatedDetourToken = new SeparatedDetourToken(),
                EndLine = new EndLine()
            };

            originalContentArea.AddSyntacticalElement(detour);

            return subsectionContentArea;
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

        #endregion Map Interactive Flow

        #region Map MarkdownElement

        #endregion Map MarkdownElement

        #region Map MetaData

        public void AddMetaData(string inputFileName, FountainFile mainFile)
        {
            // The optional Title Page is always the first thing in a Fountain document. 
            // Information is encoding in the format key: value. 
            // Keys can have spaces (e. g. Draft date), but must end with a colon.
            // Example:
            // Title:
            //    _** BRICK &STEEL * *_
            //    _** FULL RETIRED** _
            //Credit: Written by
            //Author: Stu Mark
            //Source: Story by KTM
            //Draft date: 1 / 20 / 2012
            //Contact:
            //    Next Level Productions
            //    1588 Mission Dr.
            //    Los Angeles, CA 93463

            mainFile.TitlePage.TitlePageBreakToken = new TitlePageBreakToken();
            var title = new Title() 
            { 
                Key = new KeyValuePairKeyToken() 
                { 
                    Keyword = "Title" 
                }, 
                AssignmentToken = new KeyValuePairAssignmentToken(), 
                EndLine = new EndLine() 
            };
            title.ValueLineList.Add(new ValueLine() { Value = System.IO.Path.GetFileNameWithoutExtension(inputFileName), IndentToken = new KeyValuePairIndentToken(), EndLine = new EndLine() });
            mainFile.TitlePage.KeyInformationList.Add(title);
            var draftDate = new DraftDate()
            {
                Key = new KeyValuePairKeyToken()
                {
                    Keyword = "Draft date"
                },
                AssignmentToken = new KeyValuePairAssignmentToken(),
                SpaceToken = new SpaceToken(),
                Value = DateTime.Now.ToShortDateString(),
                EndLine = new EndLine(),
            };
            mainFile.TitlePage.KeyInformationList.Add(draftDate);

            mainFile.TitlePage.TitlePageBreakToken = new TitlePageBreakToken();
        }

        #endregion Map MetaData

        #region Map Section

        private bool Handle(ContentAreaManager contentAreaManager, Parsed.FlowBase flowBase)
        {
            if (flowBase == null)
                return false;

            var fiction = flowBase as Parsed.Fiction;
            Handle(contentAreaManager, fiction);

            var knot = flowBase as Parsed.Knot;
            Handle(contentAreaManager, knot);

            var stitch = flowBase as Parsed.Stitch;
            Handle(contentAreaManager, stitch);

            return true;
        }

        private bool Handle(ContentAreaManager contentAreaManager, Parsed.Fiction fiction)
        {
            if (fiction == null)
                return false;

            // The Fiction is generally handled at a higher level, so getting here is probably an error.

            return true;
        }

        private bool Handle(ContentAreaManager contentAreaManager, Parsed.Knot knot)
        {
            if (knot == null)
                return false;

            if (knot.isFunction)
            {
                var definingCodeBlock = new DefiningCodeBlock()
                {
                    DefiningCodeBlockToken = new DefiningCodeBlockToken(),
                    CodeBlockStartToken = new CodeBlockStartToken(),
                    CodeBlockEndToken = new CodeBlockEndToken(),
                };
                definingCodeBlock.TextContent = CreateCodeContainerContent(knot);
                var currentContentArea = contentAreaManager.CurrentContentArea;
                if (currentContentArea != null)
                    currentContentArea.AddSyntacticalElement(definingCodeBlock);
            }
            else
            {
                var currentContentArea = contentAreaManager.CreateSubsectionContentArea(knot.name);
                var knotContentAreaManager = new ContentAreaManager(currentContentArea);

                foreach (var content in knot.content)
                {
                    HandleParsedObject(knotContentAreaManager, content);
                }
            }
            return true;
        }
        private bool Handle(ContentAreaManager contentAreaManager, Parsed.Stitch stitch)
        {
            if (stitch == null)
                return false;

            var currentContentArea = contentAreaManager.CreateSubsectionContentArea(stitch.name);
            var stitchContentAreaManager = new ContentAreaManager(currentContentArea);
            foreach (var content in stitch.content)
            {
                HandleParsedObject(stitchContentAreaManager, content);
            }

            return true;
        }

        #endregion Map Section 

        #region Map Basic

        private bool Handle(ContentAreaManager contentAreaManager, Parsed.AuthorWarning parsedAuthorWarning)
        {
            if (parsedAuthorWarning == null)
                return false;

            var actionDescription = new ActionDescription() { TextContent = parsedAuthorWarning.warningMessage, IndentLevel = new IndentLevel() };

            var actionContentArea = contentAreaManager.CurrentContentArea;
            actionContentArea.AddSyntacticalElement(actionDescription);

            return true;
        }

        private bool Handle(ContentAreaManager contentAreaManager, Parsed.Sequence parsedSequence)
        {
            if (parsedSequence == null)
                return false;


            return true;
        }

        private bool Handle(ContentAreaManager contentAreaManager, Parsed.ContentList parsedContentList)
        {
            if (parsedContentList == null)
                return false;

            foreach (var content in parsedContentList.content)
            {
                HandleParsedObject(contentAreaManager, content);
            }

            return true;
        }

        private bool Handle(ContentAreaManager contentAreaManager, Parsed.ListDefinition parsedListDefinition)
        {
            if (parsedListDefinition == null)
                return false;


            return true;
        }

        #endregion Map Basic
    }
}
