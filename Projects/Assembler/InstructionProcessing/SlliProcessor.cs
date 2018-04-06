using Assembler.Util;
using System;
using System.Collections.Generic;

namespace Assembler.InstructionProcessing
{
    class SlliProcessor : BaseInstructionProcessor
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

            IEnumerable<int> returnVal = null;
            int rdReg = RegisterMap.GetNumericRegisterValue(args[0]);
            int rs1Reg = RegisterMap.GetNumericRegisterValue(args[1]);
            byte shiftAmt = 0;
            bool isValidImmediate = byte.TryParse(args[2], out shiftAmt);

            // ensure our shift amount is 5 bits or less.
            isValidImmediate = isValidImmediate && ((shiftAmt & 0xE0) != 0);
            if (isValidImmediate)
            { 
                var instructionList = new List<int>();
                int instruction = 0;
                instruction |= (shiftAmt << 20);
                instruction |= (rs1Reg << 15);
                instruction |= (0x1 << 12);
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
