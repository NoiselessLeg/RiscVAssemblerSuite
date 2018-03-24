using Assembler.Util;
using System;
using System.Collections.Generic;

namespace Assembler.CodeGeneration.InstructionGenerators
{
    public class XOrImmediateInstructionParser : IParser
    {
        public IEnumerable<int> ParseInstruction(int nextTextAddress, string[] args)
        {
            if (args.Length != 3)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 3, received " + args.Length + '.');
            }

            string rd = args[0].Trim();
            string rs1 = args[1].Trim();
            string imm = args[2].Trim();
            int rdReg = RegisterMap.GetNumericRegisterValue(rd);
            int rs1Reg = RegisterMap.GetNumericRegisterValue(rs1);
            short immVal = 0;
            bool isValidImmediate = short.TryParse(imm, out immVal);

            if (isValidImmediate)
            {
                // TODO: check if instruction expansion is required
                var instructionList = new List<int>();
                immVal &= 0xFFF;
                int instruction = 0;
                instruction |= (immVal << 20);
                instruction |= (rs1Reg << 15);

                // xori opcode/funct3 = 0x13/0x4
                instruction |= (0x4 << 12);
                instruction |= (rdReg << 7);
                instruction |= 0x13;
                instructionList.Add(instruction);
                return instructionList;
            }
            else
            {
                throw new ArgumentException(imm + " is not a valid immediate value.");
            }
        }
    }
}
