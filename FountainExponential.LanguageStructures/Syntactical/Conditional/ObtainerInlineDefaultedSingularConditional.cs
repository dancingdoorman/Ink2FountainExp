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
    public class ObtainerInlineDefaultedSingularConditional : InlineDefaultedSingularConditional, ISyntacticalElementable, IConditionable, ISingularConditionable, IConditionEvaluatable, IDefaultSyntacticalElementsObtainable
    {
        public ObtainerToken ObtainerToken { get; set; }
    }
}
