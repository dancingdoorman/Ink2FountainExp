﻿using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;
using FountainExponential.LanguageStructures.Lexical.Code;
using FountainExponential.LanguageStructures.Lexical.Conditional;
using FountainExponential.LanguageStructures.Syntactical;
using FountainExponential.LanguageStructures.Syntactical.Code;

namespace FountainExponential.LanguageStructures.Syntactical.Conditional
{
    /// <summary>The CaseCondition class encapsulates a binary condition that obtains a value when true and another value when false</summary>
    public class CaseCondition : Condition, ISyntacticalElementable, IConditionEvaluatable, ICaseConditionable
    {

        /// <summary>Gets or sets the code container syntactical elements. This list is expected to hold all the syntactical elements in the code container.</summary>
        /// <value>The code container syntactical elements.</value>
        public List<ISyntacticalElementable> CaseSyntacticalElements { get; set; } = new List<ISyntacticalElementable>();

        //public override string ToString()
        //{
        //    var builder = new StringBuilder();
        //    builder.Append(DefaultCodeSpanToken);
        //    return builder.ToString();
        //}
    }
}
