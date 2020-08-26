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
    /// <summary>The BinaryCondition class encapsulates a binary condition that obtains a value when true and another value when false</summary>
    public class BinaryCondition : CodeSpan, ISyntacticalElementable, IConditionable, IConditionEvaluatable
    {
        public ConditionToken ConditioningToken { get; set; }
        public Condition Condition { get; set; }
        public AlternativeConditionToken AlternativeCodeSpanToken { get; set; }

        /// <summary>Gets or sets the code container syntactical elements. This list is expected to hold all the syntactical elements in the code container.</summary>
        /// <value>The code container syntactical elements.</value>
        public List<ISyntacticalElementable> SyntacticalElementsWhenTrue { get; set; } = new List<ISyntacticalElementable>();

        /// <summary>Gets or sets the code container syntactical elements. This list is expected to hold all the syntactical elements in the code container.</summary>
        /// <value>The code container syntactical elements.</value>
        public List<ISyntacticalElementable> SyntacticalElementsWhenFalse { get; set; } = new List<ISyntacticalElementable>();

        public bool IsTrue
        { 
            get 
            {
                return true;
            }  
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(CodeSpanStartToken);
            builder.Append(AlternativeCodeSpanToken);
            builder.Append(CodeSpanEndToken);
            return builder.ToString();
        }
    }
}
