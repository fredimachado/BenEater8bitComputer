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
        public void SumsARegisterAndBRegister()
        {
            // Arrange
            aRegister.Value = 6;
            bRegister.Value = 2;

            // Act
            sut.Low();

            // Assert
            sut.Sum.ShouldBe((byte)8);
        }

        [Fact]
        public void SubtractsBRegisterFromARegister()
        {
            // Arrange
            aRegister.Value = 6;
            bRegister.Value = 2;
            sut.Subtract = true;

            // Act
            sut.Low();

            // Assert
            sut.Sum.ShouldBe((byte)4);
        }
    }
}
