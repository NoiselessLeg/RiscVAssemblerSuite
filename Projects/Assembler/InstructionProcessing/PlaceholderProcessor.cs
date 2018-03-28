using System;
using System.Collections.Generic;

namespace Assembler.InstructionProcessing
{
    class PlaceholderParser : BaseInstructionProcessor
    {
        public PlaceholderParser(string instName)
        {
            m_InstName = instName;
        }

        public override IEnumerable<int> ParseInstruction(int nextTextAddress, string[] instructionArgs)
        {
            throw new NotImplementedException(m_InstName + " instruction is not yet implemented.");
        }

        private readonly string m_InstName;
    }
}
