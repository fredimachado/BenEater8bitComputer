using Shouldly;
using Xunit;

namespace BenEater8BitComputer.Emulator.Tests
{
    public class ComputerTests
    {
        private readonly Bus bus;
        private readonly Computer computer;

        public ComputerTests()
        {
            bus = new Bus();
            computer = new Computer(bus);
        }

        [Fact]
        public void AddTwoNumbers()
        {
            //Arrange
            byte number1 = 0x1C; // 1C (28)
            byte number2 = 0x0E; // 0E (14)
            byte expected = 42;  // 28 + 14

            // LDA will load the contents of a memory address into the A register
            computer.Ram.Data[0x00] = 0x1E; // LDA 14
            // ADD will load the contents of a memory address, sum with the
            // contents of the A register and store the result back in the A register
            computer.Ram.Data[0x01] = 0x2F; // ADD 15
            // OUT will store the contents of the A register into the Out register
            computer.Ram.Data[0x02] = 0xE0; // OUT
            // HLT will stop the clock
            computer.Ram.Data[0x03] = 0xF0; // HLT

            computer.Ram.Data[0x0E] = number1; // Value stored in memory address 0E (14)
            computer.Ram.Data[0x0F] = number2; // Value stored in memory address 0F (15)

            // Act
            Run();

            // Assert
            computer.Out.Value.ShouldBe(expected);
        }

        [Fact]
        public void AddTwoNumbers_AndSubtract()
        {
            //Arrange
            byte number1 = 0x05; // 5
            byte number2 = 0x06; // 6
            byte number3 = 0x07; // 7
            byte expected = 4; // 5 + 6 - 7

            // LDA will load the contents of a memory address into the A register
            computer.Ram.Data[0x00] = 0x1D; // LDA 15
            // ADD will load the contents of a memory address, sum with the
            // contents of the A register and store the result back in the A register
            computer.Ram.Data[0x01] = 0x2E; // ADD
            // SUB will load the contents of a memory address, subtract with the
            // contents of the A register and store the result back in the A register
            computer.Ram.Data[0x02] = 0x3F; // SUB
            // OUT will store the contents of the A register into the Out register
            computer.Ram.Data[0x03] = 0xE0; // OUT
            // HLT will stop the clock
            computer.Ram.Data[0x04] = 0xF0; // HLT

            computer.Ram.Data[0x0D] = number1; // Value stored in memory address 0D (13)
            computer.Ram.Data[0x0E] = number2; // Value stored in memory address 0E (14)
            computer.Ram.Data[0x0F] = number3; // Value stored in memory address 0F (15)

            // Act
            Run();

            // Assert
            computer.Out.Value.ShouldBe(expected);
        }

        [Fact]
        public void CountByThree()
        {
            //Arrange
            // 5 is the number of cycles an instruction needs to execute (T0 to T4)
            // (3 * 5) is the total cycles to execute the first 3 instructions (LDI 3, STA 15 and LDI 0)
            // (7 * ((3 * 5) - 1)) is the number of times we want to execute the loop (7) times the
            // total cycles of the remaining 3 instructions (ADD 15, OUT and JMP)
            // JMP only takes 4 cycles (instead of 5) to complete, so we subtract 1
            int cycles = (3 * 5) + (7 * ((3 * 5) - 1));
            byte expected = 21; // 3 * 7

            // LDI will load the immediate value of the operand (3 in this case) to the A register
            computer.Ram.Data[0x00] = 0x53; // LDI 3
            // STA will store the contents of the A register into the memory address
            // defined in the operand (15 in this case)
            computer.Ram.Data[0x01] = 0x4F; // STA 15
            // LDI will load the immediate value of the operand (0 in this case) to the A register
            computer.Ram.Data[0x02] = 0x50; // LDI 0
            // ADD will load the contents of a memory address, sum with the
            // contents of the A register and store the result back in the A register
            computer.Ram.Data[0x03] = 0x2F; // ADD 15
            // OUT will store the contents of the A register into the Out register
            computer.Ram.Data[0x04] = 0xE0; // OUT
            // JMP will set the program counter to the value in the operand (3 in this case)
            // effectively jumping back to address 3
            computer.Ram.Data[0x05] = 0x63; // JMP 3

            // Act
            Run(cycles);

            // Assert
            computer.Out.Value.ShouldBe(expected);
        }

        /// <summary>
        /// Runs the computer until it's halted or until cycles count reaches cyclesCount value
        /// Good to avoid infinite loops
        /// </summary>
        private void Run(int cyclesCount = 32)
        {
            var count = 0;
            do
            {
                computer.Clock();
                count++;
            } while (computer.IsRunning && count <= cyclesCount);
        }
    }
}
