﻿using Assembler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.InstructionProcessing
{
    class LbuProcessor : LoadInstructionBase
    {
        public LbuProcessor(SymbolTable symTbl) :
            base(symTbl)
        {
        }

        /// <summary>
        /// Gets the three bit function code associated with this branch instruction that calls out
        /// what type of load instruction this is.
        /// </summary>
        /// <returns>A three bit numeric value that tells the processor what instruction type
        /// this represents.</returns>
        protected override byte GetFunctionCode()
        {
            return 0x4;
        }
    }
}
