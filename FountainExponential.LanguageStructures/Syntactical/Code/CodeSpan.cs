using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Syntactical;

namespace FountainExponential.LanguageStructures.Syntactical.Code
{
    public class CodeSpan : CodeContainerBase, ISyntacticalElementable
    {
        public override string ToString()
        {
            if (SyntacticalElements != null)
            {
                return "` `";
                //var builder = new StringBuilder();

                //foreach (var element in SyntacticalElements)
                //{
                //}
                //return builder.ToString();
            }
            return base.ToString();
        }
    }
}
