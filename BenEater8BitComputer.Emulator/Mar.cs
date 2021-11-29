namespace BenEater8BitComputer.Emulator;

/// <summary>
/// Memory Address Register
/// 4-bit register
/// </summary>
public class Mar : Component
{
    public Mar(Bus bus) : base(bus)
    {
    }

    public byte Value { get; internal set; }

    public override void RisingEdge()
    {
        if (bus.HasControlLineFlags(ControlLineFlags.MI))
        {
            // Read data from bus and store its 4 least significant bits (since this is a 4 bit register)
            Value = (byte)(bus.Read() & 0x0F);
        }
    }

    public override string ToString()
    {
        return $"0x{Value:X2} ({Value})";
    }
}
