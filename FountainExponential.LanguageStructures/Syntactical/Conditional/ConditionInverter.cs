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
    /// <summary>The ConditionInverter class encapsulates the functionality of inverting the value of a condition making true into false and false into true.
    /// This is also called the unary operator ! because it's the only operator that acts on one element.</summary>
    public class ConditionInverter : Condition, ISyntacticalElementable, IConditionEvaluatable
    {

    }
}
