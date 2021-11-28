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

    public bool CounterIn { get; internal set; }
    public bool CounterOut { get; internal set; }
    public bool CounterEnabled { get; internal set; }

    public override void Low()
    {
        if (CounterOut)
        {
            // We only write the 4 least significant bits
            var pc = (byte)(Value & 0x0F);
            bus.Write(pc);
        }
    }

    public override void RisingEdge()
    {
        if (CounterIn)
        {
            // Read data from bus and store its 4 least significant bits
            Value = (byte)(bus.Read() & 0x0F);
        }

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
