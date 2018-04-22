using Assembler.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.InstructionProcessing
{
    class SrliProcessor : BaseInstructionProcessor
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
            int shiftAmt = 0;
            bool isValidImmediate = IntExtensions.TryParseEx(args[2], out shiftAmt);

            // ensure our shift amount is 5 bits or less.
            isValidImmediate = isValidImmediate && ((shiftAmt & 0xFFFFFFE0) == 0);
            var instructionList = new List<int>();
            if (isValidImmediate)
            {
                int instruction = 0;
                instruction |= (shiftAmt << 20);
                instruction |= (rs1Reg << 15);
                instruction |= (0x5 << 12);
                instruction |= (rdReg << 7);
                instruction |= 0x13;
                instructionList.Add(instruction);
                returnVal = instructionList;
            }
            else
            {
                // otherwise, emit three instructions. load the upper 20 bits of the immediate into the destination register,
                // bitwise-or it with the remaining 12 bits, and then shift the target register by the destination register.
                var luiProc = new LuiProcessor();
                instructionList.AddRange(luiProc.GenerateCodeForInstruction(address, new string[] { args[0], (shiftAmt >> 12).ToString() }));

                int orImmVal = shiftAmt & 0xFFF;
                var oriProc = new OriProcessor();
                instructionList.AddRange(oriProc.GenerateCodeForInstruction(address, new string[] { args[0], orImmVal.ToString() }));

                var srlProc = new SrlProcessor();
                instructionList.AddRange(srlProc.GenerateCodeForInstruction(address, new string[] { args[0], args[1], args[0] }));
            }

            return returnVal;
        }
    }
}
