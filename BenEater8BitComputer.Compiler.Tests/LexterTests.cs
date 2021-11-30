using BenEater8BitComputer.Compiler;
using BenEater8BitComputer.Compiler.Text;
using Shouldly;
using Xunit;

namespace BenEater8BitComputer.Compiler.Tests
{
    public class LexterTests
    {
        [Fact]
        public void TestSymbol()
        {
            // Arrange
            var text = "LDA";
            var sourceText = SourceText.From(text);
            var lexer = new Lexer(sourceText);

            // Act
            var token = lexer.Lex();

            // Assert
            token.Kind.ShouldBe(SyntaxKind.SymbolToken);
            token.Position.ShouldBe(0);
            token.Text.ShouldBe(text);
            token.Value.ShouldBe(text);
        }

        [Fact]
        public void TestNumber()
        {
            // Arrange
            var text = "15";
            var sourceText = SourceText.From(text);
            var lexer = new Lexer(sourceText);

            // Act
            var token = lexer.Lex();

            // Assert
            token.Kind.ShouldBe(SyntaxKind.NumberToken);
            token.Position.ShouldBe(0);
            token.Text.ShouldBe(text);
            token.Value.ShouldBe(15);
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("  ")]
        [InlineData("\t")]
        [InlineData("\r")]
        [InlineData("\n")]
        [InlineData("\r\n")]
        public void TestWhitespace(string text)
        {
            // Arrange
            var sourceText = SourceText.From(text);
            var lexer = new Lexer(sourceText);

            // Act
            var token = lexer.Lex();

            // Assert
            token.Kind.ShouldBe(SyntaxKind.WhitespaceToken);
            token.Position.ShouldBe(0);
            token.Text.ShouldBe(text);
            token.Value.ShouldBe(text);
        }

        [Theory]
        [InlineData("!")]
        [InlineData("@")]
        [InlineData("#")]
        [InlineData("$")]
        [InlineData("%")]
        public void TestBadToken(string text)
        {
            // Arrange
            var sourceText = SourceText.From(text);
            var lexer = new Lexer(sourceText);

            // Act
            var token = lexer.Lex();

            // Assert
            token.Kind.ShouldBe(SyntaxKind.BadToken);
            token.Position.ShouldBe(0);
            token.Text.ShouldBe(text);
            token.Value.ShouldBe(null);
            lexer.Diagnostics[0].Message.ShouldStartWith("Invalid character input");
        }

        [Fact]
        public void TestEndOfFile()
        {
            // Arrange
            var lexer = new Lexer(SourceText.From(""));

            // Act
            var token = lexer.Lex();

            // Assert
            token.Kind.ShouldBe(SyntaxKind.EndOfFileToken);
            token.Position.ShouldBe(0);
            token.Text.ShouldBe("\0");
            token.Value.ShouldBe(null);
        }
    }
}
