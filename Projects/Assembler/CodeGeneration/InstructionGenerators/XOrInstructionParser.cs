using Assembler.Util;
using System;
using System.Collections.Generic;

namespace Assembler.CodeGeneration.InstructionGenerators
{
    public class XOrInstructionParser : IParser
    {
        public IEnumerable<int> ParseInstruction(int nextTextAddress, string[] args)
        {
            if (args.Length != 3)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 3, received " + args.Length + '.');
            }

            string rd = args[0].Trim();
            string rs1 = args[1].Trim();
            string rs2 = args[2].Trim();

            IEnumerable<int> returnVal = null;
            int instruction = 0;
            int rdReg = RegisterMap.GetNumericRegisterValue(rd);
            int rs1Reg = RegisterMap.GetNumericRegisterValue(rs1);
            int rs2Reg = 0;
            try
            {
                rs2Reg = RegisterMap.GetNumericRegisterValue(rs2);


                List<int> instructionList = new List<int>();
                instruction |= (rs2Reg << 20);
                instruction |= (rs1Reg << 15);

                // xor opcode/funt3/funct7 = x33/x4/x0
                instruction |= (0x4 << 12);
                instruction |= (rdReg << 7);
                instruction |= 0x33;
                instructionList.Add(instruction);
                returnVal = instructionList;
            }
            catch (ArgumentException)
            {
                // try to parse as xori
                short immediate = 0;
                bool isShort = short.TryParse(rs2, out immediate);
                if (isShort)
                {
                    var immediateParser = new XOrImmediateInstructionParser();
                    returnVal = immediateParser.ParseInstruction(nextTextAddress, args);
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
