using Assembler.Common;
using Assembler.Util;
using System;
using System.Collections.Generic;

namespace Assembler.InstructionProcessing
{
    class AuipcProcessor : BaseInstructionProcessor
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
            // we expect two arguments. if not, throw an ArgumentException
            if (args.Length != 2)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 2, received " + args.Length + '.');
            }

            string rs1 = args[0].Trim();
            string immediateStr = args[1].Trim();

            int immediate = 0;
            if (!IntExtensions.TryParseEx(immediateStr, out immediate))
            {
                throw new ArgumentException("auipc - argument 2 was non-integer immediate value.");
            }

            int rs1Reg = RegisterMap.GetNumericRegisterValue(rs1);

            // shift this such that 
            int bitShiftedImm = immediate << 12;

            int instruction = 0;
            instruction |= bitShiftedImm;
            instruction |= (rs1Reg << 7);
            instruction |= 0x17;
            var inList = new List<int>();
            inList.Add(instruction);
            return inList;
        }
    }
}
