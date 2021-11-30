namespace BenEater8BitComputer.Compiler;

internal static class InstructionMap
{
    public static Opcode[] Opcodes { get; } = new[]
    {
            new Opcode("NOP", 0b0000, hasOperand: false),
            new Opcode("LDA", 0b0001, hasOperand: true),
            new Opcode("ADD", 0b0010, hasOperand: true),
            new Opcode("SUB", 0b0011, hasOperand: true),
            new Opcode("STA", 0b0100, hasOperand: true),
            new Opcode("LDI", 0b0101, hasOperand: true),
            new Opcode("JMP", 0b0110, hasOperand: true),
            new Opcode("OUT", 0b1110, hasOperand: false),
            new Opcode("HLT", 0b1111, hasOperand: false),
        };
}
