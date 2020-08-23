using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;

namespace FountainExponential.LanguageStructures.Lexical.FountainElement
{
    public class IntExteriorSceneHeadingSideToken : SceneHeadingSideToken, ILexicalElementable
    {
        public const string Keyword = "INT./EXT."; // Also "INT/EXT" or "I/E"
        public override string ToString()
        {
            return Keyword;
        }
    }
}
