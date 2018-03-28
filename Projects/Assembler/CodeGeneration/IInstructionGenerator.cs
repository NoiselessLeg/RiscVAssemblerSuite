using System.Collections.Generic;

namespace Assembler.CodeGeneration
{
    /// <summary>
    /// General interface that describes a class that generates one or more assembly 
    /// instructions for a given .text segment line.
    /// </summary>
    interface IInstructionGenerator
    {
        /// <summary>
        /// Parses an instruction and generates the binary code for it.
        /// </summary>
        /// <param name="nextTextAddress">The address of the theoretical next instruction being parsed in the .text segment.</param>
        /// <param name="instructionArgs">An array containing the arguments of the instruction.</param>
        /// <returns>One or more 32-bit integers representing this instruction. If this interface is implemented
        /// for a pseudo-instruction, this may return more than one instruction value.</returns>
        IEnumerable<int> GenerateCodeForInstruction(int nextTextAddress, string[] instructionArgs);
    }
}
