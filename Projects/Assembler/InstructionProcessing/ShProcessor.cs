namespace Assembler.InstructionProcessing
{
    class ShProcessor : StoreInstructionBase
    {
        /// <summary>
        /// Gets the three bit function code associated with this branch instruction that calls out
        /// what type of store instruction this is.
        /// </summary>
        /// <returns>A three bit numeric value that tells the processor what instruction type
        /// this represents.</returns>
        protected override byte GetFunctionCode()
        {
            return 0x1;
        }
    }
}
