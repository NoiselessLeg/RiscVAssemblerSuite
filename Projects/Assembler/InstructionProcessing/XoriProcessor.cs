using Assembler.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.InstructionProcessing
{
    class XoriProcessor : BaseInstructionProcessor
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

                // if the immediate is greater than our 12 bit immediate value, then treat this as a pseudo instruction
                // and generate backing instructions.
                if (!IsValidTwelveBitSignedImmediate(immVal))
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
        /// Generates a list of instructions for the XORI instruction, given that the immediate argument
        /// is larger than 12 bits.
        /// </summary>
        /// <param name="address">The next address in the .text segment.</param>
        /// <param name="immediate">The numeric 32-bit immediate value.</param>
        /// <param name="args">The array of arguments passed to the instruction.</param>
        /// <returns>A list of 32-bit integers representing the real instructions backing the pseudo-instruction.</returns>
        private List<int> GenerateExpandedInstruction(int address, int immediate, string[] args)
        {
            // load the upper 20 bits of the immediate into the destination register
            int shiftedImm = immediate >> 12;

            IEnumerable<int> backingLuiInstructions = new LuiProcessor().GenerateCodeForInstruction(address, new[] { args[0], shiftedImm.ToString() });

            // or that with the lower 12 bits of that immediate.
            IEnumerable<int> backingOriInstructions = new OriProcessor().GenerateCodeForInstruction(address, new[] { args[0], args[0], (immediate & 0xFFF).ToString() });

            // xor the value of what we have in our register with the rs1 register.
            IEnumerable<int> backingAndInstructions = new XorProcessor().GenerateCodeForInstruction(address, new[] { args[0], args[0], args[1] });

            var instructionList = new List<int>();
            instructionList.AddRange(backingLuiInstructions);
            instructionList.AddRange(backingOriInstructions);
            instructionList.AddRange(backingAndInstructions);
            return instructionList;
        }

        /// <summary>
        /// Generates the single xori instruction, which is useable in the case
        /// that the immediate is 12 bits.
        /// </summary>
        /// <param name="immediate">The immediate value.</param>
        /// <param name="rs1Reg">The numeric rs register.</param>
        /// <param name="rdReg">The rd register.</param>
        /// <returns>A 32 bit RISC-V assembly instruction for the xori operation.</returns>
        private int GenerateUnexpandedInstruction(int immediate, int rs1Reg, int rdReg)
        {
            // take the first 12 bits of the immediate value.
            immediate &= 0xFFF;
            int instruction = 0;
            instruction |= (immediate << 20);
            instruction |= (rs1Reg << 15);
            instruction |= (0x4 << 12);
            instruction |= (rdReg << 7);
            instruction |= 0x13;
            return instruction;
        }
    }
}
