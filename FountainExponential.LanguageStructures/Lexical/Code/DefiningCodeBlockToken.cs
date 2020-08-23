using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;

namespace FountainExponential.LanguageStructures.Lexical.Code
{
    public class DefiningCodeBlockToken : ILexicalElementable
    {
        public const string Sign = "+";
        public const string Keyword = CodeBlockStartToken.Keyword + Sign;//"```+";
        public override string ToString()
        {
            return Keyword;
        }
    }
}
