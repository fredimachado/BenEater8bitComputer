namespace BenEater8BitComputer.Compiler;

internal static class InstructionMap
{
    public static Instruction[] Instructions { get; } = new[]
    {
            new Instruction("NOP", 0b0000, hasOperand: false),
            new Instruction("LDA", 0b0001, hasOperand: true),
            new Instruction("ADD", 0b0010, hasOperand: true),
            new Instruction("SUB", 0b0011, hasOperand: true),
            new Instruction("STA", 0b0100, hasOperand: true),
            new Instruction("LDI", 0b0101, hasOperand: true),
            new Instruction("JMP", 0b0110, hasOperand: true),
            new Instruction("OUT", 0b1110, hasOperand: false),
            new Instruction("HLT", 0b1111, hasOperand: false),
        };
}
