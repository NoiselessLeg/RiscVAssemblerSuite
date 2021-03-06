﻿using Assembler.OutputProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Interpreter.InstructionInterpretation
{
    class LuiInterpreter : IInstructionInterpreter
    {
        public bool InterpretInstruction(RuntimeContext ctx, int[] argList)
        {
            if (argList.Length != 2)
            {
                throw new InvalidOperationException("Malformed LUI instruction - expected two parameters; received " + argList.Length);
            }

            int rdIdx = argList[0];
            int immediate = (argList[1] << 12);
            ctx.UserRegisters[rdIdx].Value = 0;
            ctx.UserRegisters[rdIdx].Value = (ctx.UserRegisters[rdIdx].Value | immediate);

            return false;
        }
    }
}
