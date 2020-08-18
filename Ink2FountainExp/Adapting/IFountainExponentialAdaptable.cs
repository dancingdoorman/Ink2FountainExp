using Ink.Ink2FountainExp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ink.InkCompiler;

namespace Ink.Ink2FountainExp.Adapting
{
    /// <summary>The IFountainExponentialAdaptable interface defines the interaction with Fountain Exponential.</summary>
    public interface IFountainExponentialAdaptable
    {
        string ConvertToFountainExponential(Ink.Parsed.Fiction parsedFiction, string inputFileName);
    }
}
