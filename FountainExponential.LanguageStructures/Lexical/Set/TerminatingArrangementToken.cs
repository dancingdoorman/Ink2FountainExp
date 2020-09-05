using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;
using FountainExponential.LanguageStructures.Lexical.Code;

namespace FountainExponential.LanguageStructures.Lexical.Set
{
    public class TerminatingArrangementToken : ILexicalElementable
    {
        // $SomeValue == 1 ? 1# The first : Some other value ;
        // $SomeValue == 1 ? 1# The first : $SomeValue == 2 ? The second : Some other value ;

        // $SomeValue ? 1# The first | 2# The second : Some other value ;

        public const string Sign = ";";
        public const string Keyword = Sign; //"`;`";
        public override string ToString()
        {
            return Sign;
        }
    }
}
