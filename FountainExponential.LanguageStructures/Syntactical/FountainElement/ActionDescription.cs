using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Syntactical;

namespace FountainExponential.LanguageStructures.Syntactical.FountainElement
{
    public class ActionDescription : ISyntacticalElementable
    {
        /// <summary>Gets or sets the text content of the action.</summary>
        /// <value>The text content of the action.</value>
        public string TextContent { get; set; }


        public EndLine EndLine { get; set; }
        public IndentLevel IndentLevel { get; set; }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(TextContent))
            {
                return TextContent;
            }
            return base.ToString();
        }
    }
}
