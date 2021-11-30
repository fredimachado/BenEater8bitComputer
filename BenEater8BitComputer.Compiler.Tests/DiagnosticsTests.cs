using BenEater8BitComputer.Compiler.Text;
using Shouldly;
using Xunit;

namespace BenEater8BitComputer.Compiler.Tests
{
    public class DiagnosticsTests
    {
        [Fact]
        public void TestDiagnostics()
        {
            // Arrange
            var text = @"LDA 15
ADD 14
SUB";
            var sourceText = SourceText.From(text);
            var parser = new Parser(sourceText);

            // Act
            var program = parser.Parse();

            // Assert
            program.Diagnostics.Length.ShouldBe(1);
            program.Diagnostics[0].Message.ShouldBe("Instruction'SUB' requires an operand.");
            var (lineNumber, column) = program.Text.GetLineNumberAndColumn(program.Diagnostics[0].Span);
            lineNumber.ShouldBe(3);
            column.ShouldBe(1);
        }
        [Fact]
        public void TestDiagnosticsWithBlankLines()
        {
            // Arrange
            var text = @"LDA 15

ADD 14

SUB A";
            var sourceText = SourceText.From(text);
            var parser = new Parser(sourceText);

            // Act
            var program = parser.Parse();

            // Assert
            program.Diagnostics.Length.ShouldBe(2);
            program.Diagnostics[1].Message.ShouldBe("Unknown instruction 'A'.");
            var (lineNumber, column) = program.Text.GetLineNumberAndColumn(program.Diagnostics[1].Span);
            lineNumber.ShouldBe(5);
            column.ShouldBe(5);
        }
    }
}
