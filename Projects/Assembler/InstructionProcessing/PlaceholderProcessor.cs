using Assembler.Common;
using System;
using System.Collections.Generic;

namespace Assembler.InstructionProcessing
{
    class PlaceholderProcessor : BaseInstructionProcessor
    {
        public PlaceholderProcessor(string instName)
        {
            m_InstName = instName;
        }

        /// <summary>
        /// Parses an instruction and generates the binary code for it.
        /// </summary>
        /// <param name="address">The address of the instruction being parsed in the .text segment.</param>
        /// <param name="args">An array containing the arguments of the instruction.</param>
        /// <returns>One or more 32-bit integers representing this instruction. If this interface is implemented
        /// for a pseudo-instruction, this may return more than one instruction value.</returns>
        public override IEnumerable<int> GenerateCodeForInstruction(int address, string[] args)
        {
            throw new NotImplementedException(m_InstName + " instruction is not yet implemented.");
        }

        private readonly string m_InstName;
    }
}
