﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FountainExponential.LanguageStructures.Syntactical
{
    /// <summary>The EndLine class encapsulates the end-line that can be \r on Apple, \n on Unix/Linux or \r\n on Windows</summary>
    public class EndLine : ISyntacticalElementable, IConvergentElementable
    {
        public static string Pattern = "\r\n";

        public override string ToString()
        {
            return Pattern;
        }
    }
}
