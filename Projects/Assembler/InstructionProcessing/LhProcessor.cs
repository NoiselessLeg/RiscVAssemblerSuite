using Assembler.Common;
using Assembler.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.InstructionProcessing
{
    class LhProcessor : LoadInstructionBase
    {
        public LhProcessor(SymbolTable symTable) :
            base(symTable)
        {
        }

        protected override byte GetFunctionCode()
        {
            return 0x1;
        }
    }
}
