using System;
using System.Collections.Generic;
using System.Text;

namespace FountainExponentialSyntaxStructures
{
    public interface IMulitLineInquirable
    {
        int StartLineNumber { get; }

        int EndLineNumber { get; }
    }
}
