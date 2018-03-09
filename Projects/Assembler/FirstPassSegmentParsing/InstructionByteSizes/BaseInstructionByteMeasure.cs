namespace Assembler.FirstPassSegmentParsing.InstructionByteSizes
{
    /// <summary>
    /// Base class for instruction byte sizes. This will be what most instructions
    /// calculate out to; however, some instructions need more in-depth parsing
    /// based on their arguments.
    /// </summary>
    class BaseInstructionByteMeasure : IInstructionByteMeasure
    {
        /// <summary>
        /// Returns the size of most instructions, in bytes.
        /// </summary>
        /// <returns>Always returns the standard instruction size, 4 bytes.</returns>
        public virtual int GetNumInstructionBytes(string[] instructionArgs)
        {
            return STANDARD_INSTRUCTION_SIZE;
        }

        private const int STANDARD_INSTRUCTION_SIZE = 4;
    }
}
