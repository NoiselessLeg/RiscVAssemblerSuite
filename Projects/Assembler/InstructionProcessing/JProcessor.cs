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

        public override IEnumerable<int> GenerateCodeForInstruction(int address, string[] args)
        {
            // we expect two arguments. if not, throw an ArgumentException
            if (args.Length != 1)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 2, received " + args.Length + '.');
            }

            return m_UnderlyingProc.GenerateCodeForInstruction(address, new[] { "x0", args[1] });
        }

        protected override int GetNumOfInstructionsForSymbolicInstruction(int address, string[] args)
        {
            return m_UnderlyingProc.GetNumGeneratedInstructions(address, new[] { "x0", args[1] });
        }

        private readonly JalProcessor m_UnderlyingProc;
    }
}
