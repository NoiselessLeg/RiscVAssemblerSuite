using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.InstructionProcessing
{
    class BltuProcessor : BranchInstructionBase
    {
        public BltuProcessor(SymbolTable symbolTable) :
            base(symbolTable)
        {
        }

        protected override byte GetFunctionCode()
        {
            return 0x6;
        }
    }
}
