using Assembler.Common;
using Assembler.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.InstructionProcessing
{
    class LuiProcessor : BaseInstructionProcessor
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

            string rd = args[0].Trim();
            string immediateStr = args[1].Trim();

            int immediate = 0;
            if (!int.TryParse(immediateStr, out immediate))
            {
                throw new ArgumentException("Lui - argument 2 was non-integer immediate value.");
            }
            
            int rdReg = RegisterMap.GetNumericRegisterValue(rd);
            int bitShiftedImm = immediate << 12;

            int instruction = 0;
            instruction |= bitShiftedImm;
            instruction |= (rdReg << 7);
            instruction |= 0x37;
            var inList = new List<int>();
            inList.Add(instruction);
            return inList;
        }
    }
}
