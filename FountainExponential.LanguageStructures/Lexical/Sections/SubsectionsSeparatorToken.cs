using System;
using System.Collections.Generic;
using System.Text;

namespace FountainExponential.LanguageStructures.Lexical.Sections
{
    public class SubsectionsSeparatorToken
    {
        public static string Pattern = "\r\n";

        public override string ToString()
        {
            return Pattern;
        }
    }
}
