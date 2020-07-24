using Assembler.Interpreter;
using Assembler.Interpreter.InstructionInterpretation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Simulation.InstructionInterpretation
{
   class MulhInterpreter : IInstructionInterpreter
   {
      public bool InterpretInstruction(RuntimeContext ctx, int[] argList)
      {
         if (argList.Length != 3)
         {
            throw new InvalidOperationException("Malformed MULH instruction - expected three parameters; received " + argList.Length);
         }

         int rdIdx = argList[0];
         int rs1Idx = argList[1];
         int rs2Idx = argList[2];

         ulong result = (ulong)(ctx.UserRegisters[rs1Idx].Value * ctx.UserRegisters[rs2Idx].Value);
         ctx.UserRegisters[rdIdx].Value = (int)((result & 0xFFFFFFFF00000000) >> 32);

         return false;
      }
   }
}
