using Assembler.OutputProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Interpreter.InstructionInterpretation
{
    class SbInterpreter : IInstructionInterpreter
    {
        public bool InterpretInstruction(int[] argList, Register[] registers, DataSegmentAccessor dataSegment)
        {
            if (argList.Length != 3)
            {
                throw new InvalidOperationException("Malformed SB instruction - expected three parameters; received " + argList.Length);
            }

            int rs2Idx = argList[0];
            int rs1Idx = argList[1];
            int offset = argList[2];

            int address = registers[rs1Idx].Value + offset;

            dataSegment.WriteSignedByte(address, (sbyte)registers[rs2Idx].Value);

            return false;
        }
    }
}
