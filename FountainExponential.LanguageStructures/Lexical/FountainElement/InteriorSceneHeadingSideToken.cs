using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;

namespace FountainExponential.LanguageStructures.Lexical.FountainElement
{
    public class InteriorSceneHeadingSideToken : SceneHeadingSideToken, ILexicalElementable
    {
        public const string Keyword = "INT."; // Also "INT" or "int" as the . abbreviation and upper or lowercase does not matter.
        public override string ToString()
        {
            return Keyword;
        }
    }
}
