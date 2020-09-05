using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;
using FountainExponential.LanguageStructures.Lexical.Code;

namespace FountainExponential.LanguageStructures.Lexical.Conditional
{
    public class ConditionDefaultToken : ILexicalElementable
    {
        public const string Sign = ":";
        public const string Keyword = Sign; //"`:`";
        public override string ToString()
        {
            return Sign;
        }
    }
}
