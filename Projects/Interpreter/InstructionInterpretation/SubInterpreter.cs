﻿using Assembler.OutputProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Interpreter.InstructionInterpretation
{
    class SubInterpreter : IInstructionInterpreter
    {
        public bool InterpretInstruction(RuntimeContext ctx, int[] argList)
        {
            if (argList.Length != 3)
            {
                throw new InvalidOperationException("Malformed SUB instruction - expected three parameters; received " + argList.Length);
            }

            int rdIdx = argList[0];
            int rs1Idx = argList[1];
            int rs2Idx = argList[2];

            ctx.UserRegisters[rdIdx].Value = (int)((ctx.UserRegisters[rs1Idx].Value - ctx.UserRegisters[rs2Idx].Value) & 0xFFFFFFFF);

            return false;
        }
    }
}
