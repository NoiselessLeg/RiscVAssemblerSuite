using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Interpreter.InstructionInterpretation
{
   class EbreakInterpreter : IInstructionInterpreter
   {
      public EbreakInterpreter(IRuntimeEnvironment environment)
      {
         m_Environment = environment;
      }

      /// <summary>
      /// Performs processing related to a given instruction and its parameters.
      /// </summary>
      /// <param name="argList">The parameter list associated with the instruction.</param>
      /// <param name="registers">The array of 32-bit registers that this instruction may read from/write to.</param>
      /// <param name="dataSegment">An accessor to the program .data segment.</param>
      /// <returns>True if the program counter value is modified in this register and should not be modified otherwise,
      /// false otherwise.</returns>
      public bool InterpretInstruction(RuntimeContext ctx, int[] argList)
      {
         if (argList.Length != 0)
         {
            throw new InvalidOperationException("Malformed EBREAK instruction - expected zero parameters; received " + argList.Length);
         }

         m_Environment.Break();

         return false;

      }

      private readonly IRuntimeEnvironment m_Environment;
   }
}
