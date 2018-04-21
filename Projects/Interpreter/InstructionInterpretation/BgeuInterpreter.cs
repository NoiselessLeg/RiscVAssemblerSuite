using Assembler.OutputProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Interpreter.InstructionInterpretation
{
    class BgeuInterpreter : IInstructionInterpreter
    {
        public bool InterpretInstruction(int[] argList, Register[] registers, RuntimeDataSegmentAccessor dataSegment)
        {
            if (argList.Length != 3)
            {
                throw new InvalidOperationException("Malformed BGTU instruction - expected three parameters; received " + argList.Length);
            }

            bool pcModified = false;
            int rs1Idx = argList[0];
            int rs2Idx = argList[1];
            if ((uint)registers[rs1Idx].Value >= (uint)registers[rs2Idx].Value)
            {
                System.Diagnostics.Debug.Assert(argList[2] % 4 == 0);
                int offset = argList[2];
                registers[InterpreterCommon.PC_REGISTER].Value += offset;
                pcModified = true;
            }

            return pcModified;
        }
    }
}
