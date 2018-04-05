using Assembler.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.InstructionProcessing
{
    class SubProcessor : BaseInstructionProcessor
    {
        public override IEnumerable<int> GenerateCodeForInstruction(int address, string[] args)
        {
            // we expect three arguments. if not, throw an ArgumentException
            if (args.Length != 3)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 3, received " + args.Length + '.');
            }

            IEnumerable<int> returnVal = null;
            int instruction = 0;
            int rdReg = RegisterMap.GetNumericRegisterValue(args[0]);
            int rs1Reg = RegisterMap.GetNumericRegisterValue(args[1]);
            int rs2Reg = RegisterMap.GetNumericRegisterValue(args[2]);

            List<int> instructionList = new List<int>();
            instruction |= (0x1 << 30);
            instruction |= (rs2Reg << 20);
            instruction |= (rs1Reg << 15);
            instruction |= (rdReg << 7);
            instruction |= 0x33;
            instructionList.Add(instruction);
            returnVal = instructionList;

            return returnVal;
        }
    }
}
