using Assembler.OutputProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Interpreter.InstructionInterpretation
{
    class JalrInterpreter : IInstructionInterpreter
    {
        public bool InterpretInstruction(int[] argList, Register[] registers, RuntimeDataSegmentAccessor dataSegment)
        {
            if (argList.Length != 3)
            {
                throw new InvalidOperationException("Malformed JALR instruction - expected three parameters; received " + argList.Length);
            }

            int rdIdx = argList[0];
            int rs1Idx = argList[1];
            int offset = argList[2];

            // trick to get the sign extended 12 bits.
            offset = (offset << 20);
            offset = (offset >> 20);

            int targetAddress = (int)((offset + registers[rs1Idx].Value) & 0xFFFFFFFE);

            int nextAddress = registers[InterpreterCommon.PC_REGISTER].Value + sizeof(int);
            registers[rdIdx].Value = nextAddress;

            registers[InterpreterCommon.PC_REGISTER].Value = targetAddress;

            return true;
        }
    }
}
