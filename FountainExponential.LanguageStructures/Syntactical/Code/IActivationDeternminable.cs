using System;
using System.Collections.Generic;
using System.Text;

namespace FountainExponential.LanguageStructures.Syntactical.Code
{
    public interface IActivationDeternminable
    {
        // The visitor should handle the activating, not the object itself

        string IsActivatable { get; }
        string ActivatingFunction { get; }
    }
}
