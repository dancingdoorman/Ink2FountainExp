using FountainExponential.LanguageStructures.Lexical.Code;
using System;
using System.Collections.Generic;
using System.Text;

namespace FountainExponential.LanguageStructures.Syntactical.Code
{
    public class InlineActivator : ISyntacticalElementable, IActivationDeternminable
    {        
        public ActivatorToken ActivatorToken { get; set; }

        // The visitor should handle the activating, not the object itself

        public string IsActivatable { get; }

        public string ActivatingFunction { get; }
    }
}
