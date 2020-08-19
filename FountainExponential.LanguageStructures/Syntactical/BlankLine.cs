using System;
using System.Collections.Generic;
using System.Text;

namespace FountainExponential.LanguageStructures.Syntactical
{
    public class BlankLine : ISyntacticalElementable
    {
        public static EndLine Pattern = new EndLine();
        public override string ToString()
        {
            return Pattern.ToString();
        }
    }
}
