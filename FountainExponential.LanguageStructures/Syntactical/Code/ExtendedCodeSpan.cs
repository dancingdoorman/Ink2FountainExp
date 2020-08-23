using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical.Code;
using FountainExponential.LanguageStructures.Syntactical;

namespace FountainExponential.LanguageStructures.Syntactical.Code
{
    /// <summary>The ExtendedCodeSpan class encapsulates a code span where it's statements are continuations of a previous code span</summary>
    public class ExtendedCodeSpan : CodeSpan, ISyntacticalElementable
    {
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
