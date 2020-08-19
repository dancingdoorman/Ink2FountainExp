using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;
using FountainExponential.LanguageStructures.Lexical.MetaData;
using FountainExponential.LanguageStructures.Syntactical;

namespace FountainExponential.LanguageStructures.Syntactical.MetaData
{
    public class KeySingleLineValuePair : KeyValuePair, ISyntacticalElementable
    {
        public SpaceToken SpaceToken { get; set; }

        public string Value { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append(this.Key.Keyword);

            builder.Append(KeyValuePairAssignmentToken.Sign);

            builder.Append(Value);

            return builder.ToString();
        }
    }
}
