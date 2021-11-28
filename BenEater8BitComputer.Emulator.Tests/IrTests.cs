using Shouldly;
using Xunit;

namespace BenEater8BitComputer.Emulator.Tests
{
    public class IrTests
    {
        private readonly Bus bus;
        private readonly Ir sut;

        public IrTests()
        {
            bus = new Bus();
            sut = new Ir(bus);
        }

        [Fact]
        public void Given_DataInBus_AndControlLineIsDisabled_ShouldNotReadValueFromBus()
        {
            // Arrange
            byte data = 5;
            bus.Data = data;
            bus.SetControleLineFlags(~ControlLineFlags.II);

            // Act
            sut.RisingEdge();

            // Assert
            sut.Value.ShouldNotBe(data);
        }

        [Fact]
        public void Given_DataInBus_AndControlLineIsEnabled_ShouldReadValueFromBus()
        {
            // Arrange
            byte data = 5;
            bus.Data = data;
            bus.SetControleLineFlags(ControlLineFlags.II);

            // Act
            sut.RisingEdge();

            // Assert
            sut.Value.ShouldBe(data);
        }

        [Fact]
        public void Given_IOControlIsDisabled_ShouldNotWriteValueToBus()
        {
            // Arrange
            byte expected = 0xA;
            byte data = 0xFA;
            sut.Value = data;
            bus.SetControleLineFlags(~ControlLineFlags.IO);

            // Act
            sut.Low();

            // Assert
            bus.Data.ShouldNotBe(expected);
        }

        [Fact]
        public void Given_IOControlIsEnabled_ShouldWrite4MostSignificantBitsToBus()
        {
            // Arrange
            byte expected = 0xA;
            byte data = 0xFA;
            sut.Value = data;
            bus.SetControleLineFlags(ControlLineFlags.IO);

            // Act
            sut.Low();

            // Assert
            bus.Data.ShouldBe(expected);
        }
    }
}
