using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;
using FountainExponential.LanguageStructures.Lexical.Code;
using FountainExponential.LanguageStructures.Lexical.Conditional;
using FountainExponential.LanguageStructures.Syntactical;
using FountainExponential.LanguageStructures.Syntactical.Code;

namespace FountainExponential.LanguageStructures.Syntactical.Conditional
{
    /// <summary>The BinaryConditionedTranscriber class encapsulates a binary condition that obtains a value when true and another value when false</summary>
    public class BinaryConditionedTranscriber : BinaryCondition, ISyntacticalElementable, IConditionable, IConditionEvaluatable, ITranscribable
    {


        public string Transcription
        {
            get
            {
                return string.Empty;
            }
        }
    }
}
