using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;
using FountainExponential.LanguageStructures.Lexical.InteractiveFlow;
using FountainExponential.LanguageStructures.Syntactical;
using FountainExponential.LanguageStructures.Syntactical.FountainElement;

namespace FountainExponential.LanguageStructures.Syntactical.InteractiveFlow
{
    public class MenuChoice : SyntacticalElementContainer, ISyntacticalElementable
    {
        public IndentLevel IndentLevel { get; set; }

        public MenuChoiceToken MenuChoiceToken { get; set; }
        public SpaceToken SpaceToken { get; set; }

        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        public string Description { get; set; }
        public EndLine EndLine { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(MenuChoiceToken);
            builder.Append(Description);
            return builder.ToString();
        }
    }
}
