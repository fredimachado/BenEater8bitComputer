using System.Text;

namespace BenEater8BitComputer.Compiler;

public sealed class Assembler
{
    private readonly Program program;

    public Assembler(Program program)
    {
        this.program = program;
    }

    public string Emit()
    {
        if (program.Diagnostics.Length > 0)
        {
            return string.Join("\n", program.Diagnostics);
        }

        var sb = new StringBuilder();

        foreach (var instruction in program.Instructions)
        {
            var opcode = InstructionMap.Opcodes.FirstOrDefault(x => x.Mnemonic == instruction.Opcode.Text);

            if (opcode is null)
            {
                return $"Error: Cannot find opcode information for instruction {instruction.Opcode.Text}";
            }

            var data = opcode.ByteCode << 4;
            if (instruction.Operand is not null && instruction.Operand.Value is not null)
            {
                data |= (byte)instruction.Operand.Value & 0b1111;
            }
            sb.Append($"0x{data:X2} ");
        }

        return sb.ToString(0, sb.Length - 1);
    }
}
