using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical.Sections;
using FountainExponential.LanguageStructures.Syntactical;

namespace FountainExponential.LanguageStructures.Syntactical.Sections
{
    public class MicroSlice : SectionBase, ISyntacticalElementable
    {
        public MicroSliceToken MicroSliceStartToken { get; set; }
        public override string ToString()
        {
            return MicroSliceStartToken + SectionName;
        }
    }
}
