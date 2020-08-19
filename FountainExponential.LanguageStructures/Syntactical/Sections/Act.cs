using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical.Sections;
using FountainExponential.LanguageStructures.Syntactical;

namespace FountainExponential.LanguageStructures.Syntactical.Sections
{
    public class Act : SectionBase, ISyntacticalElementable
    {
        public ActToken ActStartToken { get; set; }

        public override string ToString()
        {
            return ActStartToken + SectionName;
        }

    }
}
