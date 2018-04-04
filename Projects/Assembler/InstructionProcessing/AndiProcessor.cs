using Assembler.Common;
using Assembler.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.InstructionProcessing
{
    class AndiProcessor : BaseInstructionProcessor
    {
        public override IEnumerable<int> GenerateCodeForInstruction(int address, string[] args)
        {
            // we expect three arguments. if not, throw an ArgumentException
            if (args.Length != 3)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 3, received " + args.Length + '.');
            }
            
            int rdReg = RegisterMap.GetNumericRegisterValue(args[0]);
            int rs1Reg = RegisterMap.GetNumericRegisterValue(args[1]);
            short immVal = 0;
            bool isValidImmediate = short.TryParse(args[2], out immVal);

            if (isValidImmediate)
            {
                // TODO: need to change this to generate instructions
                // for immediates larger than 12 bits.
                var instructionList = new List<int>();
                immVal &= 0xFFF;
                int instruction = 0;
                instruction |= (immVal << 20);
                instruction |= (rs1Reg << 15);
                instruction |= (0x7 << 12);
                instruction |= (rdReg << 7);
                instruction |= 0x13;
                instructionList.Add(instruction);
                return instructionList;
            }
            else
            {
                throw new ArgumentException(args[2] + " is not a valid immediate value.");
            }
        }
    }
}
