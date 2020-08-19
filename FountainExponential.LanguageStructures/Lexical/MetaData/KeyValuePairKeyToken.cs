using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using FountainExponential.LanguageStructures.Lexical;

namespace FountainExponential.LanguageStructures.Lexical.MetaData
{
    public class KeyValuePairKeyToken : ILexicalElementable
    {
        public string Keyword { get; set; }
    }
}
