﻿using Assembler.Common;
using Assembler.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.InstructionProcessing
{
    class SbProcessor : StoreInstructionBase
    {
        protected override byte GetFunctionCode()
        {
            return 0x0;
        }
    }
}
