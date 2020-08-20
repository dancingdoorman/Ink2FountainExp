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

        //
        // Because a section can only be closed by a equal or bigger section we must order them from small to bigger.
        // A section can only contain sections that are smaller then itself.
        //

        public List<NanoSlice> NanoSlice { get; set; } = new List<NanoSlice>();

        public override string ToString()
        {
            return MicroSliceStartToken + SectionName;
        }
    }
}
