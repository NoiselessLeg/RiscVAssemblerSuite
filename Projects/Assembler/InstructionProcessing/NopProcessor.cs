using Assembler.Common;
using System;
using System.Collections.Generic;

namespace Assembler.InstructionProcessing
{
    class NopProcessor : BaseInstructionProcessor
    {
        /// <summary>
        /// Parses an instruction and generates the binary code for it.
        /// </summary>
        /// <param name="address">The address of the instruction being parsed in the .text segment.</param>
        /// <param name="args">An array containing the arguments of the instruction. Should be empty.</param>
        /// <returns>One or more 32-bit integers representing this instruction. If this interface is implemented
        /// for a pseudo-instruction, this may return more than one instruction value.</returns>
        public override IEnumerable<int> GenerateCodeForInstruction(int address, string[] instructionArgs)
        {
            // we expect no arguments. if not, throw an ArgumentException
            if (instructionArgs.Length != 0)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 0, received " + instructionArgs.Length + '.');
            }
            var delegateParser = new AddiProcessor();
            return delegateParser.GenerateCodeForInstruction(address, new string[] { "x0", "x0", "0" });
        }
    }
}
