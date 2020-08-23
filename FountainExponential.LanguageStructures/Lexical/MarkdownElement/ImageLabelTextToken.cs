using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;

namespace FountainExponential.LanguageStructures.Lexical.MarkdownElement
{
    public class ImageTextToken : ILexicalElementable
    {
        public string Text { get; set; }
        public override string ToString()
        {
            return Text;
        }
    }
}
