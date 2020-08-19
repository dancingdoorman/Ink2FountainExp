using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Syntactical;

namespace FountainExponential.LanguageStructures.Syntactical.FountainElement
{
    public class IndentLevel : ISyntacticalElementable
    {
        public int Level { get; set; }
        public override string ToString()
        {
            return "Level" + Level;
        }
    }
}
