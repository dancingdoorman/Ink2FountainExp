using FountainExponential.LanguageStructures.Lexical;
using System;
using System.Collections.Generic;
using System.Text;

namespace FountainExponential.LanguageStructures.Syntactical
{
    public class SyntacticalElementBase : ISyntacticalElementable
    {
        /// <summary>Gets or sets the text content of the section.</summary>
        /// <value>The text content of the section.</value>
        public string TextContent { get; set; }

        /// <summary>Gets or sets the section lexical elements.</summary>
        /// <value>The section lexical elements.</value>
        public List<ILexicalElementable> LexicalElements { get; set; } = new List<ILexicalElementable>();
    }
}
