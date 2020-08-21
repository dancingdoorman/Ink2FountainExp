using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;
using FountainExponential.LanguageStructures.Lexical.Sections;
using FountainExponential.LanguageStructures.Syntactical;

namespace FountainExponential.LanguageStructures.Syntactical.Sections
{
    public class NanoSlice : SectionBase, ISyntacticalElementable, IStartTokenEnsurable
    {
        public NanoSliceToken NanoSliceStartToken { get; set; }

        public ILexicalElementable EnsureStartToken()
        {
            if (NanoSliceStartToken == null)
                NanoSliceStartToken = new NanoSliceToken();

            return NanoSliceStartToken;
        }

        public override string ToString()
        {
            return NanoSliceStartToken + SectionName;
        }
    }
}
