using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical.Comment;
using FountainExponential.LanguageStructures.Syntactical;

namespace FountainExponential.LanguageStructures.Syntactical.Comments
{
    public class Synopsis : ISyntacticalElementable
    {
        public SynopsisToken SynopsisToken { get; set; }
        public EndLine EndLine { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(SynopsisToken);
            builder.Append(EndLine);
            return builder.ToString();
        }
    }
}
