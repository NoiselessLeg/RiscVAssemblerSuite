using Assembler.Common;
using System;
using System.Collections.Generic;

namespace Assembler.InstructionProcessing
{
    class BgezProcessor : BgeProcessor
    {
        public BgezProcessor(SymbolTable symTbl) :
            base(symTbl)
        {
        }

        /// <summary>
        /// Parses an instruction and generates the binary code for it.
        /// </summary>
        /// <param name="instructionArgs">The address of the instruction being parsed in the .text segment.</param>
        /// <param name="args">An array containing the arguments of the instruction.</param>
        /// <returns>One or more 32-bit integers representing this instruction. If this interface is implemented
        /// for a pseudo-instruction, this may return more than one instruction value.</returns>
        public override IEnumerable<int> GenerateCodeForInstruction(int address, string[] instructionArgs)
        {
            if (instructionArgs.Length != 2)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 2, received " + instructionArgs.Length + '.');
            }

            return base.GenerateCodeForInstruction(address, new string[] { "x0", instructionArgs[0], instructionArgs[1] });
        }
    }
}
