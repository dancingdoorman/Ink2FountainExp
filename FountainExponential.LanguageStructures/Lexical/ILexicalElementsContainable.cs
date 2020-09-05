using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;

namespace FountainExponential.LanguageStructures.Lexical
{
    public interface ILexicalElementsContainable
    {
        /// <summary>Gets or sets the section lexical elements.</summary>
        /// <value>The section lexical elements.</value>
        List<ILexicalElementable> LexicalElements { get; set; }
    }
}
