using System;
using System.Collections.Generic;
using System.Text;

namespace FountainExponential.LanguageStructures.Syntactical
{
    public class SyntacticalElementContainer : SyntacticalElementBase
    {
        /// <summary>Gets or sets the play syntactical elements. This list is expected to hold all the syntactical elements in the section.</summary>
        /// <value>The section syntactical elements.</value>
        public List<ISyntacticalElementable> SyntacticalElements { get; set; } = new List<ISyntacticalElementable>();
    }
}
