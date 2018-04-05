using Assembler.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.InstructionProcessing
{
    class SltiuProcessor : BaseInstructionProcessor
    {
        /// <summary>
        /// Parses an instruction and generates the binary code for it.
        /// </summary>
        /// <param name="address">The address of the instruction being parsed in the .text segment.</param>
        /// <param name="instructionArgs">An array containing the arguments of the instruction.</param>
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
            isValidImmediate = isValidImmediate && (immVal <= 2047) && (immVal >= -2048);

            if (isValidImmediate)
            {
                var instructionList = default(List<int>);

                // if greater than 0x7FF (2047) or less than 0xFFF, then help the user by trying to expand out the instruction
                // so that it effectively does the same thing.
                // TODO: do we need to use another mask for negative numbers?
                instructionList = new List<int>();
                int instruction = GenerateUnexpandedInstruction(immVal, rs1Reg, rdReg);
                instructionList.Add(instruction);

                return instructionList;
            }
            else
            {
                throw new ArgumentException(args[2] + " is not a valid 12-bit immediate value.");
            }
        }

        /// <summary>
        /// Generates the single sltiu instruction, which is useable in the case
        /// that the immediate is 11 bits (sans the sign bit).
        /// </summary>
        /// <param name="immediate">The immediate value.</param>
        /// <param name="rs1Reg">The numeric rs register.</param>
        /// <param name="rdReg">The rd register.</param>
        /// <returns>A 32 bit RISC-V assembly instruction for the addi operation.</returns>
        private int GenerateUnexpandedInstruction(int immediate, int rs1Reg, int rdReg)
        {
            // take the first 12 bits of the immediate value.
            immediate &= 0xFFF;
            int instruction = 0;
            instruction |= (immediate << 20);
            instruction |= (rs1Reg << 15);
            instruction |= (0x3 << 12);
            instruction |= (rdReg << 7);
            instruction |= 0x13;
            return instruction;
        }
    }
}
