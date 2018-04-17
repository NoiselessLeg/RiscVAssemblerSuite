using Assembler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.InstructionProcessing
{
    class BltzProcessor : BltProcessor
    {
        public BltzProcessor(SymbolTable symTbl) :
            base(symTbl)
        {
        }

        /// <summary>
        /// Parses an instruction and generates the binary code for it.
        /// </summary>
        /// <param name="address">The address of the instruction being parsed in the .text segment.</param>
        /// <param name="instructionArgs">An array containing the arguments of the instruction.</param>
        /// <returns>One or more 32-bit integers representing this instruction. If this interface is implemented
        /// for a pseudo-instruction, this may return more than one instruction value.</returns>
        public override IEnumerable<int> GenerateCodeForInstruction(int address, string[] instructionArgs)
        {
            if (instructionArgs.Length != 2)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 2, received " + instructionArgs.Length + '.');
            }

            return base.GenerateCodeForInstruction(address, new string[] { "x0", instructionArgs[0], instructionArgs[1] });
        }
    }
}
