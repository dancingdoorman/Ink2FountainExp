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
    /// <summary>The DefaultedMatchingMultipleConditional class encapsulates a MultipleConditional that compares the central expression with the cases or uses the default.</summary>
    public class DefaultedMatchingMultipleConditional : MatchingMultipleConditional, ISyntacticalElementable, IConditionable, IMultipleConditionable, IConditionEvaluatable, IDefaultSyntacticalElementsObtainable
    {
        // A switch case like matching of the conditions with a default
        // $SomeValue ? 1# The first | 2# The second : Some other value ;

        // should also be able to do with other expressions
        // $SomeValue ? 2+3# The first | $GetGameValue()# The second : Some other value ;

        public SpaceToken SpaceTokenBeforeConditionDefaultToken { get; set; }
        public ConditionDefaultToken ConditionDefaultToken { get; set; }
        public SpaceToken SpaceTokenAfterConditionDefaultToken { get; set; }

        /// <summary>Gets or sets the code container syntactical elements. This list is expected to hold all the syntactical elements in the code container.</summary>
        /// <value>The code container syntactical elements.</value>
        public List<ISyntacticalElementable> DefaultSyntacticalElements { get; set; } = new List<ISyntacticalElementable>();

        //public override string ToString()
        //{
        //    var builder = new StringBuilder();
        //    builder.Append(DefaultCodeSpanToken);
        //    return builder.ToString();
        //}
    }
}
