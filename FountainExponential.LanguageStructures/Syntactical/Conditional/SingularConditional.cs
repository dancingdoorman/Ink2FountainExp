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
    public class SingularConditional : Conditional, ISyntacticalElementable, IConditionable, ISingularConditionable, IConditionEvaluatable, ICaseConditionalSyntacticalElementsObtainable
    {

        public Condition Condition { get; set; }


        /// <summary>Gets or sets the code container syntactical elements. This list is expected to hold all the syntactical elements in the code container.</summary>
        /// <value>The code container syntactical elements.</value>
        public List<ISyntacticalElementable> CaseSyntacticalElements { get; set; } = new List<ISyntacticalElementable>();

    }
}
