using Ink.Ink2FountainExp.Interaction;
using Ink.Runtime;
using System;
using System.Collections.Generic;
using System.Text;
using Ink.InkCompiler;

namespace Ink.Ink2FountainExp.OutputManagement
{
    public interface IPlayerOutputManagable
    {
        IConsoleInteractable ConsoleInteractor { get; set; }

        void ShowChoices(List<Ink.Runtime.Choice> choices, ConsoleUserInterfaceOptions options);

        void RequestInput(ConsoleUserInterfaceOptions options);
        string GetUserInput();
        
        void ShowStreamError(ConsoleUserInterfaceOptions options);

        void ShowOutputResult(InputInterpretationResult result, ConsoleUserInterfaceOptions options);

        void ShowChoiceOutOffRange(ConsoleUserInterfaceOptions options);





        void ShowCurrentText(IStory story, ConsoleUserInterfaceOptions options);

        void ShowTags(List<string> tags, ConsoleUserInterfaceOptions options);

        void ShowWarningsAndErrors(List<string> warnings, List<string> errors, ConsoleUserInterfaceOptions options);

        void ShowEndOfStory(ConsoleUserInterfaceOptions options);
    }
}
