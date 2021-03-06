﻿using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;

namespace FountainExponential.LanguageStructures.Lexical.Code
{
    public class ExtractingCodeSpanToken : ILexicalElementable
    {
        public const string Sign = "=";
        public const string Keyword = Sign + CodeSpanEndToken.Keyword; //"=`"; // At the back of the span.
        public override string ToString()
        {
            return Sign;
        }
    }
}
