using Assembler.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.InstructionProcessing
{
    class JalrProcessor : BaseInstructionProcessor
    {
        public override IEnumerable<int> GenerateCodeForInstruction(int address, string[] args)
        {
            // we expect two arguments. if not, throw an ArgumentException
            if (args.Length != 3)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 3, received " + args.Length + '.');
            }

            // we expect three arguments. if not, throw an ArgumentException
            if (args.Length != 3)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 3, received " + args.Length + '.');
            }

            int rdReg = RegisterMap.GetNumericRegisterValue(args[0]);
            int rs1Reg = RegisterMap.GetNumericRegisterValue(args[1]);
            short immVal = 0;
            bool isValidImmediate = short.TryParse(args[2], out immVal);

            var instructionList = new List<int>();

            immVal &= 0xFFF;
            int instruction = 0;
            instruction |= (immVal << 20);
            instruction |= (rs1Reg << 15);
            instruction |= (rdReg << 7);
            instruction |= 0x67;
            instructionList.Add(instruction);

            return instructionList;
        }
    }
}
