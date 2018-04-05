﻿using Assembler.Common;
using Assembler.Util;
using System;
using System.Collections.Generic;

namespace Assembler.InstructionProcessing
{
    class OrProcessor : BaseInstructionProcessor
    {
        public override IEnumerable<int> GenerateCodeForInstruction(int address, string[] args)
        {
            if (args.Length != 3)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 3, received " + args.Length + '.');
            }

            IEnumerable<int> returnVal = null;
            int instruction = 0;
            int rdReg = RegisterMap.GetNumericRegisterValue(args[0]);
            int rs1Reg = RegisterMap.GetNumericRegisterValue(args[1]);
            int rs2Reg = 0;
            try
            {
                rs2Reg = RegisterMap.GetNumericRegisterValue(args[2]);

                List<int> instructionList = new List<int>();
                instruction |= (rs2Reg << 20);
                instruction |= (rs1Reg << 15);

                // or opcode/funt3/funct7 = 0x33/0x6/0x0
                instruction |= (0x6 << 12);
                instruction |= (rdReg << 7);
                instruction |= 0x33;
                instructionList.Add(instruction);
                returnVal = instructionList;
            }
            catch (ArgumentException)
            {
                // try parsing as ori instruction
                short immediate = 0;
                bool isShort = short.TryParse(args[2], out immediate);
                if (isShort)
                {
                    var immediateParser = new OriProcessor();
                    returnVal = immediateParser.GenerateCodeForInstruction(address, args);
                }
                else
                {
                    throw;
                }
            }

            return returnVal;
        }
    }
}
