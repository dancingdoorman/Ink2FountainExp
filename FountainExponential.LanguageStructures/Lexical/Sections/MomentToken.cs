﻿using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;

namespace FountainExponential.LanguageStructures.Lexical.Sections
{
    public class MomentToken : ILexicalElementable
    {
        public const string Keyword = "####";
        public override string ToString()
        {
            return Keyword;
        }
    }
}
