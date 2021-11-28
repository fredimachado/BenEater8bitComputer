namespace BenEater8BitComputer.Emulator;

/// <summary>
/// Generic 8-bit Register
/// </summary>
public class Register : Component
{
    public Register(Bus bus) : base(bus)
    {
    }

    public byte Value { get; internal set; }

    public override void Low()
    {
        bus.Write(Value);
    }

    public override void RisingEdge()
    {
        Value = bus.Read();
    }
}
