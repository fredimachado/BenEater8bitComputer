using BenEater8BitComputer.Compiler.Text;
using System.Collections.Immutable;

namespace BenEater8BitComputer.Compiler;

internal sealed class Parser
{
    private readonly ImmutableArray<SyntaxToken> tokens;
    private readonly SourceText text;
    private int position;

    public Parser(SourceText text)
    {
        var tokens = new List<SyntaxToken>();

        var lexer = new Lexer(text);
        SyntaxToken token;

        do
        {
            token = lexer.Lex();

            if (token.Kind != SyntaxKind.WhitespaceToken
                && token.Kind != SyntaxKind.BadToken)
            {
                tokens.Add(token);
            }
        } while (token.Kind != SyntaxKind.EndOfFileToken);

        this.text = text;
        this.tokens = tokens.ToImmutableArray();

        Diagnostics.AddRange(lexer.Diagnostics);
    }

    public DiagnosticBag Diagnostics { get; } = new DiagnosticBag ();

    private SyntaxToken Peek(int offset)
    {
        var index = position + offset;
        if (index >= tokens.Length)
        {
            return tokens[tokens.Length - 1];
        }

        return tokens[index];
    }

    private SyntaxToken Current => Peek(0);

    private SyntaxToken NextToken()
    {
        var current = Current;
        position++;
        return current;
    }

    private SyntaxToken MatchToken(SyntaxKind kind)
    {
        if (Current.Kind == kind)
        {
            return NextToken();
        }

        Diagnostics.ReportUnexpectedToken(Current.Span, Current.Kind, kind);

        return new SyntaxToken(kind, Current.Position, Current.Text, null);
    }

    public Program Parse()
    {
        var instructions = new List<InstructionSyntax>();
        do
        {
            instructions.Add(ParseInstruction());
        } while (Current.Kind != SyntaxKind.EndOfFileToken);

        var endOfFileToken = MatchToken(SyntaxKind.EndOfFileToken);

        return new Program(text, Diagnostics.ToImmutableArray(), instructions.ToImmutableArray(), endOfFileToken);
    }

    public InstructionSyntax ParseInstruction()
    {
        var instruction = MatchToken(SyntaxKind.SymbolToken);

        var opcode = InstructionMap.Opcodes.FirstOrDefault(x => x.Mnemonic == instruction.Text);
        if (opcode is null)
        {
            Diagnostics.ReportUnknownInstruction(instruction);
        }
        else if (opcode.HasOperand && Current.Kind != SyntaxKind.NumberToken)
        {
            Diagnostics.ReportMissingOperand(instruction);
        }

        SyntaxToken operand = null;
        if (Current.Kind == SyntaxKind.NumberToken)
        {
            operand = NextToken();
        }
        return new InstructionSyntax(instruction, operand);
    }
}
