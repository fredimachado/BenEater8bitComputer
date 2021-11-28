namespace BenEater8BitComputer.Emulator;

/// <summary>
/// The bus is used to transfer data between components
/// and also to set/read control lines
/// </summary>
public class Bus
{
    public ControlLineFlags ControlLine { get; private set; }

    public byte Data { get;  internal set; }

    internal byte Read()
    {
        return Data;
    }

    internal void Write(byte value)
    {
        Data = value;
    }

    public bool HasControlLineFlags(ControlLineFlags flags)
    {
        return ControlLine.HasFlag(flags) && flags != ControlLineFlags.None;
    }

    public void SetControleLineFlags(ControlLineFlags flags)
    {
        ControlLine = flags;
    }
}

[Flags]
public enum ControlLineFlags
{
    None,
    /// <summary>
    /// Halt
    /// </summary>
    HLT = 0b1000000000000000,
    /// <summary>
    /// Memory Address Register In
    /// </summary>
    MI = 0b0100000000000000,
    /// <summary>
    /// RAM In
    /// </summary>
    RI = 0b0010000000000000,
    /// <summary>
    /// RAM Out
    /// </summary>
    RO = 0b0001000000000000,
    /// <summary>
    /// Instruction Register Out
    /// </summary>
    IO = 0b0000100000000000,
    /// <summary>
    /// Instruction Register In
    /// </summary>
    II = 0b0000010000000000,
    /// <summary>
    /// A Register In
    /// </summary>
    AI = 0b0000001000000000,
    /// <summary>
    /// A Register Out
    /// </summary>
    AO = 0b0000000100000000,
    /// <summary>
    /// ALU Out
    /// </summary>
    EO = 0b0000000010000000,
    /// <summary>
    /// Subtract
    /// </summary>
    SU = 0b0000000001000000,
    /// <summary>
    /// B Register In
    /// </summary>
    BI = 0b0000000000100000,
    /// <summary>
    /// Output In
    /// </summary>
    OI = 0b0000000000010000,
    /// <summary>
    /// Counter Enabled
    /// </summary>
    CE = 0b0000000000001000,
    /// <summary>
    /// Counter Out
    /// </summary>
    CO = 0b0000000000000100,
    /// <summary>
    /// Jump
    /// </summary>
    J = 0b0000000000000010,
}