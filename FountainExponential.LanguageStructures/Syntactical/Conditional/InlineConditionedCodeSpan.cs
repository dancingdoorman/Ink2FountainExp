using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;
using FountainExponential.LanguageStructures.Lexical.Code;
using FountainExponential.LanguageStructures.Lexical.Conditional;
using FountainExponential.LanguageStructures.Syntactical;
using FountainExponential.LanguageStructures.Syntactical.Code;

namespace FountainExponential.LanguageStructures.Syntactical.Conditional
{
    public class InlineConditionedCodeSpan : CodeSpan, ISyntacticalElementable, IConditionable, IConditionEvaluatable, ICodeSpanEnclosable, ICodeConditionEnclosable
    {
        public ConditionToken ConditioningToken { get; set; }

        public Condition Condition { get; set; }

        public ConditionOpenToken ConditionOpenToken { get; set; }

        public ConditionCloseToken ConditionCloseToken { get; set; }

        public bool IsTrue
        {
            get
            {
                if (Condition != null)
                {
                    return Condition.IsTrue;
                }
                return true;
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(CodeSpanStartToken);

            builder.Append(CodeSpanEndToken);
            return builder.ToString();
        }

    }
}
