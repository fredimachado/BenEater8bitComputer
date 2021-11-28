using Shouldly;
using Xunit;

namespace BenEater8BitComputer.Emulator.Tests
{
    public class StepperTests
    {
        private readonly Bus bus;
        private readonly Stepper sut;

        public StepperTests()
        {
            bus = new Bus();
            sut = new Stepper(bus);
        }

        [Fact]
        public void Given_RisingEdge_ShouldIncrementStep()
        {
            // Arrange
            sut.Value = 0;

            // Act
            sut.RisingEdge();

            // Assert
            sut.Value.ShouldBe((byte)1);
        }

        [Fact]
        public void Given_ValueIs4_ShouldResetToStep0()
        {
            // Arrange
            sut.Value = 4;

            // Act
            sut.RisingEdge();

            // Assert
            sut.Value.ShouldBe((byte)0);
        }
    }
}
