using Assembler.OutputProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Interpreter.InstructionInterpretation
{
    class LuiInterpreter : IInstructionInterpreter
    {
        public bool InterpretInstruction(int[] argList, Register[] registers, RuntimeDataSegmentAccessor dataSegment)
        {
            if (argList.Length != 2)
            {
                throw new InvalidOperationException("Malformed LUI instruction - expected two parameters; received " + argList.Length);
            }

            int rdIdx = argList[0];
            int immediate = (argList[1] << 12);
            registers[rdIdx].Value = 0;
            registers[rdIdx].Value = (registers[rdIdx].Value | immediate);

            return false;
        }
    }
}
