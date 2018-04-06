﻿using Assembler.Common;
using Assembler.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.InstructionProcessing
{
    class JalProcessor : SymbolicInstructionProcessor 
    {
        public JalProcessor(SymbolTable symbolTable):
            base(symbolTable)
        {
        }

        /// <summary>
        /// Parses an instruction and generates the binary code for it.
        /// </summary>
        /// <param name="address">The address of the instruction being parsed in the .text segment.</param>
        /// <param name="args">An array containing the arguments of the instruction.</param>
        /// <returns>One or more 32-bit integers representing this instruction. If this interface is implemented
        /// for a pseudo-instruction, this may return more than one instruction value.</returns>
        public override IEnumerable<int> GenerateCodeForInstruction(int address, string[] args)
        {
            // we expect two arguments. if not, throw an ArgumentException
            if (args.Length != 2)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 2, received " + args.Length + '.');
            }

            int rdReg = RegisterMap.GetNumericRegisterValue(args[0]);
            Symbol symbolLabel = SymbolTable.GetSymbol(args[1]);
            var instructionList = new List<int>();

            // the offset is doubled implicitly by the processor, so halve it here.
            int offset = (symbolLabel.Address - address) / 2;

            // this should rarely happen, but if the halved immediate exceeds the 21 bit boundary,
            // error out and notify the user.
            if ((offset & 0xFFE00000) != 0)
            {
                throw new ArgumentException("jal - the offset between the address of \"" + symbolLabel.LabelName + "\"" +
                    " (0x" + symbolLabel.Address.ToString("X") + " and this instruction address (0x" +
                    address.ToString("X") + ") exceeds the 21 bit immediate limit. Use jalr instead.");
            }

            int instruction = 0;

            // get the twentieth bit offset (21st bit) of the offset value
            // and shift it to the 31st bit offset.
            instruction |= ((offset & 0x100000) << 11);

            // get the 10-1 bit offsets and shift that range to the 30-20 offset.
            instruction |= ((offset & 0x7FE) << 20);

            // get the 11th bit offset and shift it to offset 19.
            instruction |= ((offset & 0x800) << 8);

            // get the 19-12 bit offsets and shift them to position 18-11
            instruction |= ((offset & 0xFF00) >> 1);

            // shift the rd register value up to offset 11-7
            instruction |= (rdReg << 7);

            instruction |= 0x6F;
            instructionList.Add(instruction);

            return instructionList;
        }

        protected override int GetNumOfInstructionsForSymbolicInstruction(int address, string[] args)
        {
            // always should return one instruction.
            return 1;
        }
    }
}
