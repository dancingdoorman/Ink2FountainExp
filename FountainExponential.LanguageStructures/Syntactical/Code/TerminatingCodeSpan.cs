using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical.Code;
using FountainExponential.LanguageStructures.Syntactical;

namespace FountainExponential.LanguageStructures.Syntactical.Code
{
    /// <summary>The TerminatingCodeSpan class encapsulates a code span that marks the end of an alternative to the condition defined in the ConditioningCodeSpan</summary>
    public class TerminatingCodeSpan : CodeSpan, ISyntacticalElementable
    { 
        
        public TerminatingCodeSpanToken TerminatingCodeSpanToken { get; set; }
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(CodeSpanStartToken);
            builder.Append(TerminatingCodeSpanToken);
            builder.Append(CodeSpanEndToken);
            return builder.ToString();
        }
    }
}
