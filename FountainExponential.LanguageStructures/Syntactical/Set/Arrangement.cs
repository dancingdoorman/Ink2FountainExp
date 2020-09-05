using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;
using FountainExponential.LanguageStructures.Lexical.Code;
using FountainExponential.LanguageStructures.Lexical.Conditional;
using FountainExponential.LanguageStructures.Lexical.Set;
using FountainExponential.LanguageStructures.Syntactical.FountainElement;

namespace FountainExponential.LanguageStructures.Syntactical.Set
{
    public class Arrangement : ISyntacticalElementable, ITextContentSummarizable, ILexicalElementsContainable, ISyntacticalElementsContainable, IIndentable
    {
        // An Arrangement can yield values in a linear (-), random (~) or reverse way (_)
        // An Arrangement can end on the last value ('), emptying without values (!) or loop endlessly (*).

        // A sequence (or a "stopping block") default $ is default linear and ending on last $-'
        // The radio hissed into life. $"Three!"|"Two!"|"One!"|There was the white noise racket of an explosion.|But it was just static.;
        // $I bought a coffee with my five-pound note.|I bought a second coffee for my friend.|I didn't have enough money to buy any more coffee.;
        // 

        // Looping cycles (there is no last) $-*
        // It was $*Monday|Tuesday|Wednesday|Thursday|Friday|Saturday|Sunday; today.

        // Emptying (once-only)
        // He told me a joke. $!I laughed politely.|I smiled.|I grimaced.|I promised myself to not react again.;

        // Random/Shuffles cycling $~*
        // I tossed the coin. $~Heads|Tails;.

        // Random/Shuffles $~|
        // I'm here $*~sometimes|occasionally;.


        /// <summary>Gets or sets the text content of the code container.</summary>
        /// <value>The text content of the code container.</value>
        public string TextContent { get; set; }


        /// <summary>Gets or sets the code container lexical elements.</summary>
        /// <value>The code container lexical elements.</value>
        public List<ILexicalElementable> LexicalElements { get; set; } = new List<ILexicalElementable>();

        /// <summary>Gets or sets the code container syntactical elements. This list is expected to hold all the syntactical elements in the code container.</summary>
        /// <value>The code container syntactical elements.</value>
        public List<ISyntacticalElementable> SyntacticalElements { get; set; } = new List<ISyntacticalElementable>();


        public ObtainerToken ObtainerToken { get; set; }
        public IndentLevel IndentLevel { get; set; }


        public TerminatingArrangementToken TerminatingArrangementToken { get; set; } // can be ; or end-line

        public List<ICaseArrangeable> Cases { get; set; } = new List<ICaseArrangeable>();
    }
}
