using C = BenEater8BitComputer.Emulator.ControlLineFlags;

using Shouldly;
using Xunit;

namespace BenEater8BitComputer.Emulator.Tests
{
    public class ControlTests
    {
        private readonly Bus bus;
        private readonly Ir ir;
        private readonly Stepper stepper;
        private readonly Control sut;

        public ControlTests()
        {
            bus = new Bus();
            ir = new Ir(bus);
            stepper = new Stepper(bus);
            sut = new Control(bus, ir, stepper);
        }

        public static TheoryData<byte> GetInstructions()
        {
            return new TheoryData<byte>
            {
                0b0000_0000, // NOP
                0b0001_0000, // LDA
                0b0010_0000, // ADD
                0b0011_0000, // SUB
                0b0100_0000, // STA
                0b0101_0000, // LDI
                0b0110_0000, // JMP
                0b0111_0000, // NOP
                0b1000_0000, // NOP
                0b1001_0000, // NOP
                0b1010_0000, // NOP
                0b1011_0000, // NOP
                0b1100_0000, // NOP
                0b1101_0000, // NOP
                0b1110_0000, // OUT
                0b1111_0000, // HLT
            };
        }

        [Theory]
        [MemberData(nameof(GetInstructions))]
        public void Given_StepT0_AndAnyInstruction_ShouldSetControlLines(byte instruction)
        {
            // Arrange
            ir.Value = instruction;
            stepper.Value = 0;

            // Act
            sut.FallingEdge();

            // Assert
            bus.ControlLine.ShouldBe(ControlLineFlags.MI | ControlLineFlags.CO);
        }

        [Theory]
        [MemberData(nameof(GetInstructions))]
        public void Given_StepT1_AndAnyInstruction_ShouldSetControlLines(byte instruction)
        {
            // Arrange
            ir.Value = instruction;
            stepper.Value = 1;

            // Act
            sut.FallingEdge();

            // Assert
            bus.ControlLine.ShouldBe(ControlLineFlags.RO | ControlLineFlags.II | ControlLineFlags.CE);
        }

        [Fact]
        public void Given_LDAInstruction_AndStepT2_ShouldSetControlLines()
        {
            // Arrange
            ir.Value = 0b0001_0000;
            stepper.Value = 2;

            // Act
            sut.FallingEdge();

            // Assert
            bus.ControlLine.ShouldBe(ControlLineFlags.IO | ControlLineFlags.MI);
        }

        [Fact]
        public void Given_LDAInstruction_AndStepT3_ShouldSetControlLines()
        {
            // Arrange
            ir.Value = 0b0001_0000;
            stepper.Value = 3;

            // Act
            sut.FallingEdge();

            // Assert
            bus.ControlLine.ShouldBe(ControlLineFlags.RO | ControlLineFlags.AI);
        }

        [Fact]
        public void Given_LDAInstruction_AndLastStep_ShouldNotHaveControlLinesSet()
        {
            // Arrange
            ir.Value = 0b0001_0000;
            stepper.Value = 4;

            // Act
            sut.FallingEdge();

            // Assert
            bus.ControlLine.ShouldBe(ControlLineFlags.None);
        }
    }
}
