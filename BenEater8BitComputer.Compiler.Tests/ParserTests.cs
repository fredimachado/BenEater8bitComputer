using BenEater8BitComputer.Compiler;
using BenEater8BitComputer.Compiler.Text;
using Shouldly;
using Xunit;

namespace BenEater8BitComputer.Compiler.Tests
{
    public class ParserTests
    {
        [Fact]
        public void TestInstructions()
        {
            // Arrange
            var text = "LDA 15";
            var parser = new Parser(SourceText.From(text));

            // Act
            var program = parser.Parse();

            // Assert
            program.Instructions[0].Opcode.Kind.ShouldBe(SyntaxKind.SymbolToken);
            program.Instructions[0].Opcode.Text.ShouldBe("LDA");
            program.Instructions[0].Operand.Kind.ShouldBe(SyntaxKind.NumberToken);
            program.Instructions[0].Operand.Text.ShouldBe("15");
            program.Instructions[0].Operand.Value.ShouldBe(15);
        }

        [Fact]
        public void TestMissingOperand()
        {
            // Arrange
            var text = "LDA";
            var parser = new Parser(SourceText.From(text));

            // Act
            var program = parser.Parse();

            // Assert
            program.Instructions[0].Opcode.Kind.ShouldBe(SyntaxKind.SymbolToken);
            program.Instructions[0].Opcode.Text.ShouldBe("LDA");
            program.Instructions[0].Operand.ShouldBeNull();
            parser.Diagnostics[0].Message.ShouldBe("Instruction 'LDA' requires an operand.");
        }

        [Fact]
        public void TestParserInstructionError()
        {
            // Arrange
            var text = "10 15";
            var parser = new Parser(SourceText.From(text));

            // Act
            var program = parser.Parse();

            // Assert
            program.Instructions[0].Opcode.Kind.ShouldBe(SyntaxKind.SymbolToken);
            program.Instructions[0].Operand.Kind.ShouldBe(SyntaxKind.NumberToken);
            parser.Diagnostics[0].Message.ShouldStartWith("Unexpected token <NumberToken>");
        }
    }
}
