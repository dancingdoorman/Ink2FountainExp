using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;
using FountainExponential.LanguageStructures.Lexical.InteractiveFlow;
using FountainExponential.LanguageStructures.Syntactical;

namespace FountainExponential.LanguageStructures.Syntactical.InteractiveFlow
{
    public class Menu : ISyntacticalElementable
    {
        /// <summary>Gets or sets the text content of the section.</summary>
        /// <value>The text content of the section.</value>
        public string TextContent { get; set; }

        /// <summary>Gets or sets the section lexical elements.</summary>
        /// <value>The section lexical elements.</value>
        public List<ILexicalElementable> LexicalElements { get; set; } = new List<ILexicalElementable>();

        /// <summary>Gets or sets the choices.</summary>
        /// <value>The choices.</value>
        public List<MenuChoice> Choices { get; set; } = new List<MenuChoice>();

        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var choice in Choices)
            {
                builder.Append(choice);
            }
            return builder.ToString();
        }
    }
}
