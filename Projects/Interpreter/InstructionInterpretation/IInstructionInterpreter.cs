using Assembler.OutputProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Interpreter.InstructionInterpretation
{
    /// <summary>
    /// Defines a class that can interpret an instruction, and perform changes to register
    /// values based on the parameters of the instruction.
    /// </summary>
    interface IInstructionInterpreter
    {
        /// <summary>
        /// Performs processing related to a given instruction and its parameters.
        /// </summary>
        /// <param name="argList">The parameter list associated with the instruction.</param>
        /// <param name="registers">The array of 32-bit registers that this instruction may read from/write to.</param>
        /// <param name="dataSegment">An accessor to the program .data segment.</param>
        /// <returns>True if the program counter value is modified in this register and should not be modified otherwise,
        /// false otherwise.</returns>
        bool InterpretInstruction(int[] argList, Register[] registers, RuntimeDataSegmentAccessor dataSegment);
    }
}
