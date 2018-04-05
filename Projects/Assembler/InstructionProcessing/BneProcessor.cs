using Assembler.Util;
using System;
using System.Collections.Generic;

namespace Assembler.InstructionProcessing
{
    class BneProcessor : BranchInstructionBase
    {
        public BneProcessor(SymbolTable symTbl):
            base(symTbl)
        {
        }

        protected override byte GetFunctionCode()
        {
            return 0x1;
        }
    }
}
