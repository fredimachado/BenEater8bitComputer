using Shouldly;
using Xunit;

namespace BenEater8BitComputer.Emulator.Tests
{
    public class RegisterTests
    {
        private readonly Bus bus;
        private readonly Register sut;

        public RegisterTests()
        {
            bus = new Bus();
            sut = new Register(bus);
        }

        [Fact]
        public void ReadsFromBusOnRisingEdge()
        {
            // Arrange
            byte data = 42;
            bus.Data = data;

            // Act
            sut.RisingEdge();

            // Assert
            sut.Value.ShouldBe(data);
        }

        [Fact]
        public void WriteToBusOnLow()
        {
            // Arrange
            byte data = 43;
            sut.Value = data;

            // Act
            sut.Low();

            // Assert
            bus.Data.ShouldBe(data);
        }

        [Fact]
        public void CanTransferDataToAnotherRegister()
        {
            // Arrange
            byte data = 44;
            var registerB = new Register(bus);
            registerB.Value = data;

            // Act
            registerB.Low();
            sut.RisingEdge();

            // Assert
            sut.Value.ShouldBe(data);
        }
    }
}
