using Assembler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.InstructionProcessing
{
    abstract class StoreInstructionBase : BaseInstructionProcessor
    {
        /// <summary>
        /// Parses an instruction and generates the binary code for it.
        /// </summary>
        /// <param name="address">The address of the instruction being parsed in the .text segment.</param>
        /// <param name="args">An array containing the arguments of the instruction.</param>
        /// <returns>One or more 32-bit integers representing this instruction. If this interface is implemented
        /// for a pseudo-instruction, this may return more than one instruction value.</returns>
        public override IEnumerable<int> GenerateCodeForInstruction(int nextTextAddress, string[] args)
        {
            if (args.Length != 2)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 2, received " + args.Length + '.');
            }

            int rs2 = RegisterMap.GetNumericRegisterValue(args[0]);
            
            var retList = new List<int>();
            ParameterizedInstructionArg arg = ParameterizedInstructionArg.ParameterizeArgument(args[1]);
            int instruction = 0;
            int upperOffset = (arg.Offset & 0xFE0);
            int lowerOffset = (arg.Offset & 0x1F);
                
            instruction |= (upperOffset << 25);
            instruction |= (rs2 << 20);
            instruction |= (arg.Register << 15);

            byte funcCode = GetFunctionCode();
            instruction |= (funcCode << 12);

            instruction |= (lowerOffset << 7);
            instruction |= 0x23;
            retList.Add(instruction);

            return retList;
        }

        /// <summary>
        /// Gets the three bit function code associated with this branch instruction that calls out
        /// what type of store instruction this is.
        /// </summary>
        /// <returns>A three bit numeric value that tells the processor what instruction type
        /// this represents.</returns>
        protected abstract byte GetFunctionCode();
    }
}
