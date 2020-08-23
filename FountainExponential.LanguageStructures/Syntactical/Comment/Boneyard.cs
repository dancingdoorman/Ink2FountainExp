using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical.Comment;
using FountainExponential.LanguageStructures.Syntactical;

namespace FountainExponential.LanguageStructures.Syntactical.Comment
{
    public class Boneyard : ISyntacticalElementable
    {
        public BoneyardStartToken BoneyardStartToken { get; set; }

        public BoneyardEndToken BoneyardEndToken { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(BoneyardStartToken);
            builder.Append(BoneyardEndToken);
            return builder.ToString();
        }
    }
}
