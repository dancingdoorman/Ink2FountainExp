using FountainExponential.LanguageStructures.Lexical.Code;
using System;
using System.Collections.Generic;
using System.Text;

namespace FountainExponential.LanguageStructures.Syntactical.Code
{
    public interface ICodeBlockEnclosable
    {
        CodeBlockStartToken CodeBlockStartToken { get; set; }

        CodeBlockEndToken CodeBlockEndToken { get; set; }

    }
}
