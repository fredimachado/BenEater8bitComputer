using BenEater8BitComputer.Compiler.Text;

namespace BenEater8BitComputer.Compiler;

/// <summary>
/// Source code based on Immo Landwerth's (@terrajobst) compiler series
/// https://www.youtube.com/channel/UCaFP8iQMTuPXinXBMEXsSuw
/// </summary>
public sealed class InstructionSyntax : BaseSyntax
{
    public InstructionSyntax(SyntaxToken instruction, SyntaxToken operand)
    {
        Instruction = instruction;
        Operand = operand;
    }

    public override SyntaxKind Kind => SyntaxKind.InstructionSyntax;
    public SyntaxToken Instruction { get; }
    public SyntaxToken Operand { get; }

    public TextSpan Span => TextSpan.FromBounds(Instruction.Span, Operand.Span);
}
