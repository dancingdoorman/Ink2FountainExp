using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical.AutomaticFlow;
using FountainExponential.LanguageStructures.Syntactical;

namespace FountainExponential.LanguageStructures.Syntactical.AutomaticFlow
{
    public class SeparatedDetour : Detour, ISyntacticalElementable
    {
        public SeparatedDetourToken SeparatedDetourToken { get; set; }
    }
}
