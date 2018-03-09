namespace Assembler.FirstPassSegmentParsing.InstructionByteSizes
{
    /// <summary>
    /// Interface that defines a class which returns
    /// how many bytes an instruction takes up in memory.
    /// This is necessary as pseudoinstructions can take up
    /// multiple bytes instead of the standard four.
    /// </summary>
    interface IInstructionByteMeasure
    {
        /// <summary>
        /// Gets the number of bytes in the instruction.
        /// </summary>
        /// <returns>The number of bytes in the instruction.</returns>
        int GetNumInstructionBytes(string[] instructionArgs);
    }
}
