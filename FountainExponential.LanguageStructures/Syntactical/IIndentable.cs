using FountainExponential.LanguageStructures.Syntactical.FountainElement;
using System;
using System.Collections.Generic;
using System.Text;

namespace FountainExponential.LanguageStructures.Syntactical
{
    /// <summary>The IIndentable interface ensures a syntactical element can be indented.</summary>
    public interface IIndentable
    {
        IndentLevel IndentLevel { get; set; }
    }
}
