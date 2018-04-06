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
        /// <summary>
        /// Parses an instruction and generates the binary code for it.
        /// </summary>
        /// <param name="address">The address of the instruction being parsed in the .text segment.</param>
        /// <param name="args">An array containing the arguments of the instruction.</param>
        /// <returns>One or more 32-bit integers representing this instruction. If this interface is implemented
        /// for a pseudo-instruction, this may return more than one instruction value.</returns>
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

            isValidImmediate = isValidImmediate && ((immVal & 0xF000) == 0);

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
