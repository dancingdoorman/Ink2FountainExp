﻿using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;

namespace FountainExponential.LanguageStructures.Lexical.MetaData
{
    public class KeyValuePairAssignmentToken : ILexicalElementable
    {
        public const char Sign = ':';
        public const string Keyword = ":";

        public override string ToString()
        {
            return Keyword;
        }
    }
}
