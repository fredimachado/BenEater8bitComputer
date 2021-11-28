using Shouldly;
using Xunit;

namespace BenEater8BitComputer.Emulator.Tests
{
    public class AluTests
    {
        private readonly Bus bus;
        private readonly Register aRegister;
        private readonly Register bRegister;
        private readonly Alu sut;

        public AluTests()
        {
            bus = new Bus();
            aRegister = new Register(bus);
            bRegister = new Register(bus);
            sut = new Alu(bus, aRegister, bRegister);
        }

        [Fact]
        public void Given_AAndBRegister_AndSUControlLineIsDisabled_ShouldSumValues()
        {
            // Arrange
            aRegister.Value = 6;
            bRegister.Value = 2;
            bus.SetControleLineFlags(~ControlLineFlags.SU);

            // Act
            sut.Low();

            // Assert
            sut.Sum.ShouldBe((byte)8);
        }

        [Fact]
        public void Given_AAndBRegister_AndSUControlLineIsEnabled_ShouldSubtractValues()
        {
            // Arrange
            aRegister.Value = 6;
            bRegister.Value = 2;
            bus.SetControleLineFlags(ControlLineFlags.SU);

            // Act
            sut.Low();

            // Assert
            sut.Sum.ShouldBe((byte)4);
        }

        [Fact]
        public void Given_AAndBRegister_AndControlLineIsDisabled_ShouldNotWriteResultToBus()
        {
            // Arrange
            aRegister.Value = 6;
            bRegister.Value = 2;
            bus.SetControleLineFlags(~ControlLineFlags.EO);

            // Act
            sut.Low();

            // Assert
            bus.Data.ShouldNotBe((byte)8);
        }

        [Fact]
        public void Given_AAndBRegister_AndControlLineIsEnabled_ShouldWriteResultToBus()
        {
            // Arrange
            aRegister.Value = 6;
            bRegister.Value = 2;
            bus.SetControleLineFlags(ControlLineFlags.EO);

            // Act
            sut.Low();

            // Assert
            bus.Data.ShouldBe((byte)8);
        }
    }
}
