namespace BenEater8BitComputer.Emulator;

/// <summary>
/// Instruction Register
/// </summary>
public class Ir : Component
{
    public Ir(Bus bus) : base(bus)
    {
    }

    public byte Value { get; internal set; }

    public override void Reset()
    {
        Value = 0;
    }

    public override void Low()
    {
        if (bus.HasControlLineFlags(ControlLineFlags.IO))
        {
            // We only write the 4 least significant bits, which should be an address
            var data = (byte)(Value & 0x0F);
            bus.Write(data);
        }
    }

    public override void RisingEdge()
    {
        if (bus.HasControlLineFlags(ControlLineFlags.II))
        {
            Value = bus.Read();
        }
    }

    public override string ToString()
    {
        return $"0x{Value:X2} ({Value})";
    }
}
