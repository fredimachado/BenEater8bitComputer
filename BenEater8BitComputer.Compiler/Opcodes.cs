namespace BenEater8BitComputer.Compiler;

public class Opcode
{
    public Opcode(string mnemonic, byte byteCode, bool hasOperand)
    {
        Mnemonic = mnemonic;
        ByteCode = byteCode;
        HasOperand = hasOperand;
    }

    public string Mnemonic { get; }
    public byte ByteCode { get; }
    public bool HasOperand { get; }
}
