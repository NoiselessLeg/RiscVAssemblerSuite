using Assembler.CodeGeneration;
using Assembler.Common;
using Assembler.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assembler.InstructionProcessing
{
    class AddiProcessor : BaseInstructionProcessor
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

            int immVal = 0;
            bool isValidImmediate = IntExtensions.TryParseEx(args[2], out immVal);

            if (isValidImmediate)
            {
                var instructionList = default(List<int>);

                // if greater than 0x7FF (2047) or less than 0xFFF, then help the user by trying to expand out the instruction
                // so that it effectively does the same thing.
                // TODO: do we need to use another mask for negative numbers?
                if (immVal > 2047 || immVal < -2048)
                {
                    instructionList = GenerateExpandedInstruction(address, immVal, args);
                }
                else
                {
                    instructionList = new List<int>();
                    int instruction = GenerateUnexpandedInstruction(immVal, rs1Reg, rdReg);
                    instructionList.Add(instruction);
                }

                return instructionList;
            }
            else
            {
                throw new ArgumentException(args[2] + " is not a valid immediate value.");
            }
        }

        /// <summary>
        /// Generates a list of instructions for the ADDI instruction, given that the immediate argument
        /// is larger than 11 bits (neglecting the sign bit).
        /// </summary>
        /// <param name="address">The next address in the .text segment.</param>
        /// <param name="immediate"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private List<int> GenerateExpandedInstruction(int address, int immediate, string[] args)
        {
            // load the upper 20 bits of the immediate into the destination register
            int shiftedImm = immediate >> 12;

            IEnumerable<int> backingLuiInstructions = new LuiProcessor().GenerateCodeForInstruction(address, new[] { args[0], shiftedImm.ToString() });

            // or that with the lower 12 bits of that immediate.
            IEnumerable<int> backingOriInstructions = new OriProcessor().GenerateCodeForInstruction(address, new[] { args[0], args[0], (immediate & 0xFFF).ToString() });

            // add the value of what we have in our register with the rs1 register.
            IEnumerable<int> backingAddInstructions = new AddProcessor().GenerateCodeForInstruction(address, new[] { args[0], args[0], args[1] });

            var instructionList = new List<int>();
            instructionList.AddRange(backingLuiInstructions);
            instructionList.AddRange(backingOriInstructions);
            instructionList.AddRange(backingAddInstructions);
            return instructionList;
        }

        /// <summary>
        /// Generates the single addi instruction, which is useable in the case
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
            instruction |= (rdReg << 7);
            instruction |= 0x13;
            return instruction;
        }
    }
}
