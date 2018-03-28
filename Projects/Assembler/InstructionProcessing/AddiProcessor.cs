using Assembler.CodeGeneration;
using Assembler.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assembler.InstructionProcessing
{
    class AddImmediateInstructionParser : BaseInstructionProcessor
    {
        /// <summary>
        /// Parses an instruction and generates the binary code for it.
        /// </summary>
        /// <param name="nextTextAddress">The address of the theoretical next instruction being parsed in the .text segment.</param>
        /// <param name="instructionArgs">An array containing the arguments of the instruction.</param>
        /// <returns>One or more 32-bit integers representing this instruction. If this interface is implemented
        /// for a pseudo-instruction, this may return more than one instruction value.</returns>
        public override IEnumerable<int> GenerateCodeForInstruction(int nextTextAddress, string[] args)
        {
            return GenerateInstructionList(nextTextAddress, args);
        }

        /// <summary>
        /// Determines how many instructions are generated via a pseudo-instruction. The default implementation assumes
        /// that only one instruction will be returned.
        /// </summary>
        /// <param name="nextTextAddress">The address of the theoretical next instruction being parsed in the .text segment.</param>
        /// <param name="instructionArgs">An array containing the arguments of the instruction.</param>
        /// <returns>An integer representing how many instructions will be generated for a line of assembly.</returns>
        public override int GetNumGeneratedInstructions(int nextTextAddress, string[] instructionArgs)
        {
            return GenerateInstructionList(nextTextAddress, instructionArgs).Count();
        }

        /// <summary>
        /// Generates a list of one or more instructions based on the provided arguments.
        /// </summary>
        /// <param name="nextTextAddress"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private IEnumerable<int> GenerateInstructionList(int nextTextAddress, string[] args)
        {
            // we expect three arguments. if not, throw an ArgumentException
            if (args.Length != 3)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 3, received " + args.Length + '.');
            }

            int immVal = 0;
            if (!int.TryParse(args[2], out immVal))
            {
                throw new ArgumentException("Addi: Immediate \"" + args[2] + "\" was not a valid 32-bit integer");
            }

            string rd = args[0].Trim();
            string rs1 = args[1].Trim();
            string imm = args[2].Trim();
            int rdReg = RegisterMap.GetNumericRegisterValue(rd);
            int rs1Reg = RegisterMap.GetNumericRegisterValue(rs1);
            bool isValidImmediate = int.TryParse(imm, out immVal);

            if (isValidImmediate)
            {
                var instructionList = default(List<int>);

                // if greater than 0x7FF (2047), then help the user by trying to expand out the instruction
                // so that it effectively does the same thing.
                // TODO: do we need to use another mask for negative numbers?
                if (immVal > 2047 || immVal < -2048)
                {
                    instructionList = GenerateExpandedInstruction(nextTextAddress, immVal, args);
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
                throw new ArgumentException(imm + " is not a valid immediate value.");
            }
        }

        /// <summary>
        /// Generates a list of instructions for an instruction, given that the immediate argument
        /// is larger than 11 bits (neglecting the sign bit).
        /// </summary>
        /// <param name="nextTextAddress">The next address in the .text segment.</param>
        /// <param name="immediate"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private List<int> GenerateExpandedInstruction(int nextTextAddress, int immediate, string[] args)
        {
            IEnumerable<int> backingLuiInstructions = new LuiProcessor().GenerateCodeForInstruction(nextTextAddress, new[] { "x1", args[2] });
            IEnumerable<int> backingOriInstructions = new OriProcessor().GenerateCodeForInstruction(nextTextAddress, new[] { "x1", "x1", (immediate & 0x7FF).ToString() });
            IEnumerable<int> backingAddInstructions = new AddProcessor().GenerateCodeForInstruction(nextTextAddress, new[] { args[0], args[1], "x1" });

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
