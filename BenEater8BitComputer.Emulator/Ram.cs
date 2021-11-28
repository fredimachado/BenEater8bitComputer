namespace BenEater8BitComputer.Emulator;

/// <summary>
/// Random Access Memory
/// </summary>
public class Ram : Component
{
    private readonly Mar memoryAddressRegister;

    public Ram(Bus bus, Mar memoryAccessRegister) : base(bus)
    {
        memoryAddressRegister = memoryAccessRegister;
    }

    public Dictionary<int, byte> Data { get; } = new Dictionary<int, byte>();

    // Value in memory address pointed by the memory address register
    public byte Value => Data.GetValueOrDefault(memoryAddressRegister.Value);

    public bool RamIn { get; internal set; }
    public bool RamOut { get; internal set; }

    public override void Low()
    {
        if (RamOut)
        {
            bus.Write(Value);
        }
    }

    public override void High()
    {
        if (RamIn)
        {
            Data[memoryAddressRegister.Value] = bus.Read();
        }
    }
}
