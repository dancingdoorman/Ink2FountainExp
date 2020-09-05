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
    public class CodeSpanCondition : Condition, ISyntacticalElementable, IConditionEvaluatable, IConditionalEnclosable, ICodeSpanEnclosable
    {
        public CodeSpanStartToken CodeSpanStartToken { get; set; }

        public CodeSpanEndToken CodeSpanEndToken { get; set; }

        public ConditionOpenToken ConditionOpenToken { get; set; }

        public ConditionCloseToken ConditionCloseToken { get; set; }

        //public override string ToString()
        //{
        //    var builder = new StringBuilder();

        //    return builder.ToString();
        //}
    }
}
