namespace BenEater8BitComputer.Compiler;

/// <summary>
/// Source code based on Immo Landwerth's (@terrajobst) compiler series
/// https://www.youtube.com/channel/UCaFP8iQMTuPXinXBMEXsSuw
/// </summary>
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
