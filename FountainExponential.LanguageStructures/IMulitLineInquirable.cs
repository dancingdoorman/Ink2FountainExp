using System;
using System.Collections.Generic;
using System.Text;

namespace FountainExponential.LanguageStructures
{
    public interface IMulitLineInquirable
    {
        int StartLineNumber { get; }

        int EndLineNumber { get; }
    }
}
