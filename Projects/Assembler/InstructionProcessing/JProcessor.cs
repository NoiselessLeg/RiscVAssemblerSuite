using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.InstructionProcessing
{
    class JProcessor : SymbolicInstructionProcessor
    {
        public JProcessor(SymbolTable symbolTable):
            base(symbolTable)
        {
            m_UnderlyingProc = new JalProcessor(symbolTable);
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
            if (args.Length != 1)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 1, received " + args.Length + '.');
            }

            return m_UnderlyingProc.GenerateCodeForInstruction(address, new[] { "x0", args[0] });
        }

        protected override int GetNumOfInstructionsForSymbolicInstruction(int address, string[] args)
        {
            return m_UnderlyingProc.GetNumGeneratedInstructions(address, new[] { "x0", args[0] });
        }

        private readonly JalProcessor m_UnderlyingProc;
    }
}
