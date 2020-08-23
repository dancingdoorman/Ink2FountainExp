using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical.Code;
using FountainExponential.LanguageStructures.Syntactical;

namespace FountainExponential.LanguageStructures.Syntactical.Code
{
    /// <summary>The AlternativeCodeSpan class encapsulates a code span that marks an alternative to the condition defined in the ConditioningCodeSpan</summary>
    public class AlternativeCodeSpan : CodeSpan, ISyntacticalElementable
    { 
        
        public AlternativeCodeSpanToken AlternativeCodeSpanToken { get; set; }
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(CodeSpanStartToken);
            builder.Append(AlternativeCodeSpanToken);
            builder.Append(CodeSpanEndToken);
            return builder.ToString();
        }
    }
}
