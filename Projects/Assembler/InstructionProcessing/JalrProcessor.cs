using Assembler.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.InstructionProcessing
{
    class JalrProcessor : BaseInstructionProcessor
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
                var instructionList = new List<int>();

                // if the immediate is greater than 12 bits, use
                // an auipc instruction.
                if ((int)(immVal & 0xFFFFF000) != 0)
                {
                    var auipcHelper = new AuipcProcessor();
                    IEnumerable<int> auipcInstructions =
                        auipcHelper.GenerateCodeForInstruction(address, new string[] { args[1], (immVal >> 12).ToString() });
                    instructionList.AddRange(auipcInstructions);
                }
                
                int instruction = 0;
                instruction |= (immVal << 20);
                instruction |= (rs1Reg << 15);
                instruction |= (rdReg << 7);
                instruction |= 0x67;
                instructionList.Add(instruction);

                return instructionList;
            }
            else
            {
                throw new ArgumentException("Immediate was not a valid 32-bit integer.");
            }
            
        }
    }
}
