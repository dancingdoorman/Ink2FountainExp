﻿using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;

namespace FountainExponential.LanguageStructures.Lexical.InteractiveFlow
{
    public class MenuChoiceToken : ILexicalElementable
    {
        //* Consuming Choice
        //+ Sticky Choice
        //- Gather /end choice /continuing

        public override string ToString()
        {
            return "O";
        }
    }
}
