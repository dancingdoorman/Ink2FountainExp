﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FountainExponential.LanguageStructures.Lexical.InteractiveFlow
{
    public class InteractiveFlowTargetToken : ITargetLabellable
    {
        public string Label { get; set; }

        public override string ToString()
        {
            return Label;
        }
    }
}
