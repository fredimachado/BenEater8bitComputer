namespace BenEater8BitComputer.Compiler.Text;

/// <summary>
/// Source code based on Immo Landwerth's (@terrajobst) compiler series
/// https://www.youtube.com/channel/UCaFP8iQMTuPXinXBMEXsSuw
/// </summary>
public struct TextSpan
{
    public TextSpan(int start, int length)
    {
        Start = start;
        Length = length;
    }

    public int Start { get; }
    public int Length { get; }
    public int End => Start + Length;

    internal static TextSpan FromBounds(TextSpan first, TextSpan last)
    {
        var length = last.End - first.Start;
        return new TextSpan(first.Start, length);
    }
}
