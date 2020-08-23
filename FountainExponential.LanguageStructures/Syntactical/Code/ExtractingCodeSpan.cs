using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical.Code;
using FountainExponential.LanguageStructures.Syntactical;

namespace FountainExponential.LanguageStructures.Syntactical.Code
{
    public class ExtractingCodeSpan : CodeSpan, ISyntacticalElementable
    {
        public ExtractingCodeSpanToken ExtractingCodeSpanToken { get; set; }
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(CodeSpanStartToken);
            builder.Append(ExtractingCodeSpanToken);
            builder.Append(CodeSpanEndToken);
            return builder.ToString();
        }
    }
}
