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
    public class ProcessedOptionsTests
    {
        public class IsInputFileJsonTests
        {
            [Fact]
            public void With_JsonFile()
            {
                // Arrange
                var processedOptions = new CommandLineToolOptions() { InputFileName = "test.json" };

                // Act
                var isInputFileJson = processedOptions.IsInputFileJson;

                // Assert
                isInputFileJson.Should().BeTrue("because the given file has a JSON extension");
            }

            [Fact]
            public void With_InkFile()
            {
                // Arrange
                var processedOptions = new CommandLineToolOptions() { InputFileName = "test.ink" };

                // Act
                var isInputFileJson = processedOptions.IsInputFileJson;

                // Assert
                isInputFileJson.Should().BeFalse("because the given file has no JSON extension");
            }
        }
    }
}
