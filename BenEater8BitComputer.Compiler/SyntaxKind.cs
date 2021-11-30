namespace BenEater8BitComputer.Compiler;

public enum SyntaxKind
{
    // Tokens
    BadToken,
    EndOfFileToken,
    WhitespaceToken,
    SymbolToken,
    NumberToken,

    // Syntax
    InstructionSyntax,
}
