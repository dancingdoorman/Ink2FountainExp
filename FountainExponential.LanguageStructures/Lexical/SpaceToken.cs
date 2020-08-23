using System;
using System.Collections.Generic;
using System.Text;

namespace FountainExponential.LanguageStructures.Lexical
{
    /// <summary>The SpaceToken class encapsulates the space between words. It can be 1 or multiple spaces.</summary>
    public class SpaceToken : ILexicalElementable
    {
        public const string Sign = " ";
        public override string ToString()
        {
            return Sign;
        }
    }
}
