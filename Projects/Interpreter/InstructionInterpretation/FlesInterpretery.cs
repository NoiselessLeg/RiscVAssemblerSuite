using Assembler.Interpreter;
using Assembler.Interpreter.InstructionInterpretation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Simulation.InstructionInterpretation
{
   class FlesInterpreter : IInstructionInterpreter
   {
      public bool InterpretInstruction(RuntimeContext ctx, int[] argList)
      {
         if (argList.Length != 3)
         {
            throw new InvalidOperationException("Malformed FLE.S instruction - expected three parameters; received " + argList.Length);
         }

         int rdIdx = argList[0];
         int rs1Idx = argList[1];
         int rs2Idx = argList[2];

         float val1 = ctx.FloatingPointRegisters[rs1Idx].Value;
         float val2 = ctx.FloatingPointRegisters[rs2Idx].Value;

         if (float.IsNaN(val1) || float.IsNaN(val2))
         {
            //TODO: need to set an exception flag if either of these inputs are NaN values
            ctx.UserRegisters[rdIdx].Value = 0;
         }
         else
         {
            if (val1 < val2 || val1.BinaryEquals(val2))
            {
               ctx.UserRegisters[rdIdx].Value = 1;
            }
            else
            {
               ctx.UserRegisters[rdIdx].Value = 0;
            }
         }

         return false;
      }
   }
}
