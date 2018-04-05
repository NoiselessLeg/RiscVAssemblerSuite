using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.InstructionProcessing
{
    class BgeuProcessor : BranchInstructionBase
    {
        public BgeuProcessor(SymbolTable symTbl) :
            base(symTbl)
        {
        }

        protected override byte GetFunctionCode()
        {
            return 0x7;
        }
    }
}
