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

    public bool CounterOutput { get; set; }
    public bool CounterEnabled { get; set; }

    public override void Low()
    {
        if (CounterOutput)
        {
            // We only write the 4 least significant bits
            var pc = (byte)(Value & 0x0F);
            bus.Write(pc);
        }
    }

    public override void RisingEdge()
    {
        if (CounterEnabled)
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
}
