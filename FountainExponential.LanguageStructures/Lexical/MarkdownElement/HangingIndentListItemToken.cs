using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;

namespace FountainExponential.LanguageStructures.Lexical.MarkdownElement
{
    public class HangingIndentListItemToken : ILexicalElementable
    {
        public const string Sign = "\t";
        public const string Keyword = "\t>";
        public override string ToString()
        {
            return Keyword;
        }
    }
}
