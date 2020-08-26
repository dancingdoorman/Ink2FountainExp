using FountainExponential.LanguageStructures.Lexical.Code;
using System;
using System.Collections.Generic;
using System.Text;

namespace FountainExponential.LanguageStructures.Syntactical.Code
{
    public class InlineNamespace : ISyntacticalElementable
    {
        public NamespaceToken NamespaceToken { get; set; }

        public string Transcription
        {
            get
            {
                return string.Empty;
            }
        }
    }
}
