﻿using Assembler.OutputProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Interpreter.InstructionInterpretation
{
    class SltInterpreter : IInstructionInterpreter
    {
        public bool InterpretInstruction(RuntimeContext ctx, int[] argList)
        {
            if (argList.Length != 3)
            {
                throw new InvalidOperationException("Malformed SLLI instruction - expected three parameters; received " + argList.Length);
            }

            int rdIdx = argList[0];
            int rs1Idx = argList[1];
            int rs2Idx = argList[2];

            if (ctx.RuntimeRegisters[rs1Idx].Value < ctx.RuntimeRegisters[rs2Idx].Value)
            {
                ctx.RuntimeRegisters[rdIdx].Value = 1;
            }
            else
            {
                ctx.RuntimeRegisters[rdIdx].Value = 0;
            }

            return false;
        }
    }
}
