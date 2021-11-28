using Shouldly;
using Xunit;

namespace BenEater8BitComputer.Emulator.Tests
{
    public class RegisterTests
    {
        private readonly Bus bus;
        private readonly Register sut;
        private readonly ControlLineFlags inControl = ControlLineFlags.AI;
        private readonly ControlLineFlags outControl = ControlLineFlags.AO;

        public RegisterTests()
        {
            bus = new Bus();
            sut = new Register(bus, inControl, outControl);
        }

        [Fact]
        public void Given_DataInBus_AndInControlIsDisabled_ShouldNotReadOnRisingEdge()
        {
            // Arrange
            byte data = 42;
            bus.Data = data;
            bus.SetControleLineFlags(~inControl);

            // Act
            sut.RisingEdge();

            // Assert
            sut.Value.ShouldNotBe(data);
        }

        [Fact]
        public void Given_DataInBus_AndInControlIsEnabled_ShouldReadOnRisingEdge()
        {
            // Arrange
            byte data = 42;
            bus.Data = data;
            bus.SetControleLineFlags(inControl);

            // Act
            sut.RisingEdge();

            // Assert
            sut.Value.ShouldBe(data);
        }

        [Fact]
        public void Given_Value_AndOutControlIsDisabled_ShouldNotWriteToBusOnLow()
        {
            // Arrange
            byte data = 43;
            sut.Value = data;
            bus.SetControleLineFlags(~outControl);

            // Act
            sut.Low();

            // Assert
            bus.Data.ShouldNotBe(data);
        }

        [Fact]
        public void Given_Value_AndOutControlIsEnabled_ShouldWriteToBusOnLow()
        {
            // Arrange
            byte data = 43;
            sut.Value = data;
            bus.SetControleLineFlags(outControl);

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
            sut.Value = data;
            
            var registerB = new Register(bus, ControlLineFlags.BI);

            // Act
            bus.SetControleLineFlags(outControl);
            sut.Low();
            bus.SetControleLineFlags(ControlLineFlags.BI);
            registerB.RisingEdge();

            // Assert
            registerB.Value.ShouldBe(data);
        }
    }
}
