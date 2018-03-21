using System.Collections.Generic;

namespace Assembler.CodeGeneration.InstructionGenerators
{
    /// <summary>
    /// General interface to parse an instruction.
    /// </summary>
    interface IParser
    {
        /// <summary>
        /// Parses an instruction and generates the binary code for it.
        /// </summary>
        /// <param name="currentTextAddress">The address of the instruction being parsed in the .text segment.</param>
        /// <param name="instructionArgs">An array containing the arguments of the instruction.</param>
        /// <returns>One or more 32-bit integers representing this instruction. If this interface is implemented
        /// for a pseudo-instruction, this may return more than one instruction value.</returns>
        IEnumerable<int> ParseInstruction(int currentTextAddress, string[] instructionArgs);
    }
}
