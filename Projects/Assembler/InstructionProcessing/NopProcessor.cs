using System;
using System.Collections.Generic;

namespace Assembler.InstructionProcessing
{
    class NopProcessor : BaseInstructionProcessor
    {
        public override IEnumerable<int> GenerateCodeForInstruction(int nextTextAddress, string[] instructionArgs)
        {
            // we expect no arguments. if not, throw an ArgumentException
            if (instructionArgs.Length != 0)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 0, received " + instructionArgs.Length + '.');
            }
            var delegateParser = new AndiProcessor();
            return delegateParser.GenerateCodeForInstruction(nextTextAddress, new string[] { "x0", "x0", "0" });
        }
    }
}
