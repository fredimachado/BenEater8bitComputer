using BenEater8BitComputer.Compiler;
using BenEater8BitComputer.Emulator;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BenEater8BitComputer.Compiler.Tests
{
    public class InstructionMapTests
    {
        [Fact]
        public void ShouldMapAllImplementedInstructions()
        {
            var opcodes = InstructionMap.Opcodes;

            for (int i = 0; i < 16; i++)
            {
                // Check T2 (0b010)
                var index = i << 3 | 0b010;
                var microcode = Control.Microcode[index];

                // If not None, than it is implemented
                if (microcode != ControlLineFlags.None)
                {
                    opcodes.FirstOrDefault(x => x.ByteCode == i)
                        .ShouldNotBeNull($"Missing opcode information for instruction {i}");
                }
            }
        }
    }
}
