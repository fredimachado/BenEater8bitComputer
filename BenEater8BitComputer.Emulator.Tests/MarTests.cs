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
        public void ReadsValueFromBus()
        {
            // Arrange
            byte data = 5;
            bus.Data = data;

            // Act
            sut.RisingEdge();

            // Assert
            sut.Value.ShouldBe(data);
        }

        [Fact]
        public void OnlyReads4MostSignificantBits()
        {
            // Arrange
            byte expected = 0xA;
            byte data = 0xFA;
            bus.Data = data;

            // Act
            sut.RisingEdge();

            // Assert
            sut.Value.ShouldBe(expected);
        }
    }
}
