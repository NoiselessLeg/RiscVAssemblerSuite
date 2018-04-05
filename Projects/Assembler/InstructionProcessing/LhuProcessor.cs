using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.InstructionProcessing
{
    class LhuProcessor : LoadInstructionBase
    {
        public LhuProcessor(SymbolTable symTbl) : 
            base(symTbl)
        {
        }

        protected override byte GetFunctionCode()
        {
            return 0x5;
        }
    }
}
