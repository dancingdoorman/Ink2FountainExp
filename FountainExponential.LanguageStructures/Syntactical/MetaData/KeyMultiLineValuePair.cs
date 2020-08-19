using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical.MetaData;
using FountainExponential.LanguageStructures.Syntactical;

namespace FountainExponential.LanguageStructures.Syntactical.MetaData
{
    public class KeyMultiLineValuePair : KeyValuePair, ISyntacticalElementable
    {
        public List<ValueLine> ValueLineList { get; set; } = new List<ValueLine>();

        public override string ToString()
        {
            if (ValueLineList != null)
            {
                var builder = new StringBuilder();
                if(Key != null)
                    builder.Append(this.Key.Keyword);

                builder.Append(KeyValuePairAssignmentToken.Sign);
                //builder.Append(" ");

                bool first = true;
                foreach (var valueLine in ValueLineList)
                {
                    if(first == false)
                    {
                        builder.Append(" ");
                    }
                    builder.Append(valueLine.Value);

                    first = false;
                }
                return builder.ToString();
            }
            return base.ToString();
        }
    }
}
