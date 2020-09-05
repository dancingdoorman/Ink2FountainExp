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
    /// <summary>The MultipleConditional class encapsulates a binary condition that obtains a value when true and another value when false</summary>
    public class MultipleConditional : Conditional, ISyntacticalElementable, IConditionable, IMultipleConditionable, IConditionEvaluatable
    {
        // Excample of 2 conditions linked by a : 
        // $SomeValue == 1 ? 1# The first : $SomeValue == 2 ? The second;

        public List<ICaseConditionable> Cases { get; set; } = new List<ICaseConditionable>();


        //public override string ToString()
        //{
        //    var builder = new StringBuilder();
        //    builder.Append(DefaultCodeSpanToken);
        //    return builder.ToString();
        //}
    }
}
