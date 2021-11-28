namespace BenEater8BitComputer.Emulator;

/// <summary>
/// Step Counter
/// T0..T4
/// </summary>
public class Stepper : Component
{
    public Stepper(Bus bus) : base(bus)
    {
    }

    public byte Value { get; internal set; }

    public override void RisingEdge()
    {
        Value++;

        // Count to 4
        if (Value > 4)
        {
            Value = 0;
        }
    }
}
