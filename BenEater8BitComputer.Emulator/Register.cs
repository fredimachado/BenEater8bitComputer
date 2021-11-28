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

    public bool RegisterIn { get; internal set; }
    public bool RegisterOut { get; internal set; }

    public override void Low()
    {
        if (RegisterOut)
        {
            bus.Write(Value);
        }
    }

    public override void RisingEdge()
    {
        if (RegisterIn)
        {
            Value = bus.Read();
        }
    }
}
