﻿using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;

namespace FountainExponential.LanguageStructures.Lexical.Data
{
    public class AttributeSpanEndToken : ILexicalElementable
    {
        public const string Sign = "}";
        public const string Keyword = "}";
        public override string ToString()
        {
            return Keyword;
        }
    }
}
