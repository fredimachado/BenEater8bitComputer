using Shouldly;
using Xunit;

namespace BenEater8BitComputer.Emulator.Tests
{
    public class MarTests
    {
        private readonly Bus bus;
        private readonly Mar sut;

        public MarTests()
        {
            bus = new Bus();
            sut = new Mar(bus);
        }

        [Fact]
        public void Given_DataInBus_AndMarInIsFalse_ShouldNotReadValueFromBus()
        {
            // Arrange
            byte data = 5;
            bus.Data = data;
            sut.MemoryAddressRegisterIn = false;

            // Act
            sut.RisingEdge();

            // Assert
            sut.Value.ShouldNotBe(data);
        }

        [Fact]
        public void Given_DataInBus_AndMarInIsTrue_ShouldReadValueFromBus()
        {
            // Arrange
            byte data = 5;
            bus.Data = data;
            sut.MemoryAddressRegisterIn = true;

            // Act
            sut.RisingEdge();

            // Assert
            sut.Value.ShouldBe(data);
        }

        [Fact]
        public void Given_DataInBus_ShouldOnlyRead4MostSignificantBits()
        {
            // Arrange
            byte expected = 0xA;
            byte data = 0xFA;
            bus.Data = data;
            sut.MemoryAddressRegisterIn = true;

            // Act
            sut.RisingEdge();

            // Assert
            sut.Value.ShouldBe(expected);
        }
    }
}
