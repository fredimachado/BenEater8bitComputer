using BenEater8BitComputer.Compiler.Text;

namespace BenEater8BitComputer.Compiler;

public sealed class InstructionSyntax : BaseSyntax
{
    public InstructionSyntax(SyntaxToken instruction, SyntaxToken operand)
    {
        Opcode = instruction;
        Operand = operand;
    }

    public override SyntaxKind Kind => SyntaxKind.InstructionSyntax;
    public SyntaxToken Opcode { get; }
    public SyntaxToken Operand { get; }

    public TextSpan Span => TextSpan.FromBounds(Opcode.Span, Operand.Span);
}
