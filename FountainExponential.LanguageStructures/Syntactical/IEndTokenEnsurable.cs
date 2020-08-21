using FountainExponential.LanguageStructures.Lexical;
using System;
using System.Collections.Generic;
using System.Text;

namespace FountainExponential.LanguageStructures.Syntactical
{
    public interface IEndTokenEnsurable
    {
        ILexicalElementable EnsureEndToken();
    }
}
