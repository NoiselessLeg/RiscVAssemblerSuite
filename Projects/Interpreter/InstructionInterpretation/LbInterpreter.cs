﻿using Assembler.OutputProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Interpreter.InstructionInterpretation
{
    class LbInterpreter : IInstructionInterpreter
    {
        public bool InterpretInstruction(int[] argList, Register[] registers, DataSegmentAccessor dataSegment)
        {
            if (argList.Length != 3)
            {
                throw new InvalidOperationException("Malformed LB instruction - expected three parameters; received " + argList.Length);
            }
            
            int rdIdx = argList[0];
            int rs1Idx = argList[1];
            int offset = argList[2];

            int addressToLoad = registers[rs1Idx].Value + offset;

            registers[rdIdx].Value = dataSegment.ReadSignedByte(addressToLoad);

            return false;
        }
    }
}