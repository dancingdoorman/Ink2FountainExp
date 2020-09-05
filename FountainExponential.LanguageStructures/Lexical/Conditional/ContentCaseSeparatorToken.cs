using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;
using FountainExponential.LanguageStructures.Lexical.Code;

namespace FountainExponential.LanguageStructures.Lexical.Conditional
{
    public class ContentCaseSeparatorToken : ILexicalElementable
    {
        // It is cleaner to do
        // $SomeValue ? 1# The first | 2# The second : Some other value ;

        // then
        // $SomeValue == 1 ? 1# The first : $SomeValue == 2 ? The second : Some other value ;

        public const string Sign = "|";
        public const string Keyword = CodeSpanStartToken.Keyword + Sign + CodeSpanEndToken.Keyword; //"`|`";
        public override string ToString()
        {
            return Sign;
        }
    }
}
