﻿
using System.Collections.Generic;

namespace Ink.InkCompiler
{
    public interface IInputInterpreter
    {
        InputInterpretationResult InterpretCommandLineInput(string userInput, Parsed.IFiction parsedFiction, Runtime.IStory runtimeStory);
        void RetrieveDebugSourceForLatestContent(Runtime.IStory runtimeStory);

        //List<DebugSourceRange> DebugSourceRanges { get; set; }
    }
}