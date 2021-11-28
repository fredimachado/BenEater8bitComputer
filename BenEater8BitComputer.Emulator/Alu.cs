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

    public bool Subtract { get; internal set; }

    public bool AluOut { get; internal set; }

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
        var a = aRegister.Value;
        var b = (byte)(Subtract ? ~bRegister.Value : bRegister.Value);
        var carryIn = Subtract ? 1 : 0;
        Sum = (byte)(a + b + carryIn);

        if (AluOut)
        {
            bus.Write(Sum);
        }
    }
}
