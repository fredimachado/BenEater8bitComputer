using Shouldly;
using Xunit;
using System.Collections.Generic;

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
        public void Given_RamInIsFalse_ShouldNotReadValueFromBus()
        {
            // Arrange
            byte address = 0xA;
            byte data = 5;
            mar.Value = address;
            bus.Data = data;
            sut.RamIn = false;

            // Act
            sut.High();

            // Assert
            sut.Data.GetValueOrDefault(address).ShouldNotBe(data);
        }

        [Fact]
        public void Given_RamInIsTrue_ReadsValueFromBus()
        {
            // Arrange
            byte address = 0xA;
            byte data = 5;
            mar.Value = address;
            bus.Data = data;
            sut.RamIn = true;

            // Act
            sut.High();

            // Assert
            sut.Data.GetValueOrDefault(address).ShouldBe(data);
        }

        [Fact]
        public void Given_RamOutIsFalse_WritesValueToBus()
        {
            // Arrange
            byte address = 0xA;
            byte data = 5;
            mar.Value = address;
            sut.Data[address] = data;
            sut.RamOut = false;

            // Act
            sut.Low();

            // Assert
            bus.Data.ShouldNotBe(data);
        }

        [Fact]
        public void Given_RamOutIsTrue_WritesValueToBus()
        {
            // Arrange
            byte address = 0xA;
            byte data = 5;
            mar.Value = address;
            sut.Data[address] = data;
            sut.RamOut = true;

            // Act
            sut.Low();

            // Assert
            bus.Data.ShouldBe(data);
        }
    }
}
