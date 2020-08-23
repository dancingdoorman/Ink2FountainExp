using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;

namespace FountainExponential.LanguageStructures.Lexical.Code
{
    public class InjectingCodeSpanToken : ILexicalElementable
    {
        public const string Sign = "=";
        public const string Keyword = CodeSpanStartToken.Keyword + Sign;//"`=";
        public override string ToString()
        {
            return Sign;
        }
    }
}
