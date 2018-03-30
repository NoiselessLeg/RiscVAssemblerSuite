using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.InstructionProcessing
{
    class LiProcessor : BaseInstructionProcessor
    {
        public override IEnumerable<int> GenerateCodeForInstruction(int nextTextAddress, string[] instructionArgs)
        {
            if (instructionArgs.Length != 2)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 2, received " + instructionArgs.Length + '.');
            }

            return new AddiProcessor().GenerateCodeForInstruction(nextTextAddress, new string[] { instructionArgs[0], "x0", instructionArgs[1] });
        }
    }
}
