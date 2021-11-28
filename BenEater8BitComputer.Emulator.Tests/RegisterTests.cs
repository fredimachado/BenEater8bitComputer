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
        public void Given_DataInBus_AndRegisterInIsFalse_ShouldNotReadOnRisingEdge()
        {
            // Arrange
            byte data = 42;
            bus.Data = data;
            sut.RegisterIn = false;

            // Act
            sut.RisingEdge();

            // Assert
            sut.Value.ShouldNotBe(data);
        }

        [Fact]
        public void Given_DataInBus_AndRegisterInIsTrue_ShouldReadOnRisingEdge()
        {
            // Arrange
            byte data = 42;
            bus.Data = data;
            sut.RegisterIn = true;

            // Act
            sut.RisingEdge();

            // Assert
            sut.Value.ShouldBe(data);
        }

        [Fact]
        public void Given_Value_AndRegisterOutIsFalse_ShouldNotWriteToBusOnLow()
        {
            // Arrange
            byte data = 43;
            sut.Value = data;
            sut.RegisterOut = false;

            // Act
            sut.Low();

            // Assert
            bus.Data.ShouldNotBe(data);
        }

        [Fact]
        public void Given_Value_AndRegisterOutIsTrue_ShouldWriteToBusOnLow()
        {
            // Arrange
            byte data = 43;
            sut.Value = data;
            sut.RegisterOut = true;

            // Act
            sut.Low();

            // Assert
            bus.Data.ShouldBe(data);
        }

        [Fact]
        public void Given_OutputRegister_AndRegisterInIsTrue_ShouldTransferData()
        {
            // Arrange
            byte data = 44;
            var registerB = new Register(bus);
            registerB.Value = data;
            registerB.RegisterOut = true;

            sut.RegisterIn = true;

            // Act
            registerB.Low();
            sut.RisingEdge();

            // Assert
            sut.Value.ShouldBe(data);
        }
    }
}
