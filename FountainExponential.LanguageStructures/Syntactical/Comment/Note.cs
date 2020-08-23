using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical.Comment;
using FountainExponential.LanguageStructures.Syntactical;

namespace FountainExponential.LanguageStructures.Syntactical.Comment
{
    public class Note : ISyntacticalElementable
    {
        public NoteStartToken NoteStartToken { get; set; }

        public NoteEndToken NoteEndToken { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(NoteStartToken);
            builder.Append(NoteEndToken);
            return builder.ToString();
        }
    }
}
