using Assembler.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.InstructionProcessing
{
    class SrliProcessor : BaseInstructionProcessor
    {
        public override IEnumerable<int> GenerateCodeForInstruction(int address, string[] args)
        {
            // we expect three arguments. if not, throw an ArgumentException
            if (args.Length != 3)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 3, received " + args.Length + '.');
            }

            string rd = args[0].Trim();
            string rs1 = args[1].Trim();
            string rs2 = args[2].Trim();

            IEnumerable<int> returnVal = null;
            int rdReg = RegisterMap.GetNumericRegisterValue(rd);
            int rs1Reg = RegisterMap.GetNumericRegisterValue(rs1);
            byte shiftAmt = 0;
            bool isValidImmediate = byte.TryParse(args[2], out shiftAmt);

            // ensure our shift amount is 5 bits or less.
            isValidImmediate = isValidImmediate && (shiftAmt <= 0x1F);
            if (isValidImmediate)
            {
                var instructionList = new List<int>();
                int instruction = 0;
                instruction |= (shiftAmt << 20);
                instruction |= (rs1Reg << 15);
                instruction |= (0x5 << 12);
                instruction |= (rdReg << 7);
                instruction |= 0x13;
                instructionList.Add(instruction);
                returnVal = instructionList;
            }
            else
            {
                throw new ArgumentException("Value \"" + args[2] + "\" is greater than allowed 5-bit value.");
            }

            return returnVal;
        }
    }
}
