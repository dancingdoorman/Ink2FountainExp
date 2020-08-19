using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;
using FountainExponential.LanguageStructures.Lexical.MetaData;
using FountainExponential.LanguageStructures.Syntactical;

namespace FountainExponential.LanguageStructures.Syntactical.MetaData
{
    public class KeyValuePair : IKeyInformable, ISyntacticalElementable
    {
        public KeyValuePairKeyToken Key { get; set; }
        public KeyValuePairAssignmentToken AssignmentToken { get; set; }

        public EndLine EndLine { get; set; }
    }
}
