﻿using Assembler.OutputProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Interpreter.InstructionInterpretation
{
    class AddiInterpreter : IInstructionInterpreter
    {
        public bool InterpretInstruction(int[] argList, Register[] registers, DataSegmentAccessor dataSegment)
        {
            if (argList.Length != 3)
            {
                throw new InvalidOperationException("Malformed ADDI instruction - expected three parameters; received " + argList.Length);
            }

            int rdIdx = argList[0];
            int rs1Idx = argList[1];
            int immediate = argList[2];

            registers[rdIdx].Value = registers[rs1Idx].Value + immediate;

            return false;
        }
    }
}