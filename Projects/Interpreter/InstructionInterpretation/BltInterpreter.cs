using Assembler.OutputProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Interpreter.InstructionInterpretation
{
    class BltInterpreter : IInstructionInterpreter
    {
        public bool InterpretInstruction(RuntimeContext ctx, int[] argList)
        {
            if (argList.Length != 3)
            {
                throw new InvalidOperationException("Malformed BLT instruction - expected three parameters; received " + argList.Length);
            }

            bool pcModified = false;
            int rs1Idx = argList[0];
            int rs2Idx = argList[1];
            if (ctx.RuntimeRegisters[rs1Idx].Value < ctx.RuntimeRegisters[rs2Idx].Value)
            {
                System.Diagnostics.Debug.Assert(argList[2] % 4 == 0);
                int offset = argList[2];
                ctx.RuntimeRegisters[InterpreterCommon.PC_REGISTER].Value += offset;
                pcModified = true;
            }

            return pcModified;
        }
    }
}
