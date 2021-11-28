using Shouldly;
using Xunit;

namespace BenEater8BitComputer.Emulator.Tests
{
    public class RamTests
    {
        private readonly Bus bus;
        private readonly Mar mar;
        private readonly Ram sut;

        public RamTests()
        {
            bus = new Bus();
            mar = new Mar(bus);
            sut = new Ram(bus, mar);
        }

        [Fact]
        public void ReadsValueFromBus()
        {
            // Arrange
            byte address = 0xA;
            byte data = 5;
            mar.Value = address;
            bus.Data = data;

            // Act
            sut.High();

            // Assert
            sut.Data[address].ShouldBe(data);
        }

        [Fact]
        public void WritesValueToBus()
        {
            // Arrange
            byte address = 0xA;
            byte data = 5;
            mar.Value = address;
            sut.Data[address] = data;

            // Act
            sut.Low();

            // Assert
            bus.Data = data;
        }
    }
}
