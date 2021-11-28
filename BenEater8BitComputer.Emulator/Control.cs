namespace BenEater8BitComputer.Emulator;

/// <summary>
/// Control Logic
/// Contains microcode for each instruction and sets the correct control lines for each step (T0..T4)
/// </summary>
public class Control : Component
{
    public Control(Bus bus) : base(bus)
    {
    }
}
