using Ink.Ink2FountainExp.Interaction;
using System;
using System.Collections.Generic;
using System.Text;
using Ink.InkCompiler;

namespace Ink.Ink2FountainExp.OutputManagement
{
    public interface IToolOutputManagable
    {
        IConsoleInteractable ConsoleInteractor { get; set; }

        void ShowExportComplete(CommandLineToolOptions options);

        void ShowStats(CommandLineToolOptions options, Stats stats);

        void ShowCompileSuccess(CommandLineToolOptions options, bool compileSuccess);


        void PrintAllMessages(List<string> authorMessages, List<string> warnings, List<string> errors);
    }
}
