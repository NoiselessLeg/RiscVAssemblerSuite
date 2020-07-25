using Assembler.Interpreter;
using Assembler.Interpreter.InstructionInterpretation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Simulation.InstructionInterpretation
{
   class FswInterpreter : IInstructionInterpreter
   {
      public bool InterpretInstruction(RuntimeContext ctx, int[] argList)
      {
         if (argList.Length != 3)
         {
            throw new InvalidOperationException("Malformed FSW instruction - expected three parameters; received " + argList.Length);
         }

         int rs2Idx = argList[0];
         int rs1Idx = argList[1];
         int offset = argList[2];

         int address = ctx.UserRegisters[rs1Idx].Value + offset;

         ctx.WriteSinglePrecisionFloat(address, ctx.FloatingPointRegisters[rs2Idx].Value);

         return false;
      }
   }
}
