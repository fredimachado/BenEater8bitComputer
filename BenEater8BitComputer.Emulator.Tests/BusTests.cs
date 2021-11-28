using Shouldly;
using Xunit;

namespace BenEater8BitComputer.Emulator.Tests
{
    public class BusTests
    {
        private readonly Bus sut;

        public BusTests()
        {
            sut = new Bus();
        }

        [Fact]
        public void Given_ControlLineFlags_ShouldSetFlags()
        {
            // Arrange
            var flags = ControlLineFlags.CE | ControlLineFlags.RO;

            // Act
            sut.SetControleLineFlags(flags);

            // Assert
            sut.ControlLine.ShouldBe(flags);
        }

        [Theory]
        [InlineData(ControlLineFlags.CE, true)]
        [InlineData(ControlLineFlags.RO, true)]
        [InlineData(ControlLineFlags.CE | ControlLineFlags.RO, true)]
        [InlineData(ControlLineFlags.None, false)]
        [InlineData(ControlLineFlags.AI, false)]
        [InlineData(ControlLineFlags.CE | ControlLineFlags.AI, false)]
        [InlineData(ControlLineFlags.RO | ControlLineFlags.AI, false)]
        public void Given_ControlLineFlags_ShouldCheckFlags(ControlLineFlags check, bool expected)
        {
            // Arrange
            var flags = ControlLineFlags.CE | ControlLineFlags.RO;
            sut.SetControleLineFlags(flags);

            // Act
            var result = sut.HasControlLineFlags(check);

            // Assert
            result.ShouldBe(expected);
        }
    }
}
