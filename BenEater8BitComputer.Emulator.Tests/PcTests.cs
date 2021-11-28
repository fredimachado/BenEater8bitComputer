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
        public void WritesProgramCounterToBus()
        {
            // Arrange
            byte pcValue = 2;
            sut.Value = pcValue;
            sut.CounterOutput = true;

            // Act
            sut.Low();

            // Assert
            bus.Data.ShouldBe(pcValue);
        }

        [Fact]
        public void DontIncrementIfCounterNotEnabled()
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
        public void IncrementsPcOnRisingEdge()
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
        public void CanCountTo15()
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
        public void CountOverThan15ShouldResetCounter()
        {
            // Arrange
            sut.Value = 15;
            sut.CounterEnabled = true;

            // Act
            sut.RisingEdge();

            // Assert
            sut.Value.ShouldBe((byte)0);
        }
    }
}
