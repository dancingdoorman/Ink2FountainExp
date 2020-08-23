﻿using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;

namespace FountainExponential.LanguageStructures.Lexical.InteractiveFlow
{
    public class LabelUrlToken : ILexicalElementable
    {
        public string Url { get; set; }
        public override string ToString()
        {
            return Url;
        }
    }
}
