using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;
using FountainExponential.LanguageStructures.Lexical.InteractiveFlow;
using FountainExponential.LanguageStructures.Syntactical;
using FountainExponential.LanguageStructures.Syntactical.FountainElement;

namespace FountainExponential.LanguageStructures.Syntactical.InteractiveFlow
{
    public class ContainerBlock : ISyntacticalElementable
    {
        /// <summary>Gets or sets the text content of the section.</summary>
        /// <value>The text content of the section.</value>
        public string TextContent { get; set; }

        /// <summary>Gets or sets the section lexical elements.</summary>
        /// <value>The section lexical elements.</value>
        public List<ILexicalElementable> LexicalElements { get; set; } = new List<ILexicalElementable>();

        /// <summary>Gets or sets the play syntactical elements. This list is expected to hold all the syntactical elements in the section.</summary>
        /// <value>The section syntactical elements.</value>
        public List<ISyntacticalElementable> SyntacticalElements { get; set; } = new List<ISyntacticalElementable>();

        public ContainerBlockToken StartMenuChoiceToken { get; set; }
        public EndLine StartEndLine { get; set; }
        public ContainerBlockToken CloseMenuChoiceToken { get; set; }
        public EndLine CloseEndLine { get; set; }
        public IndentLevel IndentLevel { get; set; }


        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(StartMenuChoiceToken);
            foreach(var element in SyntacticalElements)
            {
                builder.Append(element);
            }
            builder.Append(CloseMenuChoiceToken);
            return builder.ToString();
        }

    }
}
