using Assembler.Common;
using Assembler.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.InstructionProcessing
{
    class SwProcessor : StoreInstructionBase
    {
        protected override byte GetFunctionCode()
        {
            return 0x2;
        }
    }
}
