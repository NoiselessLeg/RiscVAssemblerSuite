﻿using Assembler.Common;
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

            uint immVal = 0;
            bool isValidImmediate = IntExtensions.TryParseEx(args[2], out immVal);

            // this is okay, as we expect an unsigned value.
            isValidImmediate = isValidImmediate && ((immVal & 0xFFFFF000) == 0);

            var instructionList = new List<int>();

            if (isValidImmediate)
            {
                int instruction = GenerateUnexpandedInstruction(immVal, rs1Reg, rdReg);
                instructionList.Add(instruction);
            }
            else
            {
                // otherwise, emit three instructions. load the upper 20 bits of the immediate into the destination register,
                // bitwise-or it with the remaining 12 bits, and then use sltu (the s-type).
                var luiProc = new LuiProcessor();
                instructionList.AddRange(luiProc.GenerateCodeForInstruction(address, new string[] { args[0], (immVal >> 12).ToString() }));

                uint orImmVal = immVal & 0xFFF;
                var oriProc = new OriProcessor();
                instructionList.AddRange(oriProc.GenerateCodeForInstruction(address, new string[] { args[0], orImmVal.ToString() }));

                var sltuProc = new SltuProcessor();
                instructionList.AddRange(sltuProc.GenerateCodeForInstruction(address, new string[] { args[0], args[1], args[0] }));
            }

            return instructionList;
        }

        /// <summary>
        /// Generates the single sltiu instruction, which is useable in the case
        /// that the immediate is 11 bits (sans the sign bit).
        /// </summary>
        /// <param name="immediate">The immediate value.</param>
        /// <param name="rs1Reg">The numeric rs register.</param>
        /// <param name="rdReg">The rd register.</param>
        /// <returns>A 32 bit RISC-V assembly instruction for the addi operation.</returns>
        private int GenerateUnexpandedInstruction(uint immediate, int rs1Reg, int rdReg)
        {
            // take the first 12 bits of the immediate value.
            immediate &= 0xFFF;
            int instruction = 0;
            instruction |= (int)(immediate << 20);
            instruction |= (rs1Reg << 15);
            instruction |= (0x3 << 12);
            instruction |= (rdReg << 7);
            instruction |= 0x13;
            return instruction;
        }
    }
}
