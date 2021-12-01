namespace BenEater8BitComputer.Compiler;

public sealed class Assembler
{
    private readonly Program program;

    public Assembler(Program program)
    {
        this.program = program;
    }

    public AssemblerResult Emit()
    {
        if (program.Diagnostics.Length > 0)
        {
            return AssemblerResult.FromError(string.Join("\n", program.Diagnostics));
        }

        var output = new List<byte>();

        foreach (var instruction in program.Instructions)
        {
            var opcode = InstructionMap.Opcodes.FirstOrDefault(x => x.Mnemonic == instruction.Opcode.Text);

            if (opcode is null)
            {
                return AssemblerResult.FromError($"Error: Cannot find opcode information for instruction {instruction.Opcode.Text}");
            }

            byte data = (byte)(opcode.ByteCode << 4);
            if (instruction.Operand is not null && instruction.Operand.Value is not null)
            {
                data |= (byte)((byte)instruction.Operand.Value & 0b1111);
            }
            output.Add(data);
        }

        return AssemblerResult.FromOutput(output.ToArray());
    }
}

public class AssemblerResult
{
    private AssemblerResult(byte[] output)
    {
        Output = output;
    }

    private AssemblerResult(string error)
    {
        Error = error;
    }

    public byte[] Output { get; }
    public string Error { get; }

    public static AssemblerResult FromOutput(byte[] output) => new AssemblerResult(output);
    public static AssemblerResult FromError(string error) => new AssemblerResult(error);
}