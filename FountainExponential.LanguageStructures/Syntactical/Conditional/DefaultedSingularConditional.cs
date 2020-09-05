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
    public class DefaultedSingularConditional : SingularConditional, ISyntacticalElementable, IConditionable, ISingularConditionable, IConditionEvaluatable, ICaseConditionalSyntacticalElementsObtainable, IDefaultSyntacticalElementsObtainable
    {
        public SpaceToken SpaceTokenBeforeConditionDefaultToken { get; set; }
        public ConditionDefaultToken ConditionDefaultToken { get; set; }
        public SpaceToken SpaceTokenAfterConditionDefaultToken { get; set; }

        /// <summary>Gets or sets the code container syntactical elements. This list is expected to hold all the syntactical elements in the code container.</summary>
        /// <value>The code container syntactical elements.</value>
        public List<ISyntacticalElementable> DefaultSyntacticalElements { get; set; } = new List<ISyntacticalElementable>();

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(ConditionDefaultToken);
            return builder.ToString();
        }
    }
}
