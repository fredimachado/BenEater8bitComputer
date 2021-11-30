using BenEater8BitComputer.Compiler;
using BenEater8BitComputer.Compiler.Text;
using Shouldly;
using Xunit;

namespace BenEater8BitComputer.Compiler.Tests
{
    public class ProgramTests
    {
        [Fact]
        public void TestInstructions()
        {
            // Arrange
            var text = @"LDA 15
ADD 14";
            var sourceText = SourceText.From(text);
            var parser = new Parser(sourceText);

            // Act
            var program = parser.Parse();

            // Assert
            program.Instructions[0].Opcode.Kind.ShouldBe(SyntaxKind.SymbolToken);
            program.Instructions[0].Opcode.Text.ShouldBe("LDA");
            program.Instructions[0].Operand.Kind.ShouldBe(SyntaxKind.NumberToken);
            program.Instructions[0].Operand.Text.ShouldBe("15");
            program.Instructions[0].Operand.Value.ShouldBe(15);

            program.Instructions[1].Opcode.Kind.ShouldBe(SyntaxKind.SymbolToken);
            program.Instructions[1].Opcode.Text.ShouldBe("ADD");
            program.Instructions[1].Operand.Kind.ShouldBe(SyntaxKind.NumberToken);
            program.Instructions[1].Operand.Text.ShouldBe("14");
            program.Instructions[1].Operand.Value.ShouldBe(14);
        }
    }
}
