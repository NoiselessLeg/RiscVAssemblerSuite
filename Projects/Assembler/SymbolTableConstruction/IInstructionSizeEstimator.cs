namespace Assembler.SymbolTableConstruction
{
    /// <summary>
    /// General interface that describes a class that generates one or more assembly 
    /// instructions for a given .text segment line.
    /// </summary>
    interface IInstructionSizeEstimator
    {
        /// <summary>
        /// Determines how many instructions are generated via a pseudo-instruction.
        /// </summary>
        /// <param name="nextTextAddress">The address of the theoretical next instruction being parsed in the .text segment.</param>
        /// <param name="instructionArgs">An array containing the arguments of the instruction.</param>
        /// <returns>An integer representing how many instructions will be generated for a line of assembly.</returns>
        int GetNumGeneratedInstructions(int nextTextAddress, string[] instructionArgs);
    }
}
