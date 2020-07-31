using System.Collections.Generic;

namespace Ink.InkCompiler
{
    public class CompilerOptions
    {
        public string sourceFilename;
        public List<string> pluginNames;
        public bool countAllVisits;
        public Ink.InkParser.IFileHandler fileHandler;
    }
}
