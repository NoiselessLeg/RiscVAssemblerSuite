using Assembler.OutputProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Interpreter.InstructionInterpretation
{
    class JalInterpreter : IInstructionInterpreter
    {
        public bool InterpretInstruction(RuntimeContext ctx, int[] argList)
        {
            if (argList.Length != 2)
            {
                throw new InvalidOperationException("Malformed JAL instruction - expected two parameters; received " + argList.Length);
            }

            int rdIdx = argList[0];
            int offset = argList[1];

            int nextAddress = ctx.RuntimeRegisters[InterpreterCommon.PC_REGISTER].Value + sizeof(int);
            ctx.RuntimeRegisters[rdIdx].Value = nextAddress;

            ctx.RuntimeRegisters[InterpreterCommon.PC_REGISTER].Value += offset;

            return true;
        }
    }
}
