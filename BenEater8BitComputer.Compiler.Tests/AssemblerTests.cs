using BenEater8BitComputer.Compiler;
using BenEater8BitComputer.Compiler.Text;
using Shouldly;
using Xunit;

namespace BenEater8BitComputer.Compiler.Tests
{
    public class AssemblerTests
    {
        [Theory]
        [InlineData("LDA 14", 0x1E)]
        [InlineData("ADD 15", 0x2F)]
        [InlineData("SUB 9",  0x39)]
        [InlineData("STA 5",  0x45)]
        [InlineData("LDI 10", 0x5A)]
        [InlineData("JMP 12", 0x6C)]
        [InlineData("OUT",    0xE0)]
        [InlineData("HLT",    0xF0)]
        public void TestInstructions(string text, byte expected)
        {
            // Arrange
            var sourceText = SourceText.From(text);
            var program = new Parser(sourceText).Parse();
            var assembler = new Assembler(program);

            // Act
            var result = assembler.Emit();

            // Assert
            result.Output.ShouldBe(new byte[] { expected });
        }

        [Fact]
        public void TestProgram()
        {
            // Arrange
            var text = @"LDA 14
ADD 15
OUT
HLT";

            var sourceText = SourceText.From(text);
            var program = new Parser(sourceText).Parse();
            var assembler = new Assembler(program);

            // Act
            var result = assembler.Emit();

            // Assert
            result.Output.ShouldBe(new byte[] { 0x1E, 0x2F, 0xE0, 0xF0 });
        }
    }
}
