using BenEater8BitComputer.Compiler.Text;

namespace BenEater8BitComputer.Compiler;

internal sealed class Lexer
{
    private readonly SourceText text;
    
    private int position;
    private int start;
    private SyntaxKind kind;
    private object value;

    private const char NullTerminator = '\0';

    public Lexer(SourceText text)
    {
        this.text = text;
    }

    public DiagnosticBag Diagnostics { get; } = new DiagnosticBag();

    private char Current
    {
        get
        {
            if (position >= text.Length)
            {
                return NullTerminator;
            }

            return text[position];
        }
    }

    private void Next()
    {
        position++;
    }

    public SyntaxToken Lex()
    {
        kind = SyntaxKind.BadToken;
        start = position;
        value = null;
        string text = string.Empty;

        if (Current == NullTerminator)
        {
            return new SyntaxToken(SyntaxKind.EndOfFileToken, position, $"{NullTerminator}", null);
        }

        if (char.IsLetter(Current))
        {
            while (char.IsLetter(Current))
            {
                Next();
            }

            var length = position - start;

            kind = SyntaxKind.SymbolToken;
            text = this.text.ToString(start, length);
            value = text;
        }
        else if (char.IsDigit(Current))
        {
            while (char.IsDigit(Current))
            {
                Next();
            }

            var length = position - start;
            text = this.text.ToString(start, length);
            if (!byte.TryParse(text, out var byteValue))
            {
                Diagnostics.ReportInvalidNumber(new TextSpan(start, length), text);
            }

            if (byteValue > 15)
            {
                Diagnostics.ReportInvalidNumberValue(new TextSpan(start, length), text);
            }

            kind = SyntaxKind.NumberToken;
            value = byteValue;
        }
        else if (char.IsWhiteSpace(Current))
        {
            while (char.IsWhiteSpace(Current))
            {
                Next();
            }

            var length = position - start;

            kind = SyntaxKind.WhitespaceToken;
            text = this.text.ToString(start, length);
            value = text;
        }

        if (kind != SyntaxKind.BadToken)
        {
            return new SyntaxToken(kind, start, text, value);
        }

        Diagnostics.ReportInvalidCharacter(position, Current);
        return new SyntaxToken(SyntaxKind.BadToken, position++, this.text.ToString(position - 1, 1), value);
    }
}
