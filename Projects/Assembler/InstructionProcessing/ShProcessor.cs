namespace Assembler.InstructionProcessing
{
    class ShProcessor : StoreInstructionBase
    {
        protected override byte GetFunctionCode()
        {
            return 0x1;
        }
    }
}
