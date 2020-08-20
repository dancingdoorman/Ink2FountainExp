using FountainExponential.LanguageStructures.Lexical;
using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical.Sections;

namespace FountainExponential.LanguageStructures.Syntactical.Sections
{
    public class SectionBase : SyntacticalElementContainer
    {
        public SpaceToken SpaceToken { get; set; }

        public string SectionName { get; set; }

        public EndLine EndLine { get; set; }
        
        public override string ToString()
        {
            return SectionName;
        }
    }
}
