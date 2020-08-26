using FountainExponential.LanguageStructures.Lexical.Code;
using System;
using System.Collections.Generic;
using System.Text;

namespace FountainExponential.LanguageStructures.Syntactical.Code
{
    public interface ICodeSpanEnclosable
    {
        CodeSpanStartToken CodeSpanStartToken { get; set; }

        CodeSpanEndToken CodeSpanEndToken { get; set; }
    }
}
