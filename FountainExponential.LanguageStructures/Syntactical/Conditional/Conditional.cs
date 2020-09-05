using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;
using FountainExponential.LanguageStructures.Lexical.Code;
using FountainExponential.LanguageStructures.Lexical.Conditional;
using FountainExponential.LanguageStructures.Syntactical;
using FountainExponential.LanguageStructures.Syntactical.Code;

namespace FountainExponential.LanguageStructures.Syntactical.Conditional
{
    public class Conditional : ISyntacticalElementable, ITextContentSummarizable, ILexicalElementsContainable, ISyntacticalElementsContainable, IConditionable, IConditionEvaluatable
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

        public SpaceToken SpaceTokenBeforeConditionToken { get; set; }
        public ConditionToken ConditionToken { get; set; }
        public SpaceToken SpaceTokenAfterConditionToken { get; set; }


        public SpaceToken SpaceTokenBeforeTerminatingConditionToken { get; set; }
        public TerminatingConditionToken TerminatingConditionToken { get; set; } // can be ; or end-line


        public virtual bool IsTrue
        {
            get
            {
                return true;
            }
        }

        public override string ToString()
        {
            return IsTrue.ToString();
        }
    }
}
