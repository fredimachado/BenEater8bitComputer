namespace BenEater8BitComputer.Emulator;

/// <summary>
/// Program Counter
/// 4-bit counter
/// </summary>
public class Pc : Component
{
    public Pc(Bus bus) : base(bus)
    {
    }

    public byte Value { get; internal set; }

    public override void Low()
    {
        if (bus.HasControlLineFlags(ControlLineFlags.CO))
        {
            // We only write the 4 least significant bits
            var pc = (byte)(Value & 0x0F);
            bus.Write(pc);
        }
    }

    public override void RisingEdge()
    {
        if (bus.HasControlLineFlags(ControlLineFlags.J))
        {
            // Read data from bus and store its 4 least significant bits
            Value = (byte)(bus.Read() & 0x0F);
        }

        if (bus.HasControlLineFlags(ControlLineFlags.CE))
        {
            // Increment Program Counter
            Value++;

            // Simulating 4 bit counter, so we only count up to 15
            if (Value > 0x0F)
            {
                Value = 0;
            }
        }
    }

    public override string ToString()
    {
        return $"0x{Value:X2} ({Value})";
    }
}
