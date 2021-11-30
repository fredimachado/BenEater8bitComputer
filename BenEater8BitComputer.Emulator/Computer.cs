using System.Collections.Immutable;

namespace BenEater8BitComputer.Emulator;

public class Computer
{
    private readonly Bus bus;
    private readonly ImmutableArray<Component> components;

    public Register A { get; }
    public Register B { get; }
    public Ir Ir { get; }
    public Stepper Stepper { get; }
    public Pc Pc { get; }
    public Mar Mar { get; }
    public Control Control { get; }
    public Alu Alu { get; }
    public Ram Ram { get; }
    public Register Out { get; }

    public Computer(Bus bus, params Component[] extraComponents)
    {
        this.bus = bus;

        A = new Register(bus, ControlLineFlags.AI, ControlLineFlags.AO);
        B = new Register(bus, ControlLineFlags.BI, ControlLineFlags.None);
        Ir = new Ir(bus);
        Stepper = new Stepper(bus);
        Pc = new Pc(bus);
        Mar = new Mar(bus);
        Control = new Control(bus, Ir, Stepper);
        Alu = new Alu(bus, A, B);
        Ram = new Ram(bus, Mar);
        Out = new Register(bus, ControlLineFlags.OI, ControlLineFlags.None);
        var components = new Component[]
        {
            A,
            B,
            Ir,
            Stepper,
            Pc,
            Mar,
            Control,
            Alu,
            Ram,
            Out,
        };
        this.components = components
            .Concat(extraComponents)
            .ToImmutableArray();
    }

    public bool IsRunning => !bus.HasControlLineFlags(ControlLineFlags.HLT);

    public void Reset()
    {
        foreach (var component in components)
        {
            component.Reset();
        }

        GoLow();
    }

    public void Clock()
    {
        if (bus.HasControlLineFlags(ControlLineFlags.HLT))
        {
            return;
        }

        GoLow();
        GoHigh();
    }

    private void GoLow()
    {
        foreach (var component in components)
        {
            component.FallingEdge();
        }
        foreach (var component in components)
        {
            component.Low();
        }
    }

    private void GoHigh()
    {
        foreach (var component in components)
        {
            component.RisingEdge();
        }
        foreach (var component in components)
        {
            component.High();
        }
    }
}
