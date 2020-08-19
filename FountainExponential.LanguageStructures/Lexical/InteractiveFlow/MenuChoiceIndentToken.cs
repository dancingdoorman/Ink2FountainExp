using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;

namespace FountainExponential.LanguageStructures.Lexical.InteractiveFlow
{
    public class MenuChoiceIndentToken : ILexicalElementable
    {
        public const string Sign = "\t";
        public override string ToString()
        {
            return Sign.ToString();
        }
    }
}
