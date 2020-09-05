using System;
using System.Collections.Generic;
using System.Text;

namespace FountainExponential.LanguageStructures.Syntactical.Code
{
    public interface ITranscribable
    {
        // The visitor should handle the transcribing, not the object itself


        string Transcription { get; }
    }
}
