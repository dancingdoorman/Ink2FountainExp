﻿using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;

namespace FountainExponential.LanguageStructures.Lexical.InteractiveFlow
{
    public class LabelTitleToken : ILexicalElementable
    {
        public string Title { get; set; }
        public override string ToString()
        {
            return Title;
        }
    }
}
