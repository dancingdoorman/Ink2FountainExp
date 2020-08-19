using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical.Sections;
using FountainExponential.LanguageStructures.Syntactical;

namespace FountainExponential.LanguageStructures.Syntactical.Sections
{
    public class NanoSlice : SectionBase, ISyntacticalElementable
    {
        public NanoSliceToken NanoSliceStartToken { get; set; }
        public override string ToString()
        {
            return NanoSliceStartToken + SectionName;
        }
    }
}
