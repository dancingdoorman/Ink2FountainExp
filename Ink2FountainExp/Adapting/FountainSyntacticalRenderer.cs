﻿using System;
using System.Collections.Generic;
using System.IO;
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
using FountainExponential.LanguageStructures.Syntactical.FountainElement;
using FountainExponential.LanguageStructures.Syntactical.InteractiveFlow;
using FountainExponential.LanguageStructures.Syntactical.MetaData;
using FountainExponential.LanguageStructures.Syntactical.Sections;
using FountainExponential.LanguageStructures.Syntactical.Set;

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
                builder.Append(KeyValuePairIndentToken.Keyword);
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

                var arrangement = element as Arrangement;
                Write(builder, arrangement);

                var conditional = element as Conditional;
                Write(builder, conditional);

                var definingCodeBlock = element as DefiningCodeBlock;
                Write(builder, definingCodeBlock);

                var separatedDetour = element as SeparatedDetour;
                Write(builder, separatedDetour);

                var seperatedDeviation = element as SeparatedDeviation;
                Write(builder, seperatedDeviation);

                var container = element as SyntacticalElementContainer;
                if (container != null)
                {
                    Write(builder, container.SyntacticalElements);
                }
            }
        }

        #region Write Automatic Flow Elements

        public void Write(StringBuilder builder, IntegratedDetour detour)
        {
            if (builder == null || detour == null)
                return;

            Write(builder, detour.IndentLevel);
            LexicalRenderer.Write(builder, detour.IntegratedDetourToken);
            LexicalRenderer.Write(builder, detour.SpaceToken);
            LexicalRenderer.Write(builder, detour.FlowTargetToken);

            builder.Append(EndLine.Pattern);
        }

        public void Write(StringBuilder builder, IntegratedDeviation deviation)
        {
            if (builder == null || deviation == null)
                return;

            Write(builder, deviation.IndentLevel);
            LexicalRenderer.Write(builder, deviation.IntegratedDeviationToken);
            LexicalRenderer.Write(builder, deviation.SpaceToken);
            LexicalRenderer.Write(builder, deviation.FlowTargetToken);

            builder.Append(EndLine.Pattern);
        }

        public void Write(StringBuilder builder, SeparatedDetour detour)
        {
            if (builder == null || detour == null)
                return;

            Write(builder, detour.IndentLevel);
            LexicalRenderer.Write(builder, detour.SeparatedDetourToken);
            LexicalRenderer.Write(builder, detour.SpaceToken);
            LexicalRenderer.Write(builder, detour.FlowTargetToken);

            builder.Append(EndLine.Pattern);
        }

        public void Write(StringBuilder builder, SeparatedDeviation deviation)
        {
            if (builder == null || deviation == null)
                return;

            Write(builder, deviation.IndentLevel);
            LexicalRenderer.Write(builder, deviation.SeparatedDeviationToken);
            LexicalRenderer.Write(builder, deviation.SpaceToken);
            LexicalRenderer.Write(builder, deviation.FlowTargetToken);

            builder.Append(EndLine.Pattern);
        }

        #endregion Write Code Elements

        #region Write Code Elements

        public void Write(StringBuilder builder, DefiningCodeBlock definingCodeBlock)
        {
            if (builder == null || definingCodeBlock == null)
                return;

            builder.Append(definingCodeBlock.CodeBlockStartToken);
            builder.Append(definingCodeBlock.DefiningCodeBlockToken);
            builder.Append(EndLine.Pattern);
            builder.Append(definingCodeBlock.TextContent);
            builder.Append(EndLine.Pattern);
            builder.Append(definingCodeBlock.CodeBlockEndToken);
            builder.Append(EndLine.Pattern);
        }

        #endregion Write Code Elements

        #region Write Conditional

        public void Write(StringBuilder builder, Conditional conditional)
        {
            if (builder == null || conditional == null)
                return;


            var singularConditional = conditional as SingularConditional;
            Write(builder, singularConditional);

            var multipleConditional = conditional as MultipleConditional;
            Write(builder, multipleConditional);
        }

        public void Write(StringBuilder builder, SingularConditional singularConditional)
        {
            if (builder == null || singularConditional == null)
                return;


            var singularConditionedTranscriber = singularConditional as SingularConditionedTranscriber;
            Write(builder, singularConditionedTranscriber);

            var singularConditionedActivator = singularConditional as SingularConditionedActivator;
            Write(builder, singularConditionedActivator);

            var menuChoiceSingularConditional = singularConditional as MenuChoiceSingularConditional;
            Write(builder, menuChoiceSingularConditional);

            var inlineSingularConditional = singularConditional as InlineSingularConditional;
            Write(builder, inlineSingularConditional);

            var defaultedSingularConditional = singularConditional as DefaultedSingularConditional;
            Write(builder, defaultedSingularConditional);

            var codeSpanInlineSingularConditional = singularConditional as CodeSpanInlineSingularConditional;
            Write(builder, codeSpanInlineSingularConditional);
            if (
                singularConditionedTranscriber == null &&
                singularConditionedActivator == null &&
                menuChoiceSingularConditional == null &&
                inlineSingularConditional == null &&
                defaultedSingularConditional == null &&
                codeSpanInlineSingularConditional == null 
                )
            {
                if (singularConditional.Condition != null)
                {
                    builder.Append(singularConditional.Condition.TextContent);
                }
                LexicalRenderer.Write(builder, singularConditional.SpaceTokenBeforeConditionToken);
                LexicalRenderer.Write(builder, singularConditional.ConditionToken);
                LexicalRenderer.Write(builder, singularConditional.SpaceTokenAfterConditionToken);
                foreach (var caseElement in singularConditional.CaseSyntacticalElements)
                {
                    var actionDescription = caseElement as ActionDescription;
                    Write(builder, actionDescription);
                }

                LexicalRenderer.Write(builder, singularConditional.SpaceTokenBeforeTerminatingConditionToken);
                LexicalRenderer.Write(builder, singularConditional.TerminatingConditionToken);
            }
        }

        public void Write(StringBuilder builder, SingularConditionedTranscriber singularConditionedTranscriber)
        {
            if (builder == null || singularConditionedTranscriber == null)
                return;


        }

        public void Write(StringBuilder builder, SingularConditionedActivator singularConditionedActivator)
        {
            if (builder == null || singularConditionedActivator == null)
                return;


        }

        public void Write(StringBuilder builder, MenuChoiceSingularConditional menuChoiceSingularConditional)
        {
            if (builder == null || menuChoiceSingularConditional == null)
                return;

            menuChoiceSingularConditional.ToString();
        }

        public void Write(StringBuilder builder, InlineSingularConditional inlineSingularConditional)
        {
            if (builder == null || inlineSingularConditional == null)
                return;

            if (inlineSingularConditional.Condition != null)
            {
                builder.Append(inlineSingularConditional.Condition.TextContent);
            }
            LexicalRenderer.Write(builder, inlineSingularConditional.SpaceTokenBeforeConditionToken);
            LexicalRenderer.Write(builder, inlineSingularConditional.ConditionToken);
            LexicalRenderer.Write(builder, inlineSingularConditional.SpaceTokenAfterConditionToken);
            foreach (var caseElement in inlineSingularConditional.CaseSyntacticalElements)
            {
                var actionDescription = caseElement as ActionDescription;
                Write(builder, actionDescription);
            }

            LexicalRenderer.Write(builder, inlineSingularConditional.SpaceTokenBeforeTerminatingConditionToken);
            LexicalRenderer.Write(builder, inlineSingularConditional.TerminatingConditionToken);

        }

        public void Write(StringBuilder builder, DefaultedSingularConditional defaultedSingularConditional)
        {
            if (builder == null || defaultedSingularConditional == null)
                return;


            if (defaultedSingularConditional.Condition != null)
            {
                builder.Append(defaultedSingularConditional.Condition.TextContent);
            }
            LexicalRenderer.Write(builder, defaultedSingularConditional.SpaceTokenBeforeConditionToken);
            LexicalRenderer.Write(builder, defaultedSingularConditional.ConditionToken);
            LexicalRenderer.Write(builder, defaultedSingularConditional.SpaceTokenAfterConditionToken);
            foreach (var caseElement in defaultedSingularConditional.CaseSyntacticalElements)
            {
                var actionDescription = caseElement as ActionDescription;
                Write(builder, actionDescription);
            }

            LexicalRenderer.Write(builder, defaultedSingularConditional.SpaceTokenBeforeConditionDefaultToken);
            LexicalRenderer.Write(builder, defaultedSingularConditional.ConditionDefaultToken);
            LexicalRenderer.Write(builder, defaultedSingularConditional.SpaceTokenAfterConditionDefaultToken);

            foreach (var caseElement in defaultedSingularConditional.DefaultSyntacticalElements)
            {
                var actionDescription = caseElement as ActionDescription;
                Write(builder, actionDescription);
            }

            LexicalRenderer.Write(builder, defaultedSingularConditional.SpaceTokenBeforeTerminatingConditionToken);
            LexicalRenderer.Write(builder, defaultedSingularConditional.TerminatingConditionToken);


            //var inlineDefaultedSingularConditional = defaultedSingularConditional as InlineDefaultedSingularConditional;
            //Write(builder, inlineDefaultedSingularConditional);

            //var defaultedSingularConditionalTranscriber = defaultedSingularConditional as DefaultedSingularConditionalTranscriber;
            //Write(builder, defaultedSingularConditionalTranscriber);

            //var codeSpanInlineDefaultSingularConditional = defaultedSingularConditional as CodeSpanInlineDefaultSingularConditional;
            //Write(builder, codeSpanInlineDefaultSingularConditional);
        }

        public void Write(StringBuilder builder, InlineDefaultedSingularConditional inlineDefaultedSingularConditional)
        {
            if (builder == null || inlineDefaultedSingularConditional == null)
                return;


        }

        public void Write(StringBuilder builder, DefaultedSingularConditionalTranscriber defaultedSingularConditionalTranscriber)
        {
            if (builder == null || defaultedSingularConditionalTranscriber == null)
                return;


        }

        public void Write(StringBuilder builder, CodeSpanInlineDefaultSingularConditional codeSpanInlineDefaultSingularConditional)
        {
            if (builder == null || codeSpanInlineDefaultSingularConditional == null)
                return;


        }

        public void Write(StringBuilder builder, CodeSpanInlineSingularConditional codeSpanInlineSingularConditional)
        {
            if (builder == null || codeSpanInlineSingularConditional == null)
                return;


        }


        public void Write(StringBuilder builder, MultipleConditional multipleConditional)
        {
            if (builder == null || multipleConditional == null)
                return;


            var defaultedMultipleConditional = multipleConditional as DefaultedMultipleConditional;
            Write(builder, defaultedMultipleConditional);

            var matchingMultipleConditional = multipleConditional as MatchingMultipleConditional;
            Write(builder, matchingMultipleConditional);
        }
        
        public void Write(StringBuilder builder, MatchingMultipleConditional matchingMultipleConditional)
        {
            if (builder == null || matchingMultipleConditional == null)
                return;


            var defaultedMatchingMultipleConditional = matchingMultipleConditional as DefaultedMatchingMultipleConditional;
            if(defaultedMatchingMultipleConditional != null)
                Write(builder, defaultedMatchingMultipleConditional);
            else
            {
                builder.Append(matchingMultipleConditional.Expression);

                LexicalRenderer.Write(builder, matchingMultipleConditional.SpaceTokenBeforeConditionToken);
                LexicalRenderer.Write(builder, matchingMultipleConditional.ConditionToken);
                LexicalRenderer.Write(builder, matchingMultipleConditional.SpaceTokenAfterConditionToken);

                builder.Append(matchingMultipleConditional.TextContent);

                LexicalRenderer.Write(builder, matchingMultipleConditional.SpaceTokenBeforeTerminatingConditionToken);
                LexicalRenderer.Write(builder, matchingMultipleConditional.TerminatingConditionToken);
            }

        }

        public void Write(StringBuilder builder, DefaultedMatchingMultipleConditional defaultedMatchingMultipleConditional)
        {
            if (builder == null || defaultedMatchingMultipleConditional == null)
                return;


            builder.Append(defaultedMatchingMultipleConditional.Expression);

            LexicalRenderer.Write(builder, defaultedMatchingMultipleConditional.SpaceTokenBeforeConditionToken);
            LexicalRenderer.Write(builder, defaultedMatchingMultipleConditional.ConditionToken);
            LexicalRenderer.Write(builder, defaultedMatchingMultipleConditional.SpaceTokenAfterConditionToken);

            builder.Append(defaultedMatchingMultipleConditional.TextContent);

            LexicalRenderer.Write(builder, defaultedMatchingMultipleConditional.SpaceTokenBeforeConditionDefaultToken);
            LexicalRenderer.Write(builder, defaultedMatchingMultipleConditional.ConditionDefaultToken);
            LexicalRenderer.Write(builder, defaultedMatchingMultipleConditional.SpaceTokenAfterConditionDefaultToken);

            builder.Append(defaultedMatchingMultipleConditional.TextContent);

            LexicalRenderer.Write(builder, defaultedMatchingMultipleConditional.SpaceTokenBeforeTerminatingConditionToken);
            LexicalRenderer.Write(builder, defaultedMatchingMultipleConditional.TerminatingConditionToken);
        }

        public void Write(StringBuilder builder, DefaultedMultipleConditional defaultedMultipleConditional)
        {
            if (builder == null || defaultedMultipleConditional == null)
                return;


        }

        #endregion Write Conditional


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

                var arrangement = element as Arrangement;
                Write(builder, arrangement);

                var conditional = element as Conditional;
                Write(builder, conditional);

                var containerBlock = element as ContainerBlock;
                Write(builder, containerBlock);

                var separatedDetour = element as SeparatedDetour;
                Write(builder, separatedDetour);

                var seperatedDeviation = element as SeparatedDeviation;
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


            var persistentMenuChoice = menuChoice as PersistentMenuChoice;
            Write(builder, persistentMenuChoice);

            var ConsumableMenuChoice = menuChoice as ConsumableMenuChoice;
            Write(builder, ConsumableMenuChoice);

            var ContinuingMenuChoice = menuChoice as ContinuingMenuChoice;
            Write(builder, ContinuingMenuChoice);
        }
        public void Write(StringBuilder builder, PersistentMenuChoice menuChoice)
        {
            if (builder == null || menuChoice == null)
                return;

            Write(builder, menuChoice.IndentLevel);

            LexicalRenderer.Write(builder, menuChoice.MenuChoiceToken);

            LexicalRenderer.Write(builder, menuChoice.SpaceToken);
            builder.Append(menuChoice.Description);
            Write(builder, menuChoice.EndLine);

            WriteContainerBlockElements(builder, menuChoice.SyntacticalElements);
        }

        public void Write(StringBuilder builder, ConsumableMenuChoice menuChoice)
        {
            if (builder == null || menuChoice == null)
                return;

            Write(builder, menuChoice.IndentLevel);

            LexicalRenderer.Write(builder, menuChoice.MenuChoiceToken);

            LexicalRenderer.Write(builder, menuChoice.SpaceToken);
            builder.Append(menuChoice.Description);
            Write(builder, menuChoice.EndLine);

            WriteContainerBlockElements(builder, menuChoice.SyntacticalElements);
        }
        public void Write(StringBuilder builder, ContinuingMenuChoice menuChoice)
        {
            if (builder == null || menuChoice == null)
                return;

            Write(builder, menuChoice.IndentLevel);

            LexicalRenderer.Write(builder, menuChoice.MenuChoiceToken);

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
            bool first = true;
            foreach (var act in acts)
            {
                if (first == false)
                    Write(builder, new SubsectionsSeparatorToken());

                Write(builder, act);

                first = false;
            }
        }
        public void Write(StringBuilder builder, List<Sequence> sequences)
        {
            bool first = true;
            foreach (var sequence in sequences)
            {
                if (first == false)
                    Write(builder, new SubsectionsSeparatorToken());

                Write(builder, sequence);

                first = false;
            }
        }
        public void Write(StringBuilder builder, List<Scene> scenes)
        {
            bool first = true;
            foreach (var scene in scenes)
            {
                if (first == false)
                    Write(builder, new SubsectionsSeparatorToken());

                Write(builder, scene);

                first = false;
            }
        }
        public void Write(StringBuilder builder, List<Moment> moments)
        {
            bool first = true;
            foreach (var moment in moments)
            {
                if (first == false)
                    Write(builder, new SubsectionsSeparatorToken());

                Write(builder, moment);

                first = false;
            }
        }
        public void Write(StringBuilder builder, List<Slice> slices)
        {
            bool first = true;
            foreach (var slice in slices)
            {
                if (first == false)
                    Write(builder, new SubsectionsSeparatorToken());

                Write(builder, slice);

                first = false;
            }
        }
        public void Write(StringBuilder builder, List<MicroSlice> microSlices)
        {
            bool first = true;
            foreach (var microSlice in microSlices)
            {
                if (first == false)
                    Write(builder, new SubsectionsSeparatorToken());

                Write(builder, microSlice);

                first = false;
            }
        }
        public void Write(StringBuilder builder, List<NanoSlice> nanoSlices)
        {
            bool first = true;
            foreach (var nanoSlice in nanoSlices)
            {
                if (first == false)
                    Write(builder, new SubsectionsSeparatorToken());

                Write(builder, nanoSlice);

                first = false;
            }
        }

        #endregion Write Section Lists

        public void Write(StringBuilder builder, SubsectionsSeparatorToken elementSubsectionSeparatorToken)
        {
            if (elementSubsectionSeparatorToken == null)
                return;
            
            builder.Append(SubsectionsSeparatorToken.Pattern);   
        }

        public void Write(StringBuilder builder, Act act)
        {
            if (builder == null || act == null)
                return;

            LexicalRenderer.Write(builder, act.ActStartToken);
            LexicalRenderer.Write(builder, act.SpaceToken);
            builder.Append(act.SectionName);
            Write(builder, act.EndLine);

            Write(builder, act.SyntacticalElements);

            if(act.HasSubsection)
                Write(builder, act.SubsectionsSeparatorToken);

            // Write from small to large because equal or bigger sections can not be contained by equal or smaller ones.
            Write(builder, act.NanoSlices);
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


            if (sequence.HasSubsection)
                Write(builder, sequence.SubsectionsSeparatorToken);

            // Write from small to large because equal or bigger sections can not be contained by equal or smaller ones.
            Write(builder, sequence.NanoSlices);
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

            if (scene.HasSubsection)
                Write(builder, scene.SubsectionsSeparatorToken);

            // Write from small to large because equal or bigger sections can not be contained by equal or smaller ones.
            Write(builder, scene.NanoSlices);
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

            if (moment.HasSubsection)
                Write(builder, moment.SubsectionsSeparatorToken);

            // Write from small to large because equal or bigger sections can not be contained by equal or smaller ones.
            Write(builder, moment.NanoSlices);
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

            if (slice.HasSubsection)
                Write(builder, slice.SubsectionsSeparatorToken);

            // Write from small to large because equal or bigger sections can not be contained by equal or smaller ones.
            Write(builder, slice.NanoSlices);
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

            if (microSlice.HasSubsection)
                Write(builder, microSlice.SubsectionsSeparatorToken);

            // Write from small to large because equal or bigger sections can not be contained by equal or smaller ones.
            Write(builder, microSlice.NanoSlices);
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

        #region Write Set

        public void Write(StringBuilder builder, Arrangement arrangement)
        {
            if (builder == null || arrangement == null)
                return;

            LexicalRenderer.Write(builder, arrangement.ObtainerToken);
            if(arrangement is LoopingLinearArrangment)
            {
                builder.Append("*-");
            }
            if (arrangement is LoopingRandomArrangment)
            {
                builder.Append("*~");
            }
            if (arrangement is EmptyingLinearArrangment)
            {
                builder.Append("!-");
            }
            if (arrangement is HangingLinearArrangment)
            {
                builder.Append("'-");
            }
            bool first = true;
            foreach(var caseArrangment in arrangement.Cases)
            {
                if(!first)
                    builder.Append("|");

                Write(builder, caseArrangment.CaseSyntacticalElements);

                first = false;
            }
            builder.Append(arrangement.TerminatingArrangementToken);
        }

        #endregion Write Set

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
