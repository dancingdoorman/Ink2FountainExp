using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Syntactical;

namespace FountainExponential.LanguageStructures.Syntactical
{
    public interface ISyntacticalElementsContainable
    {
        /// <summary>Gets or sets the play syntactical elements. This list is expected to hold all the syntactical elements in the section.</summary>
        /// <value>The section syntactical elements.</value>
        List<ISyntacticalElementable> SyntacticalElements { get; set; }
    }
}
