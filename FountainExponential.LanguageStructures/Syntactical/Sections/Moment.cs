using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical.Sections;
using FountainExponential.LanguageStructures.Syntactical;

namespace FountainExponential.LanguageStructures.Syntactical.Sections
{
    public class Moment : SectionBase, ISyntacticalElementable
    {
        public MomentToken MomentStartToken { get; set; }
        public override string ToString()
        {
            return MomentStartToken + SectionName;
        }
    }
}
