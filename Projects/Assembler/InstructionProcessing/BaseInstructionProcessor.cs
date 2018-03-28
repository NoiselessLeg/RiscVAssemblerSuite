using Assembler.CodeGeneration;
using System.Collections.Generic;

namespace Assembler.InstructionProcessing
{
    abstract class BaseInstructionProcessor : IInstructionGenerator, IInstructionSizeEstimator
    {
        /// <summary>
        /// Parses an instruction and generates the binary code for it.
        /// </summary>
        /// <param name="nextTextAddress">The address of the theoretical next instruction being parsed in the .text segment.</param>
        /// <param name="instructionArgs">An array containing the arguments of the instruction.</param>
        /// <returns>One or more 32-bit integers representing this instruction. If this interface is implemented
        /// for a pseudo-instruction, this may return more than one instruction value.</returns>
        public abstract IEnumerable<int> GenerateCodeForInstruction(int nextTextAddress, string[] instructionArgs);

        /// <summary>
        /// Determines how many instructions are generated via a pseudo-instruction. The default implementation assumes
        /// that only one instruction will be returned.
        /// </summary>
        /// <param name="nextTextAddress">The address of the theoretical next instruction being parsed in the .text segment.</param>
        /// <param name="instructionArgs">An array containing the arguments of the instruction.</param>
        /// <returns>An integer representing how many instructions will be generated for a line of assembly.</returns>
        public virtual int GetNumGeneratedInstructions(int nextTextAddress, string[] instructionArgs)
        {
            return 1;
        }
    }
}
