using FountainExponential.LanguageStructures.Lexical.MetaData;
using System;
using System.Collections.Generic;
using System.Text;

namespace FountainExponential.LanguageStructures.Syntactical.MetaData
{
    public interface IKeyInformable
    {
        KeyValuePairKeyToken Key { get; set; }
        KeyValuePairAssignmentToken AssignmentToken { get; set; }
    }
}
