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
        public void Write(StringBuilder builder, List<ISyntacticalElementable> syntacticalElements)
        {
            foreach (var element in syntacticalElements)
            {
                var section = element as SectionBase;
                if (section != null)
                {
                    Write(builder, section);
                }

                var blankLine = element as BlankLine;
                if (blankLine != null)
                {
                    Write(builder, blankLine);
                }

                var containerBlock = element as ContainerBlock;
                if (containerBlock != null)
                {
                    Write(builder, containerBlock);
                }

                var actionDescription = element as ActionDescription;
                if (actionDescription != null)
                {
                    Write(builder, actionDescription);
                }
                

                var container = element as SyntacticalElementContainer;
                if (container != null)
                {
                    Write(builder, container.SyntacticalElements);
                }
            }
        }

        #region Write MetaData

        public void WriteMetaData(FountainFile mainFile, StringBuilder builder)
        {
            foreach (var keyInformation in mainFile.TitlePage.KeyInformationList)
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
            Write(builder, mainFile.TitlePage.TitlePageBreakToken);
        }

        public void Write(StringBuilder builder, KeySingleLineValuePair keySingleLineValuePair)
        {
            builder.Append(keySingleLineValuePair.Key.Keyword);
            builder.Append(KeyValuePairAssignmentToken.Sign);
            builder.Append(SpaceToken.Sign);
            builder.Append(keySingleLineValuePair.Value);

            Write(builder, keySingleLineValuePair.EndLine);
        }

        public void Write(StringBuilder builder, KeyMultiLineValuePair keyMultiLineValuePair)
        {
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
            foreach (var blankLine in TitlePageBreakToken.Pattern)
            {
                Write(builder, blankLine);
            }
        }

        #endregion Write MetaData

        #region Write Sections

        public void Write(StringBuilder builder, SectionBase section)
        {
            var act = section as Act;
            if (act != null)
            {
                Write(builder, act);
            }
            var sequence = section as Sequence;
            if (sequence != null)
            {
                Write(builder, sequence);
            }
            var scene = section as Scene;
            if (scene != null)
            {
                Write(builder, scene);
            }
            var moment = section as Moment;
            if (moment != null)
            {
                Write(builder, moment);
            }
            var slice = section as Slice;
            if (slice != null)
            {
                Write(builder, slice);
            }
            var microSlice = section as MicroSlice;
            if (microSlice != null)
            {
                Write(builder, microSlice);
            }
            var nanoSlice = section as NanoSlice;
            if (nanoSlice != null)
            {
                Write(builder, nanoSlice);
            }
        }

        public void Write(StringBuilder builder, Act act)
        {
            builder.Append(ActToken.Keyword);
            builder.Append(SpaceToken.Sign);
            builder.Append(act.SectionName);
            Write(builder, act.EndLine);
        }

        public void Write(StringBuilder builder, Sequence sequence)
        {
            builder.Append(SequenceToken.Keyword);
            builder.Append(SpaceToken.Sign);
            builder.Append(sequence.SectionName);
            Write(builder, sequence.EndLine);
        }

        public void Write(StringBuilder builder, Scene scene)
        {
            builder.Append(SceneToken.Keyword);
            builder.Append(SpaceToken.Sign);
            builder.Append(scene.SectionName);
            Write(builder, scene.EndLine);
        }

        public void Write(StringBuilder builder, Moment moment)
        {
            builder.Append(MomentToken.Keyword);
            builder.Append(SpaceToken.Sign);
            builder.Append(moment.SectionName);
            Write(builder, moment.EndLine);
        }

        public void Write(StringBuilder builder, Slice slice)
        {
            builder.Append(SliceToken.Keyword);
            builder.Append(SpaceToken.Sign);
            builder.Append(slice.SectionName);
            Write(builder, slice.EndLine);
        }

        public void Write(StringBuilder builder, MicroSlice microSlice)
        {
            builder.Append(MicroSliceToken.Keyword);
            builder.Append(SpaceToken.Sign);
            builder.Append(microSlice.SectionName);
            Write(builder, microSlice.EndLine);
        }

        public void Write(StringBuilder builder, NanoSlice nanoSlice)
        {
            builder.Append(NanoSliceToken.Keyword);
            builder.Append(SpaceToken.Sign);
            builder.Append(nanoSlice.SectionName);
            Write(builder, nanoSlice.EndLine);
        }

        #endregion Write Sections


        #region Write Fountain Elements

        public void Write(StringBuilder builder, ActionDescription actionDescription)
        {
            Write(builder, actionDescription.IndentLevel);
            builder.Append(actionDescription.TextContent);
            Write(builder, actionDescription.EndLine);
        }

        #endregion Write Fountain Elements

        #region InteractiveFlow

        public void Write(StringBuilder builder, ContainerBlock block)
        {
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

        #region Write Basic elements

        public void Write(StringBuilder builder, BlankLine blankLine)
        {
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
            if (indentLevel == null)
                return;

            for (int x = 0; x < indentLevel.Level; x++)
            {
                builder.Append("\t");
            }
        }

        #endregion Write Basic elements
    }
}
