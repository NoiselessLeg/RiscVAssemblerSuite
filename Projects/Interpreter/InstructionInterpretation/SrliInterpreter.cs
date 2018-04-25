﻿using Assembler.OutputProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Interpreter.InstructionInterpretation
{
    class SrliInterpreter : IInstructionInterpreter
    {
        public bool InterpretInstruction(RuntimeContext ctx, int[] argList)
        {
            if (argList.Length != 3)
            {
                throw new InvalidOperationException("Malformed SRLI instruction - expected three parameters; received " + argList.Length);
            }

            int rdIdx = argList[0];
            int rs1Idx = argList[1];
            int immediate = argList[2];

            // have to cast register to uint to force logical shift right, otherwise C# assumed arithmetic shift.
            ctx.RuntimeRegisters[rdIdx].Value = (int)(((uint)ctx.RuntimeRegisters[rs1Idx].Value) >> immediate);

            return false;
        }
    }
}
