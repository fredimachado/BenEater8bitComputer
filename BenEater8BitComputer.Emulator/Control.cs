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

    private readonly ControlLineFlags[] microcode;

    public Control(Bus bus, Ir instructionRegister, Stepper stepperRegister) : base(bus)
    {
        this.instructionRegister = instructionRegister;
        this.stepperRegister = stepperRegister;

        var fetchT0 = ControlLineFlags.MI | ControlLineFlags.CO;
        var fetchT1 = ControlLineFlags.RO | ControlLineFlags.II | ControlLineFlags.CE;

        microcode = new ControlLineFlags[]
        {
                // 0000 - NOP
                fetchT0, fetchT1, 0,           0,           0,                  0, 0, 0,
                // 0001 - LDA
                fetchT0, fetchT1, ControlLineFlags.IO | ControlLineFlags.MI, ControlLineFlags.RO | ControlLineFlags.AI, 0,                  0, 0, 0,
                // 0010 - ADD
                fetchT0, fetchT1, ControlLineFlags.IO | ControlLineFlags.MI, ControlLineFlags.RO | ControlLineFlags.BI, ControlLineFlags.EO | ControlLineFlags.AI,        0, 0, 0,
                // 0011 - SUB
                fetchT0, fetchT1, ControlLineFlags.IO | ControlLineFlags.MI, ControlLineFlags.RO | ControlLineFlags.BI, ControlLineFlags.EO | ControlLineFlags.AI | ControlLineFlags.SU, 0, 0, 0,
                // 0100 - STA
                fetchT0, fetchT1, ControlLineFlags.IO | ControlLineFlags.MI, ControlLineFlags.AO | ControlLineFlags.RI, 0,                  0, 0, 0,
                // 0101 - LDI
                fetchT0, fetchT1, ControlLineFlags.IO | ControlLineFlags.AI, 0,           0,                  0, 0, 0,
                // 0110 - JMP
                fetchT0, fetchT1, ControlLineFlags.IO | ControlLineFlags.J,  0,           0,                  0, 0, 0,
                // 0111
                fetchT0, fetchT1, 0,           0,           0,                  0, 0, 0,
                // 1000
                fetchT0, fetchT1, 0,           0,           0,                  0, 0, 0,
                // 1001
                fetchT0, fetchT1, 0,           0,           0,                  0, 0, 0,
                // 1010
                fetchT0, fetchT1, 0,           0,           0,                  0, 0, 0,
                // 1011
                fetchT0, fetchT1, 0,           0,           0,                  0, 0, 0,
                // 1100
                fetchT0, fetchT1, 0,           0,           0,                  0, 0, 0,
                // 1101
                fetchT0, fetchT1, 0,           0,           0,                  0, 0, 0,
                // 1110 - OUT
                fetchT0, fetchT1, ControlLineFlags.AO | ControlLineFlags.OI, 0,           0,                  0, 0, 0,
                // 1111 - HLT
                fetchT0, fetchT1, ControlLineFlags.HLT,       0,           0,                  0, 0, 0,
        };
    }

    public override void FallingEdge()
    {
        // Get instruction from 4 most significant bits in instruction register
        var instruction = instructionRegister.Value & 0xF0;
        // Shift right instruction because step only uses 3 bits
        var index = (byte)(instruction >> 1 | (stepperRegister.Value & 0b111));

        var controlLineFlags = microcode[index];
        bus.SetControleLineFlags(controlLineFlags);
    }

    public override string ToString()
    {
        return bus.ControlLine.ToString();
    }
}
