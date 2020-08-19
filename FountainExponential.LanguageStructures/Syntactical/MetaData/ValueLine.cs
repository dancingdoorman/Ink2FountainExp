using FountainExponential.LanguageStructures.Lexical;
using FountainExponential.LanguageStructures.Lexical.MetaData;
using System;
using System.Collections.Generic;
using System.Text;

namespace FountainExponential.LanguageStructures.Syntactical.MetaData
{
    public class ValueLine
    {
        public KeyValuePairIndentToken IndentToken { get; set; }
        public string Value { get; set; }

        public EndLine EndLine { get; set; }
    }
}
