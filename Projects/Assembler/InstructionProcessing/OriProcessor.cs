using Assembler.Util;
using System;
using System.Collections.Generic;

namespace Assembler.InstructionProcessing
{
    class OriProcessor : BaseInstructionProcessor
    {
        public override IEnumerable<int> GenerateCodeForInstruction(int nextTextAddress, string[] args)
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
                int instruction = 0;

                // first 12 bits of immediate
                immVal &= 0xFFF;
                instruction |= (immVal << 20);
                instruction |= (rs1Reg << 15);
                instruction |= (rdReg << 7);

                // ori opcode/funct3 = 0x13/0x6
                instruction |= 0x13;
                instruction |= (0x6 << 12);

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
