namespace BenEater8BitComputer.Emulator;

/// <summary>
/// The bus is used to transfer data between components
/// and also to set/read control lines
/// </summary>
public class Bus
{
    public byte Data { get;  internal set; }

    internal byte Read()
    {
        return Data;
    }

    internal void Write(byte value)
    {
        Data = value;
    }
}
