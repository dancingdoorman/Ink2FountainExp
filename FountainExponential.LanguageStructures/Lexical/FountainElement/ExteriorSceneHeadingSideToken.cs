using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;

namespace FountainExponential.LanguageStructures.Lexical.FountainElement
{
    public class ExteriorSceneHeadingSideToken : SceneHeadingSideToken, ILexicalElementable
    {
        public const string Keyword = "EXT.";
        public override string ToString()
        {
            return Keyword;
        }
    }
}
