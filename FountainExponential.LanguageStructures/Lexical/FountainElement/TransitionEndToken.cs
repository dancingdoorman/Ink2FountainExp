﻿using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;

namespace FountainExponential.LanguageStructures.Lexical.FountainElement
{
    public class TransitionEndToken : ILexicalElementable
    {
        public const string Keyword = "TO:";
        public override string ToString()
        {
            return Keyword;
        }
    }
}
