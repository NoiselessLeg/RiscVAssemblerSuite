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

        /// <summary>
        /// Gets the three bit function code associated with this branch instruction that calls out
        /// what type of branch instruction this is.
        /// </summary>
        /// <returns>A three bit numeric value that tells the processor what instruction type
        /// this represents.</returns>
        protected override byte GetFunctionCode()
        {
            return 0x1;
        }
    }
}
