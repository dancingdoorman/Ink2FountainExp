using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;

namespace FountainExponential.LanguageStructures.Lexical.Code
{
    public class NamespaceToken : ILexicalElementable
    {
        public const string Sign = "%";
        public const string Keyword = Sign;//"%";
        public override string ToString()
        {
            return Sign;
        }
    }
}
