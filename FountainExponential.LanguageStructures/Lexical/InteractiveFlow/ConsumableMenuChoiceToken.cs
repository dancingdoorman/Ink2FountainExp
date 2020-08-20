﻿using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;

namespace FountainExponential.LanguageStructures.Lexical.InteractiveFlow
{
    public class ConsumableMenuChoiceToken : MenuChoiceToken, ILexicalElementable
    {
        public const char Sign = '*';

        public override string ToString()
        {
            return Sign.ToString();
        }
    }
}