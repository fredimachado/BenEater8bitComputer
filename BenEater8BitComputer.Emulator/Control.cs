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

        var fetchT0 = C.MI | C.CO;
        var fetchT1 = C.RO | C.II | C.CE;

        microcode = new C[]
        {
                // 0000 - NOP
                fetchT0, fetchT1, 0,           0,           0,                  0, 0, 0,
                // 0001 - LDA
                fetchT0, fetchT1, C.IO | C.MI, C.RO | C.AI, 0,                  0, 0, 0,
                // 0010 - ADD
                fetchT0, fetchT1, C.IO | C.MI, C.RO | C.BI, C.EO | C.AI,        0, 0, 0,
                // 0011 - SUB
                fetchT0, fetchT1, C.IO | C.MI, C.RO | C.BI, C.EO | C.AI | C.SU, 0, 0, 0,
                // 0100 - STA
                fetchT0, fetchT1, C.IO | C.MI, C.AO | C.RI, 0,                  0, 0, 0,
                // 0101 - LDI
                fetchT0, fetchT1, C.IO | C.AI, 0,           0,                  0, 0, 0,
                // 0110 - JMP
                fetchT0, fetchT1, C.IO | C.J,  0,           0,                  0, 0, 0,
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
                fetchT0, fetchT1, C.AO | C.OI, 0,           0,                  0, 0, 0,
                // 1111 - HLT
                fetchT0, fetchT1, C.HLT,       0,           0,                  0, 0, 0,
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
