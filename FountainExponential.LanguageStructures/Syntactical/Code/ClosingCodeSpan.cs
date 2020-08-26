using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical.Code;
using FountainExponential.LanguageStructures.Syntactical;

namespace FountainExponential.LanguageStructures.Syntactical.Code
{
    /// <summary>The ClosingCodeSpan class encapsulates a code span that marks the end of a block to the code defined in the ConditioningCodeSpan probably with a } semicolon.</summary>
    public class ClosingCodeSpan : CodeSpan, ISyntacticalElementable
    {        
        public ClosingCodeToken ClosingCodeToken { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(CodeSpanStartToken);
            builder.Append(ClosingCodeToken);
            builder.Append(CodeSpanEndToken);
            return builder.ToString();
        }
    }
}
