using Ink.Parsed;
using Ink.Runtime;

namespace Ink.Ink2FountainExp
{
    public interface IConsoleUserInterface
    {
        void Begin(IStory story, IFiction parsedFiction, ConsoleUserInterfaceOptions options);
    }
}