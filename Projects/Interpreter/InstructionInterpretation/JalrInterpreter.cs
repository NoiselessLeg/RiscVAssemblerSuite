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
        public bool InterpretInstruction(RuntimeContext ctx, int[] argList)
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

            int targetAddress = (int)((offset + ctx.RuntimeRegisters[rs1Idx].Value) & 0xFFFFFFFE);

            int nextAddress = ctx.RuntimeRegisters[InterpreterCommon.PC_REGISTER].Value + sizeof(int);
            ctx.RuntimeRegisters[rdIdx].Value = nextAddress;

            ctx.RuntimeRegisters[InterpreterCommon.PC_REGISTER].Value = targetAddress;

            return true;
        }
    }
}
