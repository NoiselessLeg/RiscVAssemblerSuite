using Assembler.Util;
using System;
using System.Collections.Generic;

namespace Assembler.InstructionProcessing
{
    class BeqProcessor : BranchInstructionBase
    {
        public BeqProcessor(SymbolTable symbolTable) :
            base(symbolTable)
        {
        }

        protected override byte GetFunctionCode()
        {
            return 0x0;
        }
    }
}
