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
    /// <summary>The MatchingMultipleConditional class encapsulates a MultipleConditional that compares the central expression with the cases.</summary>
    public class MatchingMultipleConditional : MultipleConditional, ISyntacticalElementable, IConditionable, IMultipleConditionable, IConditionEvaluatable
    {
        // A switch case like matching of the conditions
        // $SomeValue ? 1# The first | 2# The second ;

        // should also be able to do with other expressions
        // $SomeValue ? 2+3# The first | GetGameValue()# The second ;

        public string Expression { get; set; }
    }
}
