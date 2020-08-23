using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;

namespace FountainExponential.LanguageStructures.Lexical.Emphasis
{
    public class UnderlinedTextEndToken : ILexicalElementable
    {
        public const string Sign = "_";
        public const string Keyword = "_";
        public override string ToString()
        {
            return Keyword;
        }
    }
}
