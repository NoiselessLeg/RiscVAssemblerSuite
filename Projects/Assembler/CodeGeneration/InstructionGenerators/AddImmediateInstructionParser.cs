using Assembler.Util;
using System;
using System.Collections.Generic;

namespace Assembler.CodeGeneration.InstructionGenerators
{
    class AddImmediateInstructionParser : IParser
    {
        public IEnumerable<int> ParseInstruction(int nextTextAddress, string[] args)
        {
            // we expect three arguments. if not, throw an ArgumentException
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
                // TODO: need to check if our immediate causes us to generate more instructions.
                var instructionList = new List<int>();

                // take the first 12 bits of the immediate value.
                immVal &= 0xFFF;
                int instruction = 0;
                instruction |= (immVal << 20);
                instruction |= (rs1Reg << 15);
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
