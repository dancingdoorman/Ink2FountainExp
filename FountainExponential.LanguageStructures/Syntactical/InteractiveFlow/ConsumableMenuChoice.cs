using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;
using FountainExponential.LanguageStructures.Lexical.InteractiveFlow;
using FountainExponential.LanguageStructures.Syntactical;
using FountainExponential.LanguageStructures.Syntactical.FountainElement;

namespace FountainExponential.LanguageStructures.Syntactical.InteractiveFlow
{
    public class ConsumableMenuChoice : MenuChoice, ISyntacticalElementable
    {
        public ConsumableMenuChoiceToken MenuChoiceToken { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(MenuChoiceToken);
            builder.Append(Description);
            return builder.ToString();
        }
    }
}
