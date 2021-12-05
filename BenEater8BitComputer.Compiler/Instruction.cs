namespace BenEater8BitComputer.Compiler;

public class Instruction
{
    public Instruction(string mnemonic, byte opcode, bool hasOperand)
    {
        Mnemonic = mnemonic;
        Opcode = opcode;
        HasOperand = hasOperand;
    }

    public string Mnemonic { get; }
    public byte Opcode { get; }
    public bool HasOperand { get; }
}
