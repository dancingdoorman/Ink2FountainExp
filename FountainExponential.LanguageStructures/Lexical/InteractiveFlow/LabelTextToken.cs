﻿using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;

namespace FountainExponential.LanguageStructures.Lexical.InteractiveFlow
{
    public class LabelTextToken : ILexicalElementable
    {
        public string Text { get; set; }
        public override string ToString()
        {
            return Text;
        }
    }
}
