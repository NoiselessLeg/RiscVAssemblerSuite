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
                var instructionList = default(List<int>);

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
            IEnumerable<int> backingAndInstructions = new AndProcessor().GenerateCodeForInstruction(address, new[] { args[0], args[0], args[1] });

            var instructionList = new List<int>();
            instructionList.AddRange(backingLuiInstructions);
            instructionList.AddRange(backingOriInstructions);
            instructionList.AddRange(backingAndInstructions);
            return instructionList;
        }

        /// <summary>
        /// Generates the single andi instruction, which is useable in the case
        /// that the immediate is 11 bits (sans the sign bit).
        /// </summary>
        /// <param name="immediate">The immediate value.</param>
        /// <param name="rs1Reg">The numeric rs register.</param>
        /// <param name="rdReg">The rd register.</param>
        /// <returns>A 32 bit RISC-V assembly instruction for the andi operation.</returns>
        private int GenerateUnexpandedInstruction(int immediate, int rs1Reg, int rdReg)
        {
            // take the first 12 bits of the immediate value.
            immediate &= 0xFFF;
            int instruction = 0;
            instruction |= (immediate << 20);
            instruction |= (rs1Reg << 15);
            instruction |= (0x7 << 12);
            instruction |= (rdReg << 7);
            instruction |= 0x13;
            return instruction;
        }
    }
}
