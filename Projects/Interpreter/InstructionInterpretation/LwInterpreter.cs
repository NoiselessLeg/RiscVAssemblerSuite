using Assembler.OutputProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Interpreter.InstructionInterpretation
{
    class LwInterpreter : IInstructionInterpreter
    {
        public bool InterpretInstruction(RuntimeContext ctx, int[] argList)
        {
            if (argList.Length != 3)
            {
                throw new InvalidOperationException("Malformed LW instruction - expected three parameters; received " + argList.Length);
            }

            int rdIdx = argList[0];
            int rs1Idx = argList[1];
            int offset = argList[2];

            int addressToLoad = ctx.RuntimeRegisters[rs1Idx].Value + offset;

            ctx.RuntimeRegisters[rdIdx].Value = ctx.ReadWord(addressToLoad);

            return false;
        }
    }
}
