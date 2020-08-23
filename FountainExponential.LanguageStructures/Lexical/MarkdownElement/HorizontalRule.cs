using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;

namespace FountainExponential.LanguageStructures.Lexical.MarkdownElement
{
    public class HorizontalRule : ILexicalElementable
    {
        public const string Keyword = "------"; // 6 times - is equal to code block opening and closing ---
        public override string ToString()
        {
            return Keyword;
        }
    }
}
