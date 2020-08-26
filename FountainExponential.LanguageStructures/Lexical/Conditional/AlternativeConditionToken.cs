using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;
using FountainExponential.LanguageStructures.Lexical.Code;

namespace FountainExponential.LanguageStructures.Lexical.Conditional
{
    public class AlternativeConditionToken : ILexicalElementable
    {
        public const string Sign = ":";
        public const string Keyword = CodeSpanStartToken.Keyword + Sign + CodeSpanEndToken.Keyword; //"`:`";
        public override string ToString()
        {
            return Sign;
        }
    }
}
