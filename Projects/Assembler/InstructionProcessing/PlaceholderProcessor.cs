using Assembler.Common;
using System;
using System.Collections.Generic;

namespace Assembler.InstructionProcessing
{
    class PlaceholderProcessor : BaseInstructionProcessor
    {
        public PlaceholderProcessor(string instName)
        {
            m_InstName = instName;
        }

        public override IEnumerable<int> GenerateCodeForInstruction(int nextTextAddress, string[] instructionArgs)
        {
            throw new NotImplementedException(m_InstName + " instruction is not yet implemented.");
        }

        private readonly string m_InstName;
    }
}
