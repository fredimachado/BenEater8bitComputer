using BenEater8BitComputer.Compiler.Text;
using System.Collections;

namespace BenEater8BitComputer.Compiler;

internal sealed class DiagnosticBag : IEnumerable<Diagnostic>
{
    private readonly List<Diagnostic> diagnostics = new List<Diagnostic>();

    public IEnumerator<Diagnostic> GetEnumerator() => diagnostics.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public Diagnostic this[int index] => diagnostics[index];

    internal void AddRange(DiagnosticBag diagnostics)
    {
        this.diagnostics.AddRange(diagnostics);
    }

    private void Report(TextSpan span, string message)
    {
        var diagnostic = new Diagnostic(span, message);
        diagnostics.Add(diagnostic);
    }

    internal void ReportInvalidNumber(TextSpan span, string text)
    {
        var message = $"The number '{text}' is not a valid byte.";
        Report(span, message);
    }

    internal void ReportInvalidNumberValue(TextSpan span, string text)
    {
        var message = $"Operand value cannot be greater than 15.";
        Report(span, message);
    }

    internal void ReportInvalidCharacter(int position, char character)
    {
        var span = new TextSpan(position, 1);
        var message = $"Invalid character input '{character}'.";
        Report(span, message);
    }

    internal void ReportUnexpectedToken(TextSpan span, SyntaxKind actualKind, SyntaxKind expectedKind)
    {
        var message = $"Unexpected token <{actualKind}>, expected <{expectedKind}>.";
        Report(span, message);
    }

    internal void ReportUnknownInstruction(SyntaxToken instruction)
    {
        var message = $"Unknown instruction '{instruction.Text}'.";
        Report(instruction.Span, message);
    }

    internal void ReportMissingOperand(SyntaxToken instruction)
    {
        var message = $"Instruction'{instruction.Text}' requires an operand.";
        Report(instruction.Span, message);
    }
}
