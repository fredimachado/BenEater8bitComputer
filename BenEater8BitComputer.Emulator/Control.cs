using C = BenEater8BitComputer.Emulator.ControlLineFlags;

namespace BenEater8BitComputer.Emulator;

/// <summary>
/// Control Logic
/// Contains microcode for each instruction and sets the correct control lines for each step (T0..T4)
/// </summary>
public class Control : Component
{
    private readonly Ir instructionRegister;
    private readonly Stepper stepperRegister;

    internal static C[] Microcode { get; } = new C[]
    {
            // 0000 - NOP
            C.MI | C.CO, C.RO | C.II | C.CE, 0,           0,           0,                  0, 0, 0,
            // 0001 - LDA
            C.MI | C.CO, C.RO | C.II | C.CE, C.IO | C.MI, C.RO | C.AI, 0,                  0, 0, 0,
            // 0010 - ADD
            C.MI | C.CO, C.RO | C.II | C.CE, C.IO | C.MI, C.RO | C.BI, C.EO | C.AI,        0, 0, 0,
            // 0011 - SUB
            C.MI | C.CO, C.RO | C.II | C.CE, C.IO | C.MI, C.RO | C.BI, C.EO | C.AI | C.SU, 0, 0, 0,
            // 0100 - STA
            C.MI | C.CO, C.RO | C.II | C.CE, C.IO | C.MI, C.AO | C.RI, 0,                  0, 0, 0,
            // 0101 - LDI
            C.MI | C.CO, C.RO | C.II | C.CE, C.IO | C.AI, 0,           0,                  0, 0, 0,
            // 0110 - JMP
            C.MI | C.CO, C.RO | C.II | C.CE, C.IO | C.J,  0,           0,                  0, 0, 0,
            // 0111
            C.MI | C.CO, C.RO | C.II | C.CE, 0,           0,           0,                  0, 0, 0,
            // 1000
            C.MI | C.CO, C.RO | C.II | C.CE, 0,           0,           0,                  0, 0, 0,
            // 1001
            C.MI | C.CO, C.RO | C.II | C.CE, 0,           0,           0,                  0, 0, 0,
            // 1010
            C.MI | C.CO, C.RO | C.II | C.CE, 0,           0,           0,                  0, 0, 0,
            // 1011
            C.MI | C.CO, C.RO | C.II | C.CE, 0,           0,           0,                  0, 0, 0,
            // 1100
            C.MI | C.CO, C.RO | C.II | C.CE, 0,           0,           0,                  0, 0, 0,
            // 1101
            C.MI | C.CO, C.RO | C.II | C.CE, 0,           0,           0,                  0, 0, 0,
            // 1110 - OUT
            C.MI | C.CO, C.RO | C.II | C.CE, C.AO | C.OI, 0,           0,                  0, 0, 0,
            // 1111 - HLT
            C.MI | C.CO, C.RO | C.II | C.CE, C.HLT,       0,           0,                  0, 0, 0,
    };

    public Control(Bus bus, Ir instructionRegister, Stepper stepperRegister) : base(bus)
    {
        this.instructionRegister = instructionRegister;
        this.stepperRegister = stepperRegister;
    }

    public override void FallingEdge()
    {
        // Get instruction from 4 most significant bits in instruction register
        var instruction = instructionRegister.Value & 0xF0;
        // Shift right instruction because step only uses 3 bits
        var index = (byte)(instruction >> 1 | (stepperRegister.Value & 0b111));

        var controlLineFlags = Microcode[index];
        bus.SetControleLineFlags(controlLineFlags);
    }

    public override string ToString()
    {
        return bus.ControlLine.ToString();
    }
}
