using FountainExponential.LanguageStructures.Lexical;
using FountainExponential.LanguageStructures.Lexical.AutomaticFlow;
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
        #region Write MetaData

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

        #endregion Write MetaData

        #region InteractiveFlow

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

        public void Write(StringBuilder builder, FlowTargetToken target)
        {
            if (builder == null || target == null)
                return;

            builder.Append(target.Label);
        }

        #endregion InteractiveFlow

        #region Write Sections

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

        #endregion Write Sections 
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
        
        public void Write(StringBuilder builder, SpaceToken spaceToken)
        {
            if (builder == null || spaceToken == null)
                return;

            // can be multiple spaces or a tab depending on the situation.
            builder.Append(" ");
        }

    }
}
