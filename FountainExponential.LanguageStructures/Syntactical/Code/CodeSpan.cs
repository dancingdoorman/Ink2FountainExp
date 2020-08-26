using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical.Code;
using FountainExponential.LanguageStructures.Syntactical;

namespace FountainExponential.LanguageStructures.Syntactical.Code
{
    public class CodeSpan : CodeContainerBase, ISyntacticalElementable, ICodeSpanEnclosable
    {
        public CodeSpanStartToken CodeSpanStartToken { get; set; }

        public CodeSpanEndToken CodeSpanEndToken { get; set; }


        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(CodeSpanStartToken);
            if (SyntacticalElements != null)
            {
                foreach (var element in SyntacticalElements)
                {
                    builder.Append(element);
                }
            }
            builder.Append(CodeSpanEndToken);
            return builder.ToString();
        }
    }
}
