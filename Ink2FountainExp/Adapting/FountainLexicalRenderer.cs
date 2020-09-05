using FountainExponential.LanguageStructures.Lexical;
using FountainExponential.LanguageStructures.Lexical.AutomaticFlow;
using FountainExponential.LanguageStructures.Lexical.Code;
using FountainExponential.LanguageStructures.Lexical.Conditional;
using FountainExponential.LanguageStructures.Lexical.InteractiveFlow;
using FountainExponential.LanguageStructures.Lexical.MetaData;
using FountainExponential.LanguageStructures.Lexical.Sections;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ink.Ink2FountainExp.Adapting
{
    /// <summary>The FountainLexicalRenderer encapsulates the rendering of lexical tokens.</summary>
    public class FountainLexicalRenderer
    {
        #region Write Automatic Flow Tokens

        public void Write(StringBuilder builder, AutomaticFlowTargetToken target)
        {
            if (builder == null || target == null)
                return;

            builder.Append(target.Label);
        }

        public void Write(StringBuilder builder, IntegratedDetourToken detour)
        {
            if (builder == null || detour == null)
                return;

            builder.Append(IntegratedDetourToken.Keyword);
        }

        public void Write(StringBuilder builder, IntegratedDeviationToken deviation)
        {
            if (builder == null || deviation == null)
                return;

            builder.Append(IntegratedDeviationToken.Keyword);
        }

        public void Write(StringBuilder builder, SeparatedDetourToken detour)
        {
            if (builder == null || detour == null)
                return;

            builder.Append(SeparatedDetourToken.Keyword);
        }

        public void Write(StringBuilder builder, SeparatedDeviationToken deviation)
        {
            if (builder == null || deviation == null)
                return;

            builder.Append(SeparatedDeviationToken.Keyword);
        }

        #endregion Write Automatic Flow

        #region Write Code Tokens

        public void Write(StringBuilder builder, ObtainerToken obtainerToken)
        {
            if (builder == null || obtainerToken == null)
                return;

            builder.Append(ObtainerToken.Keyword);
        }

        #endregion Write Code Tokens

        #region Write Comment Tokens

        #endregion Write Comment Tokens

        #region Write Conditional Tokens
        public void Write(StringBuilder builder, ConditionToken target)
        {
            if (builder == null || target == null)
                return;

            builder.Append(ConditionToken.Keyword);
        }
        public void Write(StringBuilder builder, ConditionDefaultToken target)
        {
            if (builder == null || target == null)
                return;

            builder.Append(ConditionDefaultToken.Keyword);
        }

        public void Write(StringBuilder builder, TerminatingConditionToken target)
        {
            if (builder == null || target == null)
                return;

            builder.Append(TerminatingConditionToken.Keyword);
        }

        #endregion Write Conditional Tokens

        #region Write Data Tokens

        #endregion Write Data Tokens

        #region Write Emphasis Tokens

        #endregion Write Emphasis Tokens

        #region Write FountainElement Tokens

        #endregion Write FountainElement Tokens

        #region Write Interactive Flow Tokens

        public void Write(StringBuilder builder, PersistentMenuChoiceToken choice)
        {
            if (builder == null || choice == null)
                return;

            builder.Append(PersistentMenuChoiceToken.Sign);
        }

        public void Write(StringBuilder builder, ConsumableMenuChoiceToken choice)
        {
            if (builder == null || choice == null)
                return;

            builder.Append(ConsumableMenuChoiceToken.Sign);
        }

        public void Write(StringBuilder builder, ContinuingMenuChoiceToken choice)
        {
            if (builder == null || choice == null)
                return;

            builder.Append(ContinuingMenuChoiceToken.Sign);
        }

        public void Write(StringBuilder builder, InteractiveFlowTargetToken target)
        {
            if (builder == null || target == null)
                return;

            builder.Append(target.Label);
        }

        #endregion Write Interactive Flow Tokens

        #region Write MarkdownElement Tokens

        #endregion Write MarkdownElement Tokens

        #region Write MetaData Tokens

        public void Write(StringBuilder builder, KeyValuePairKeyToken key)
        {
            if (builder == null || key == null)
                return;

            builder.Append(key.Keyword);
        }

        public void Write(StringBuilder builder, KeyValuePairAssignmentToken assignmentToken)
        {
            if (builder == null || assignmentToken == null)
                return;

            builder.Append(KeyValuePairAssignmentToken.Sign);
        }

        #endregion Write MetaData Tokens

        #region Write Section Tokens

        public void Write(StringBuilder builder, ActToken act)
        {
            if (builder == null || act == null)
                return;

            builder.Append(ActToken.Keyword);
        }

        public void Write(StringBuilder builder, SequenceToken sequence)
        {
            if (builder == null || sequence == null)
                return;

            builder.Append(SequenceToken.Keyword);
        }

        public void Write(StringBuilder builder, SceneToken scene)
        {
            if (builder == null || scene == null)
                return;

            builder.Append(SceneToken.Keyword);
        }

        public void Write(StringBuilder builder, MomentToken moment)
        {
            if (builder == null || moment == null)
                return;

            builder.Append(MomentToken.Keyword);
        }

        public void Write(StringBuilder builder, SliceToken slice)
        {
            if (builder == null || slice == null)
                return;

            builder.Append(SliceToken.Keyword);
        }

        public void Write(StringBuilder builder, MicroSliceToken microSlice)
        {
            if (builder == null || microSlice == null)
                return;

            builder.Append(MicroSliceToken.Keyword);
        }

        public void Write(StringBuilder builder, NanoSliceToken nanoSlice)
        {
            if (builder == null || nanoSlice == null)
                return;

            builder.Append(NanoSliceToken.Keyword);
        }

        #endregion Write Section Tokens 

        #region Write Basic Tokens

        public void Write(StringBuilder builder, SpaceToken spaceToken)
        {
            if (builder == null || spaceToken == null)
                return;

            // can be multiple spaces or a tab depending on the situation.
            builder.Append(" ");
        }

        #endregion Write Basic Tokens
    }
}
