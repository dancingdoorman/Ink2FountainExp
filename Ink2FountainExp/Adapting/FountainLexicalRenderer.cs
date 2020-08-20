using FountainExponential.LanguageStructures.Lexical;
using FountainExponential.LanguageStructures.Lexical.AutomaticFlow;
using FountainExponential.LanguageStructures.Lexical.InteractiveFlow;
using FountainExponential.LanguageStructures.Lexical.MetaData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ink.Ink2FountainExp.Adapting
{
    /// <summary>The FountainLexicalRenderer encapsulates the rendering of lexical tokens.</summary>
    public class FountainLexicalRenderer
    {
        public void Write(StringBuilder builder, KeyValuePairKeyToken key)
        {
            if (builder == null || key == null)
                return;

            builder.Append(key.Keyword);
        }
                    
        public void Write(StringBuilder builder, StickyMenuChoiceToken choice)
        {
            if (builder == null || choice == null)
                return;

            builder.Append(StickyMenuChoiceToken.Sign);
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

        public void Write(StringBuilder builder, SeperatedDeviationToken deviation)
        {
            if (builder == null || deviation == null)
                return;

            builder.Append(SeperatedDeviationToken.Keyword);
        }
        
        public void Write(StringBuilder builder, SpaceToken spaceToken)
        {
            if (builder == null || spaceToken == null)
                return;

            // can be multiple spaces or a tab depending on the situation.
            builder.Append(" ");
        }

        public void Write(StringBuilder builder, KeyValuePairAssignmentToken assignmentToken)
        {
            if (builder == null || assignmentToken == null)
                return;

            builder.Append(KeyValuePairAssignmentToken.Sign);
        }

    }
}
