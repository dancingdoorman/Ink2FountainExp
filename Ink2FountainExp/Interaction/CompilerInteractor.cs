using Ink.Ink2FountainExp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ink.InkCompiler;

namespace Ink.Ink2FountainExp.Interaction
{
    public class CompilerInteractor : ICompilerInteractable
    {
        public IInkCompiler CreateCompiler(string fileTextContent, CompilerOptions compilerOptions)
        {
            return new Compiler(fileTextContent, compilerOptions);
        }
    }
}
