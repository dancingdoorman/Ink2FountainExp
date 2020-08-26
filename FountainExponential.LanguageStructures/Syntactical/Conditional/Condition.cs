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
    public class Condition : CodeContainerBase, ISyntacticalElementable, IConditionEvaluatable
    {
        public bool IsTrue
        {
            get
            {
                return true;
            }
        }

        //public override string ToString()
        //{
        //    var builder = new StringBuilder();

        //    return builder.ToString();
        //}
    }
}
