using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FountainExponential.LanguageStructures;
using FountainExponential.LanguageStructures.Lexical;
using FountainExponential.LanguageStructures.Lexical.InteractiveFlow;
using FountainExponential.LanguageStructures.Lexical.MetaData;
using FountainExponential.LanguageStructures.Lexical.Sections;
using FountainExponential.LanguageStructures.Syntactical;
using FountainExponential.LanguageStructures.Syntactical.Code;
using FountainExponential.LanguageStructures.Syntactical.FountainElement;
using FountainExponential.LanguageStructures.Syntactical.InteractiveFlow;
using FountainExponential.LanguageStructures.Syntactical.MetaData;
using FountainExponential.LanguageStructures.Syntactical.Sections;

namespace Ink.Ink2FountainExp.Adapting
{
    public class FountainRenderer
    {        
        public void Write(StringBuilder builder, FountainPlay fountainPlay)
        {
            var mainFile = fountainPlay.MainFile;
            //var act = mainFile.SyntacticalElements[2] as Act;
            //var sequence = act.SyntacticalElements[1] as Sequence;

            Write(builder, mainFile);
        }

        public void Write(StringBuilder builder, FountainFile mainFile)
        {
            Write(builder, mainFile.TitlePage);

            // Syntactic elements not belonging to any of the other sections. 
            Write(builder, mainFile.SyntacticalElements);

            Write(builder, new BlankLine());

            // Write from small to large because equal or bigger sections can not be contained by equal or smaller ones.
            Write(builder, mainFile.NanoSlice);
            Write(builder, mainFile.MicroSlices);
            Write(builder, mainFile.Slices);
            Write(builder, mainFile.Moments);
            Write(builder, mainFile.Scenes);
            Write(builder, mainFile.Sequences);
            Write(builder, mainFile.Acts);
        }

        #region Write MetaData

        public void Write(StringBuilder builder, TitlePage titlePage)
        {
            if (builder == null || titlePage == null)
                return;

            foreach (var keyInformation in titlePage.KeyInformationList)
            {
                var keyMultiLineValuePair = keyInformation as KeyMultiLineValuePair;
                if (keyMultiLineValuePair != null)
                {
                    Write(builder, keyMultiLineValuePair);
                }
                var keySingleLineValuePair = keyInformation as KeySingleLineValuePair;
                if (keySingleLineValuePair != null)
                {
                    Write(builder, keySingleLineValuePair);
                }
            }

            // A page break is implicit after the Title Page. Just drop down two lines and start writing your screenplay.
            Write(builder, titlePage.TitlePageBreakToken);
        }

        public void Write(StringBuilder builder, KeySingleLineValuePair keySingleLineValuePair)
        {
            if (builder == null || keySingleLineValuePair == null)
                return;

            builder.Append(keySingleLineValuePair.Key.Keyword);
            builder.Append(KeyValuePairAssignmentToken.Sign);
            builder.Append(SpaceToken.Sign);
            builder.Append(keySingleLineValuePair.Value);

            Write(builder, keySingleLineValuePair.EndLine);
        }

        public void Write(StringBuilder builder, KeyMultiLineValuePair keyMultiLineValuePair)
        {
            if (builder == null || keyMultiLineValuePair == null)
                return;

            builder.Append(keyMultiLineValuePair.Key.Keyword);
            builder.Append(KeyValuePairAssignmentToken.Sign);
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

                var container = element as SyntacticalElementContainer;
                if (container != null)
                {
                    Write(builder, container.SyntacticalElements);
                }
            }
        }

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
                if (menu != null)
                {
                    Write(builder, menu);
                }

                var blankLine = element as BlankLine;
                if (blankLine != null)
                {
                    Write(builder, blankLine);
                }

                var actionDescription = element as ActionDescription;
                if (actionDescription != null)
                {
                    Write(builder, actionDescription);
                }

                var containerBlock = element as ContainerBlock;
                if (containerBlock != null)
                {
                    Write(builder, containerBlock);
                }


                var container = element as SyntacticalElementContainer;
                if (container != null)
                {
                    Write(builder, container.SyntacticalElements);
                }
            }
        }

        public void Write(StringBuilder builder, Menu menu)
        {
            foreach (var choice in menu.Choices)
            {
                Write(builder, choice);
            }
        }

        public void Write(StringBuilder builder, MenuChoice menuChoice)
        {
            if (menuChoice == null)
                return;

            Write(builder, menuChoice.IndentLevel);

            if (menuChoice.MenuChoiceToken is StickyMenuChoiceToken)
                builder.Append(StickyMenuChoiceToken.Sign);
            else if(menuChoice.MenuChoiceToken is ConsumableMenuChoiceToken)
                builder.Append(ConsumableMenuChoiceToken.Sign);
            else if(menuChoice.MenuChoiceToken is ContinuingMenuChoiceToken)
                builder.Append(ContinuingMenuChoiceToken.Sign);
            else
                builder.Append(StickyMenuChoiceToken.Sign);

            builder.Append(SpaceToken.Sign);
            builder.Append(menuChoice.Description);
            Write(builder, menuChoice.EndLine);

            WriteContainerBlockElements(builder, menuChoice.SyntacticalElements);
        }

        #endregion InteractiveFlow

        #region Write Sections

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

        public void Write(StringBuilder builder, Act act)
        {
            if (builder == null || act == null)
                return;

            builder.Append(ActToken.Keyword);
            builder.Append(SpaceToken.Sign);
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

            builder.Append(SequenceToken.Keyword);
            builder.Append(SpaceToken.Sign);
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

            builder.Append(SceneToken.Keyword);
            builder.Append(SpaceToken.Sign);
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

            builder.Append(MomentToken.Keyword);
            builder.Append(SpaceToken.Sign);
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

            builder.Append(SliceToken.Keyword);
            builder.Append(SpaceToken.Sign);
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

            builder.Append(MicroSliceToken.Keyword);
            builder.Append(SpaceToken.Sign);
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

            builder.Append(NanoSliceToken.Keyword);
            builder.Append(SpaceToken.Sign);
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
