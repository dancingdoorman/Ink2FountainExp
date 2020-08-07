using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("tests")]

namespace Ink.Parsed
{
    public enum SymbolType : uint
    {
        Knot,
        List,
        ListItem,
        Var,
        SubFlowAndWeave,
        Arg,
        Temp
    }
}

