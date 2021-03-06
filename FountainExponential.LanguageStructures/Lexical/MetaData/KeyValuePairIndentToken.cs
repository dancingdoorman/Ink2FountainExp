﻿using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;

namespace FountainExponential.LanguageStructures.Lexical.MetaData
{
    public class KeyValuePairIndentToken : ILexicalElementable
    {
        public const char Sign = '\t';
        public const string Keyword = "\t";
        public override string ToString()
        {
            return Keyword;
        }
    }
}
