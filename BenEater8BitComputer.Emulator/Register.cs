namespace BenEater8BitComputer.Emulator;

/// <summary>
/// Generic 8-bit Register
/// </summary>
public class Register : Component
{
    private readonly ControlLineFlags inControl;
    private readonly ControlLineFlags outControl;

    public Register(Bus bus, ControlLineFlags inControl = ControlLineFlags.None, ControlLineFlags outControl = ControlLineFlags.None)
        : base(bus)
    {
        this.inControl = inControl;
        this.outControl = outControl;
    }

    public byte Value { get; internal set; }

    public override void Low()
    {
        if (bus.HasControlLineFlags(outControl))
        {
            bus.Write(Value);
        }
    }

    public override void RisingEdge()
    {
        if (bus.HasControlLineFlags(inControl))
        {
            Value = bus.Read();
        }
    }

    public override string ToString()
    {
        return $"0x{Value:X2} ({Value})";
    }
}
