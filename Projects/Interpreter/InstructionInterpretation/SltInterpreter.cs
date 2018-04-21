using Assembler.OutputProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Interpreter.InstructionInterpretation
{
    class SltInterpreter : IInstructionInterpreter
    {
        public bool InterpretInstruction(int[] argList, Register[] registers, RuntimeDataSegmentAccessor dataSegment)
        {
            if (argList.Length != 3)
            {
                throw new InvalidOperationException("Malformed SLLI instruction - expected three parameters; received " + argList.Length);
            }

            int rdIdx = argList[0];
            int rs1Idx = argList[1];
            int rs2Idx = argList[2];

            if (registers[rs1Idx].Value < registers[rs2Idx].Value)
            {
                registers[rdIdx].Value = 1;
            }
            else
            {
                registers[rdIdx].Value = 0;
            }

            return false;
        }
    }
}
