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
    /// <summary>The TerminatingCodeSpan class encapsulates a code span that marks the end of an alternative to the condition defined in the ConditioningCodeSpan probably with a ; semicolon.</summary>
    public class TerminatingCondition : CodeSpan, ISyntacticalElementable
    {
        public TerminatingConditionToken TerminatingConditionToken { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(CodeSpanStartToken);
            builder.Append(TerminatingConditionToken);
            builder.Append(CodeSpanEndToken);
            return builder.ToString();
        }
    }
}
