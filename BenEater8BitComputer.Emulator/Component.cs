namespace BenEater8BitComputer.Emulator;

/// <summary>
/// Used to create components connected to the bus
/// </summary>
public abstract class Component
{
    protected readonly Bus bus;

    public Component(Bus bus)
    {
        this.bus = bus;
    }

    public virtual void Reset() {}
    public virtual void Low() {}
    public virtual void RisingEdge() {}
    public virtual void High() {}
    public virtual void FallingEdge() {}
}
