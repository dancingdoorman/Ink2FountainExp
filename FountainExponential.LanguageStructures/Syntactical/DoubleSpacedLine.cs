using System;
using System.Collections.Generic;
using System.Text;

namespace FountainExponential.LanguageStructures.Syntactical
{
    public class DoubleSpacedLine : ISyntacticalElementable
    {
        public static string Pattern = "  \r\n";

        public override string ToString()
        {
            return Pattern;
        }
    }
}
