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
        public void Give_CounterOutIsTrue_ShouldWriteProgramCounterToBus()
        {
            // Arrange
            byte pcValue = 2;
            sut.Value = pcValue;
            sut.CounterOut = true;

            // Act
            sut.Low();

            // Assert
            bus.Data.ShouldBe(pcValue);
        }

        [Fact]
        public void Given_CounterEnabledIsFalse_ShouldNotIncrementCounter()
        {
            // Arrange
            sut.Value = 0;
            sut.CounterEnabled = false;

            // Act
            sut.RisingEdge();

            // Assert
            sut.Value.ShouldBe((byte)0);
        }

        [Fact]
        public void Given_CounterEnabledIsTrue_ShouldIncrementCounter()
        {
            // Arrange
            sut.Value = 0;
            sut.CounterEnabled = true;

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
            sut.CounterEnabled = true;

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
            sut.CounterEnabled = true;

            // Act
            sut.RisingEdge();

            // Assert
            sut.Value.ShouldBe((byte)0);
        }

        [Fact]
        public void Given_DataInBus_AndCounterInIsFalse_ShouldNotSetCounter()
        {
            // Arrange
            byte data = 5;
            bus.Data = data;
            sut.CounterIn = false;

            // Act
            sut.RisingEdge();

            // Assert
            sut.Value.ShouldNotBe(data);
        }

        [Fact]
        public void Given_DataInBus_AndCounterInIsTrue_ShouldSetCounter()
        {
            // Arrange
            byte data = 5;
            bus.Data = data;
            sut.CounterIn = true;

            // Act
            sut.RisingEdge();

            // Assert
            sut.Value.ShouldBe(data);
        }

        [Fact]
        public void Given_DataInBus_AndCounterInIsTrue_ShouldOnlySet4MostSignificantBitsToCounter()
        {
            // Arrange
            byte data = 0xF4;
            bus.Data = data;
            sut.CounterIn = true;

            // Act
            sut.RisingEdge();

            // Assert
            sut.Value.ShouldBe((byte)4);
        }
    }
}
