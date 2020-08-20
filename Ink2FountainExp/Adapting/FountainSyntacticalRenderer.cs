using System;
using System.Collections.Generic;
using System.IO;
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
using FountainExponential.LanguageStructures.Syntactical.FountainElement;
using FountainExponential.LanguageStructures.Syntactical.InteractiveFlow;
using FountainExponential.LanguageStructures.Syntactical.MetaData;
using FountainExponential.LanguageStructures.Syntactical.Sections;

namespace Ink.Ink2FountainExp.Adapting
{
    /// <summary>The FountainSyntacticalRenderer encapsulates the rendering of syntactical elements.</summary>
    public class FountainSyntacticalRenderer
    {
        #region Properties

        /// <summary>Gets or sets the file system interactor.</summary>
        /// <value>The file system interactor.</value>
        public FountainLexicalRenderer LexicalRenderer { get; set; } = new FountainLexicalRenderer();

        #endregion Properties


        #region Write MetaData

        public void Write(StringBuilder builder, TitlePage titlePage)
        {
            if (builder == null || titlePage == null)
                return;

            foreach (var keyInformation in titlePage.KeyInformationList)
            {
                var keyMultiLineValuePair = keyInformation as KeyMultiLineValuePair;
                Write(builder, keyMultiLineValuePair);

                var keySingleLineValuePair = keyInformation as KeySingleLineValuePair;
                Write(builder, keySingleLineValuePair);
            }

            // A page break is implicit after the Title Page. Just drop down two lines and start writing your screenplay.
            Write(builder, titlePage.TitlePageBreakToken);
        }

        public void Write(StringBuilder builder, KeySingleLineValuePair keySingleLineValuePair)
        {
            if (builder == null || keySingleLineValuePair == null)
                return;

            LexicalRenderer.Write(builder, keySingleLineValuePair.Key);
            LexicalRenderer.Write(builder, keySingleLineValuePair.AssignmentToken);
            LexicalRenderer.Write(builder, keySingleLineValuePair.SpaceToken);
            builder.Append(keySingleLineValuePair.Value);

            Write(builder, keySingleLineValuePair.EndLine);
        }

        public void Write(StringBuilder builder, KeyMultiLineValuePair keyMultiLineValuePair)
        {
            if (builder == null || keyMultiLineValuePair == null)
                return;

            LexicalRenderer.Write(builder, keyMultiLineValuePair.Key);
            LexicalRenderer.Write(builder, keyMultiLineValuePair.AssignmentToken);
            Write(builder, keyMultiLineValuePair.EndLine);

            foreach (var line in keyMultiLineValuePair.ValueLineList)
            {
                builder.Append(KeyValuePairIndentToken.Indent);
                builder.Append(line.Value);

                Write(builder, line.EndLine);
            }
        }

        public void Write(StringBuilder builder, TitlePageBreakToken titlePageBreakToken)
        {
            if (builder == null)
                return;

            foreach (var blankLine in TitlePageBreakToken.Pattern)
            {
                Write(builder, blankLine);
            }
        }

        #endregion Write MetaData

        public void Write(StringBuilder builder, List<ISyntacticalElementable> syntacticalElements)
        {
            if (builder == null || syntacticalElements == null)
                return;

            foreach (var element in syntacticalElements)
            {
                var blankLine = element as BlankLine;
                Write(builder, blankLine);

                var containerBlock = element as ContainerBlock;
                Write(builder, containerBlock);

                var actionDescription = element as ActionDescription;
                Write(builder, actionDescription);

                var definingCodeBlock = element as DefiningCodeBlock;
                Write(builder, definingCodeBlock);

                var seperatedDeviation = element as SeperatedDeviation;
                Write(builder, seperatedDeviation);

                var container = element as SyntacticalElementContainer;
                if (container != null)
                {
                    Write(builder, container.SyntacticalElements);
                }
            }
        }

        #region Write Automatic Flow Elements

        public void Write(StringBuilder builder, SeperatedDeviation seperatedDeviation)
        {
            if (builder == null || seperatedDeviation == null)
                return;

            Write(builder, seperatedDeviation.IndentLevel);
            LexicalRenderer.Write(builder, seperatedDeviation.SeperatedDeviationToken);
            LexicalRenderer.Write(builder, seperatedDeviation.SpaceToken);
            LexicalRenderer.Write(builder, seperatedDeviation.FlowTargetToken);

            builder.Append(EndLine.Pattern);
        }

        #endregion Write Code Elements

        #region Write Code Elements

        public void Write(StringBuilder builder, DefiningCodeBlock definingCodeBlock)
        {
            if (builder == null || definingCodeBlock == null)
                return;

            builder.Append(CodeBlock.Keyword);
            builder.Append("+");
            builder.Append(EndLine.Pattern);
            builder.Append(definingCodeBlock.TextContent);
            builder.Append(EndLine.Pattern);
            builder.Append(CodeBlock.Keyword);
            builder.Append(EndLine.Pattern);
        }

        #endregion Write Code Elements

        #region Write Fountain Elements

        public void Write(StringBuilder builder, ActionDescription actionDescription)
        {
            if (builder == null || actionDescription == null)
                return;

            Write(builder, actionDescription.IndentLevel);
            builder.Append(actionDescription.TextContent);
            Write(builder, actionDescription.EndLine);
        }

        #endregion Write Fountain Elements

        #region InteractiveFlow

        public void Write(StringBuilder builder, ContainerBlock block)
        {
            if (builder == null || block == null)
                return;

            Write(builder, block.IndentLevel);
            builder.Append(ContainerBlockToken.Keyword);
            builder.Append(block.StartEndLine);

            WriteContainerBlockElements(builder, block.SyntacticalElements);

            Write(builder, block.IndentLevel);
            builder.Append(ContainerBlockToken.Keyword);
            builder.Append(block.CloseEndLine);
        }

        public void WriteContainerBlockElements(StringBuilder builder, List<ISyntacticalElementable> syntacticalElements)
        {
            foreach (var element in syntacticalElements)
            {
                // The container block can't have a section like a Scene or Moment
                // The container block is the only area that can have MenuChoices

                var menu = element as Menu;
                Write(builder, menu);

                var blankLine = element as BlankLine;
                Write(builder, blankLine);

                var actionDescription = element as ActionDescription;
                Write(builder, actionDescription);

                var containerBlock = element as ContainerBlock;
                Write(builder, containerBlock);

                var seperatedDeviation = element as SeperatedDeviation;
                Write(builder, seperatedDeviation);


                var container = element as SyntacticalElementContainer;
                if (container != null)
                {
                    Write(builder, container.SyntacticalElements);
                }
            }
        }

        public void Write(StringBuilder builder, Menu menu)
        {
            if (builder == null || menu == null)
                return;

            foreach (var choice in menu.Choices)
            {
                Write(builder, choice);
            }
        }

        public void Write(StringBuilder builder, MenuChoice menuChoice)
        {
            if (builder == null || menuChoice == null)
                return;

            Write(builder, menuChoice.IndentLevel);

            var stickyMenuChoiceToken = menuChoice.MenuChoiceToken as StickyMenuChoiceToken;
            LexicalRenderer.Write(builder, stickyMenuChoiceToken);

            var consumableMenuChoiceToken = menuChoice.MenuChoiceToken as ConsumableMenuChoiceToken;
            LexicalRenderer.Write(builder, consumableMenuChoiceToken);

            var continuingMenuChoiceToken = menuChoice.MenuChoiceToken as ContinuingMenuChoiceToken;
            LexicalRenderer.Write(builder, continuingMenuChoiceToken);

            // if no choice token is present we create a default choice token.
            if(stickyMenuChoiceToken == null && consumableMenuChoiceToken == null && continuingMenuChoiceToken == null)
                LexicalRenderer.Write(builder, new StickyMenuChoiceToken());

            LexicalRenderer.Write(builder, menuChoice.SpaceToken);
            builder.Append(menuChoice.Description);
            Write(builder, menuChoice.EndLine);

            WriteContainerBlockElements(builder, menuChoice.SyntacticalElements);
        }

        #endregion InteractiveFlow

        #region Write Sections

        #region Write Section Lists

        public void Write(StringBuilder builder, List<Act> acts)
        {
            foreach (var act in acts)
            {
                Write(builder, act);
            }
        }
        public void Write(StringBuilder builder, List<Sequence> sequences)
        {
            foreach (var sequence in sequences)
            {
                Write(builder, sequence);
            }
        }
        public void Write(StringBuilder builder, List<Scene> scenes)
        {
            foreach (var scene in scenes)
            {
                Write(builder, scene);
            }
        }
        public void Write(StringBuilder builder, List<Moment> moments)
        {
            foreach (var moment in moments)
            {
                Write(builder, moment);
            }
        }
        public void Write(StringBuilder builder, List<Slice> slices)
        {
            foreach (var slice in slices)
            {
                Write(builder, slice);
            }
        }
        public void Write(StringBuilder builder, List<MicroSlice> microSlices)
        {
            foreach (var microSlice in microSlices)
            {
                Write(builder, microSlice);
            }
        }
        public void Write(StringBuilder builder, List<NanoSlice> nanoSlices)
        {
            foreach (var nanoSlice in nanoSlices)
            {
                Write(builder, nanoSlice);
            }
        }

        #endregion Write Section Lists

        public void Write(StringBuilder builder, Act act)
        {
            if (builder == null || act == null)
                return;

            LexicalRenderer.Write(builder, act.ActStartToken);
            LexicalRenderer.Write(builder, act.SpaceToken);
            builder.Append(act.SectionName);
            Write(builder, act.EndLine);

            Write(builder, act.SyntacticalElements);

            // Write from small to large because equal or bigger sections can not be contained by equal or smaller ones.
            Write(builder, act.NanoSlice);
            Write(builder, act.MicroSlices);
            Write(builder, act.Slices);
            Write(builder, act.Moments);
            Write(builder, act.Scenes);
            Write(builder, act.Sequences);
        }

        public void Write(StringBuilder builder, Sequence sequence)
        {
            if (builder == null || sequence == null)
                return;

            LexicalRenderer.Write(builder, sequence.SequenceStartToken);
            LexicalRenderer.Write(builder, sequence.SpaceToken);
            builder.Append(sequence.SectionName);
            Write(builder, sequence.EndLine);

            Write(builder, sequence.SyntacticalElements);

            // Write from small to large because equal or bigger sections can not be contained by equal or smaller ones.
            Write(builder, sequence.NanoSlice);
            Write(builder, sequence.MicroSlices);
            Write(builder, sequence.Slices);
            Write(builder, sequence.Moments);
            Write(builder, sequence.Scenes);
        }

        public void Write(StringBuilder builder, Scene scene)
        {
            if (builder == null || scene == null)
                return;

            LexicalRenderer.Write(builder, scene.SceneStartToken);
            LexicalRenderer.Write(builder, scene.SpaceToken);
            builder.Append(scene.SectionName);
            Write(builder, scene.EndLine);

            Write(builder, scene.SyntacticalElements);

            // Write from small to large because equal or bigger sections can not be contained by equal or smaller ones.
            Write(builder, scene.NanoSlice);
            Write(builder, scene.MicroSlices);
            Write(builder, scene.Slices);
            Write(builder, scene.Moments);
        }

        public void Write(StringBuilder builder, Moment moment)
        {
            if (builder == null || moment == null)
                return;

            LexicalRenderer.Write(builder, moment.MomentStartToken);
            LexicalRenderer.Write(builder, moment.SpaceToken);
            builder.Append(moment.SectionName);
            Write(builder, moment.EndLine);

            Write(builder, moment.SyntacticalElements);

            // Write from small to large because equal or bigger sections can not be contained by equal or smaller ones.
            Write(builder, moment.NanoSlice);
            Write(builder, moment.MicroSlices);
            Write(builder, moment.Slices);
        }

        public void Write(StringBuilder builder, Slice slice)
        {
            if (builder == null || slice == null)
                return;

            LexicalRenderer.Write(builder, slice.SliceStartToken);
            LexicalRenderer.Write(builder, slice.SpaceToken);
            builder.Append(slice.SectionName);
            Write(builder, slice.EndLine);

            Write(builder, slice.SyntacticalElements);

            // Write from small to large because equal or bigger sections can not be contained by equal or smaller ones.
            Write(builder, slice.NanoSlice);
            Write(builder, slice.MicroSlices);
        }

        public void Write(StringBuilder builder, MicroSlice microSlice)
        {
            if (builder == null || microSlice == null)
                return;

            LexicalRenderer.Write(builder, microSlice.MicroSliceStartToken);
            LexicalRenderer.Write(builder, microSlice.SpaceToken);
            builder.Append(microSlice.SectionName);
            Write(builder, microSlice.EndLine);

            Write(builder, microSlice.SyntacticalElements);

            // Write from small to large because equal or bigger sections can not be contained by equal or smaller ones.
            Write(builder, microSlice.NanoSlice);
        }

        public void Write(StringBuilder builder, NanoSlice nanoSlice)
        {
            if (builder == null || nanoSlice == null)
                return;

            LexicalRenderer.Write(builder, nanoSlice.NanoSliceStartToken);
            LexicalRenderer.Write(builder, nanoSlice.SpaceToken);
            builder.Append(nanoSlice.SectionName);
            Write(builder, nanoSlice.EndLine);

            Write(builder, nanoSlice.SyntacticalElements);
        }

        #endregion Write Sections

        #region Write Basic elements

        public void Write(StringBuilder builder, BlankLine blankLine)
        {
            if (builder == null || blankLine == null)
                return;

            Write(builder, BlankLine.Pattern);
        }

        public void Write(StringBuilder builder, EndLine endLine)
        {
            if (builder == null || endLine == null)
                return;

            // change to \r or \n when rendering for apple or Unix.
            builder.Append("\r\n");
        }

        public void Write(StringBuilder builder, IndentLevel indentLevel)
        {
            if (builder == null || indentLevel == null)
                return;

            for (int x = 0; x < indentLevel.Level; x++)
            {
                builder.Append("\t");
            }
        }

        #endregion Write Basic elements    
    }
}
