using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.InstructionProcessing
{
    class BltzProcessor : BltProcessor
    {
        public BltzProcessor(SymbolTable symTbl) :
            base(symTbl)
        {
        }

        public override IEnumerable<int> GenerateCodeForInstruction(int address, string[] instructionArgs)
        {
            if (instructionArgs.Length != 2)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 2, received " + instructionArgs.Length + '.');
            }

            return base.GenerateCodeForInstruction(address, new string[] { "x0", instructionArgs[0], instructionArgs[1] });
        }
    }
}
