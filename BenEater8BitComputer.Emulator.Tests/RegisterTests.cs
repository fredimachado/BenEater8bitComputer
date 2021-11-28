using Shouldly;
using Xunit;

namespace BenEater8BitComputer.Emulator.Tests
{
    public class RegisterTests
    {
        [Fact]
        public void ReadsFromBusOnRisingEdge()
        {
            // Arrange
            byte data = 42;
            var bus = new Bus();
            bus.Data = data;
            var sut = new Register(bus);

            // Act
            sut.RisingEdge();

            // Assert
            sut.Value.ShouldBe(data);
        }

        [Fact]
        public void WriteToBusOnLow()
        {
            // Arrange
            byte data = 42;
            var bus = new Bus();
            var sut = new Register(bus);
            sut.Value = data;

            // Act
            sut.Low();

            // Assert
            bus.Data.ShouldBe(data);
        }
    }
}
