using Assembler.OutputProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Interpreter.InstructionInterpretation
{
    class SwInterpreter : IInstructionInterpreter
    {
        public bool InterpretInstruction(RuntimeContext ctx, int[] argList)
        {
            if (argList.Length != 3)
            {
                throw new InvalidOperationException("Malformed SW instruction - expected three parameters; received " + argList.Length);
            }

            int rs2Idx = argList[0];
            int rs1Idx = argList[1];
            int offset = argList[2];

            int address = ctx.RuntimeRegisters[rs1Idx].Value + offset;

            ctx.WriteWord(address, ctx.RuntimeRegisters[rs2Idx].Value);

            return false;
        }
    }
}
