using Assembler.CodeGeneration;
using Assembler.SymbolTableConstruction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.InstructionProcessing
{
    abstract class BaseInstructionProcessor : IInstructionGenerator, IInstructionSizeEstimator
    {
        public abstract IEnumerable<int> GenerateCodeForInstruction(int nextTextAddress, string[] instructionArgs);

        public virtual int GetNumGeneratedInstructions(int nextTextAddress, string[] instructionArgs)
        {
            return 1;
        }
    }
}
