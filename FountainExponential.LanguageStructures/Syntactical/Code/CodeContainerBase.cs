using FountainExponential.LanguageStructures.Lexical;
using System;
using System.Collections.Generic;
using System.Text;

namespace FountainExponential.LanguageStructures.Syntactical.Code
{
    public class CodeContainerBase : ISyntacticalElementable, ITextContentSummarizable, ILexicalElementsContainable, ISyntacticalElementsContainable
    {
        /// <summary>Gets or sets the text content of the code container.</summary>
        /// <value>The text content of the code container.</value>
        public string TextContent { get; set; }


        /// <summary>Gets or sets the code container lexical elements.</summary>
        /// <value>The code container lexical elements.</value>
        public List<ILexicalElementable> LexicalElements { get; set; } = new List<ILexicalElementable>();

        /// <summary>Gets or sets the code container syntactical elements. This list is expected to hold all the syntactical elements in the code container.</summary>
        /// <value>The code container syntactical elements.</value>
        public List<ISyntacticalElementable> SyntacticalElements { get; set; } = new List<ISyntacticalElementable>();
    }
}
