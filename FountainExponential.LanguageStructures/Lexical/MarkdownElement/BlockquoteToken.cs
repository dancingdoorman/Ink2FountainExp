using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;

namespace FountainExponential.LanguageStructures.Lexical.MarkdownElement
{
    public class BlockquoteToken : ILexicalElementable
    {
        public const string Sign = ">";
        public const string Keyword = "\t>";
        public override string ToString()
        {
            return Keyword;
        }
    }
}
