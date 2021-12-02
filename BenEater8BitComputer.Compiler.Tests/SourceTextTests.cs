using BenEater8BitComputer.Compiler.Text;
using Shouldly;
using Xunit;

namespace BenEater8BitComputer.Compiler.Tests
{
    public class SourceTextTests
    {
        [Theory]
        [InlineData("OUT", 1)]
        [InlineData("OUT\r\n", 2)]
        [InlineData("OUT\r\n\r\n", 3)]
        public void TestEmptyLine(string text, int expectedLineCount)
        {
            // Arrange & Act
            var sourceText = SourceText.From(text);

            // Assert
            sourceText.Lines.Length.ShouldBe(expectedLineCount);
        }
    }
}
