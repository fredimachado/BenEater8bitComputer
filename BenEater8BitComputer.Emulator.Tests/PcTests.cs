using Shouldly;
using Xunit;

namespace BenEater8BitComputer.Emulator.Tests
{
    public class PcTests
    {
        private readonly Bus bus;
        private readonly Pc sut;

        public PcTests()
        {
            bus = new Bus();
            sut = new Pc(bus);
        }

        [Fact]
        public void Give_COControlIsDisabled_ShouldWriteProgramCounterToBus()
        {
            // Arrange
            byte pcValue = 2;
            sut.Value = pcValue;
            bus.SetControleLineFlags(~ControlLineFlags.CO);

            // Act
            sut.Low();

            // Assert
            bus.Data.ShouldNotBe(pcValue);
        }

        [Fact]
        public void Give_COControlIsEnabled_ShouldWriteProgramCounterToBus()
        {
            // Arrange
            byte pcValue = 2;
            sut.Value = pcValue;
            bus.SetControleLineFlags(ControlLineFlags.CO);

            // Act
            sut.Low();

            // Assert
            bus.Data.ShouldBe(pcValue);
        }

        [Fact]
        public void Given_CEControlIsDisabled_ShouldNotIncrementCounter()
        {
            // Arrange
            sut.Value = 0;
            bus.SetControleLineFlags(~ControlLineFlags.CE);

            // Act
            sut.RisingEdge();

            // Assert
            sut.Value.ShouldBe((byte)0);
        }

        [Fact]
        public void Given_CEControlIsEnabled_ShouldIncrementCounter()
        {
            // Arrange
            sut.Value = 0;
            bus.SetControleLineFlags(ControlLineFlags.CE);

            // Act
            sut.RisingEdge();

            // Assert
            sut.Value.ShouldBe((byte)1);
        }

        [Fact]
        public void Given_CounterIs14_ShouldCountTo15()
        {
            // Arrange
            sut.Value = 14;
            bus.SetControleLineFlags(ControlLineFlags.CE);

            // Act
            sut.RisingEdge();

            // Assert
            sut.Value.ShouldBe((byte)15);
        }

        [Fact]
        public void Given_CounterIs15_ShouldResetCounter()
        {
            // Arrange
            sut.Value = 15;
            bus.SetControleLineFlags(ControlLineFlags.CE);

            // Act
            sut.RisingEdge();

            // Assert
            sut.Value.ShouldBe((byte)0);
        }

        [Fact]
        public void Given_DataInBus_AndJControlIsDisabled_ShouldNotSetCounter()
        {
            // Arrange
            byte data = 5;
            bus.Data = data;
            bus.SetControleLineFlags(~ControlLineFlags.J);

            // Act
            sut.RisingEdge();

            // Assert
            sut.Value.ShouldNotBe(data);
        }

        [Fact]
        public void Given_DataInBus_AndJControlIsEnabled_ShouldSetCounter()
        {
            // Arrange
            byte data = 5;
            bus.Data = data;
            bus.SetControleLineFlags(ControlLineFlags.J);

            // Act
            sut.RisingEdge();

            // Assert
            sut.Value.ShouldBe(data);
        }

        [Fact]
        public void Given_DataInBus_AndJControlIsEnabled_ShouldOnlySet4MostSignificantBitsToCounter()
        {
            // Arrange
            byte data = 0xF4;
            bus.Data = data;
            bus.SetControleLineFlags(ControlLineFlags.J);

            // Act
            sut.RisingEdge();

            // Assert
            sut.Value.ShouldBe((byte)4);
        }
    }
}
