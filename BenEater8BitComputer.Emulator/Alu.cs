namespace BenEater8BitComputer.Emulator;

/// <summary>
/// Arithmetic Logic Unit
/// </summary>
public class Alu : Component
{
    private readonly Register aRegister;
    private readonly Register bRegister;

    public Alu(Bus bus, Register aRegister, Register bRegister) : base(bus)
    {
        this.aRegister = aRegister;
        this.bRegister = bRegister;
    }

    public byte Sum { get; private set; }

    public override void Low()
    {
        Calculate();
    }

    public override void High()
    {
        Calculate();
    }

    private void Calculate()
    {
        var sub = bus.HasControlLineFlags(ControlLineFlags.SU);
        var a = aRegister.Value;
        var b = (byte)(sub ? ~bRegister.Value : bRegister.Value);
        var carryIn = sub ? 1 : 0;
        Sum = (byte)(a + b + carryIn);

        if (bus.HasControlLineFlags(ControlLineFlags.EO))
        {
            bus.Write(Sum);
        }
    }

    public override string ToString()
    {
        return $"0x{Sum:X2} ({Sum})";
    }
}
