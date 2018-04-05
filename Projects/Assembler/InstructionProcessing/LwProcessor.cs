﻿using Assembler.Common;
using Assembler.Util;
using System;
using System.Collections.Generic;

namespace Assembler.InstructionProcessing
{
    class LwProcessor : LoadInstructionBase
    {
        public LwProcessor(SymbolTable symTable) :
            base(symTable)
        {
        }

        protected override byte GetFunctionCode()
        {
            return 0x2;
        }
    }
}
