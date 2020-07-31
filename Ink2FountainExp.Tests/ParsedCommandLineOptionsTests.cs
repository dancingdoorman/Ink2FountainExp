using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.Extensions;
using NSubstitute.ReceivedExtensions;

using Ink;
using Ink.Ink2FountainExp;
using Ink.Ink2FountainExp.AutoPlay;
using Ink.Ink2FountainExp.Interaction;
using Ink.Ink2FountainExp.OutputManagement;
using Ink.Runtime;
using Ink.Parsed;
using Ink.InkCompiler;

namespace Ink.Ink2FountainExp.Tests
{
    public class ParsedCommandLineOptionsTests
    {
        public class IsInputPathNotGivenTests
        {
            [Fact]
            public void With_NoInputPathGiven()
            {
                // Arrange
                var parsedCommandLineOptions = new ParsedCommandLineOptions();

                // Act
                var isInputPathNotGiven = parsedCommandLineOptions.IsInputPathGiven;

                // Assert
                isInputPathNotGiven.Should().BeFalse("because there was no input file given");
            }

            [Fact]
            public void With_InputPathGiven()
            {
                // Arrange
                var parsedCommandLineOptions = new ParsedCommandLineOptions() { InputFilePath = "test.ink" };

                // Act
                var isInputPathNotGiven = parsedCommandLineOptions.IsInputPathGiven;

                // Assert
                isInputPathNotGiven.Should().BeTrue("because there was an input file given");
            }
        }
    }
}
