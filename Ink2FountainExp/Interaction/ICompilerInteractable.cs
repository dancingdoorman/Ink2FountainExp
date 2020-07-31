using Ink.Ink2FountainExp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ink.InkCompiler;

namespace Ink.Ink2FountainExp.Interaction
{
    /// <summary>The ICompilerInteractable interface defines the interaction with the compiler.</summary>
    public interface ICompilerInteractable
    {
        IInkCompiler CreateCompiler(string fileTextContent, CompilerOptions compilerOptions);
    }
}
