using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical.Sections;
using FountainExponential.LanguageStructures.Syntactical;

namespace FountainExponential.LanguageStructures.Syntactical.Sections
{
    public class Slice : SectionBase, ISyntacticalElementable
    {
        public SliceToken SliceStartToken { get; set; }
        public override string ToString()
        {
            return SliceStartToken + SectionName;
        }
    }
}
