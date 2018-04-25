using Assembler.OutputProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Interpreter.InstructionInterpretation
{
    class SltuInterpreter : IInstructionInterpreter
    {
        public bool InterpretInstruction(RuntimeContext ctx, int[] argList)
        {
            if (argList.Length != 3)
            {
                throw new InvalidOperationException("Malformed SLTU instruction - expected three parameters; received " + argList.Length);
            }

            int rdIdx = argList[0];
            int rs1Idx = argList[1];
            int rs2Idx = argList[2];

            uint rs1Val = (uint)ctx.UserRegisters[rs1Idx].Value;
            uint rs2Val = (uint)ctx.UserRegisters[rs2Idx].Value;

            if (rs1Val < rs2Val)
            {
                ctx.UserRegisters[rdIdx].Value = 1;
            }
            else
            {
                ctx.UserRegisters[rdIdx].Value = 0;
            }

            return false;
        }
    }
}
