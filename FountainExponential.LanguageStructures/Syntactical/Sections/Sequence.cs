﻿using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical.Sections;
using FountainExponential.LanguageStructures.Syntactical;

namespace FountainExponential.LanguageStructures.Syntactical.Sections
{
    public class Sequence : SectionBase, ISyntacticalElementable
    {
        public SequenceToken SequenceStartToken { get; set; }
        public override string ToString()
        {
            return SequenceStartToken + SectionName;
        }
    }
}
