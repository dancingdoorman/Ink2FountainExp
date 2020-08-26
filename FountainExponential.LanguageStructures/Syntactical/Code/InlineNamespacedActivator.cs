using FountainExponential.LanguageStructures.Lexical.Code;
using System;
using System.Collections.Generic;
using System.Text;

namespace FountainExponential.LanguageStructures.Syntactical.Code
{
    public class InlineNamespacedActivator : InlineNamespace, ISyntacticalElementable
    {
        public ActivatorToken ActivatorToken { get; set; }
    }
}
