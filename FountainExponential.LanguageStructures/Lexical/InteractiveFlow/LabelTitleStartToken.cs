﻿using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;

namespace FountainExponential.LanguageStructures.Lexical.InteractiveFlow
{
    public class LabelTitleStartToken : ILexicalElementable
    {
        public const char Sign = '"';
        public const string Keyword = "\"";
        public override string ToString()
        {
            return Keyword;
        }
    }
}