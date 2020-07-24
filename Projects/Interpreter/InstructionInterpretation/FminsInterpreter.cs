using System;

namespace Assembler.Interpreter.InstructionInterpretation
{
   internal class FminsInterpreter : IInstructionInterpreter
   {
      public bool InterpretInstruction(RuntimeContext ctx, int[] argList)
      {
         if (argList.Length != 3)
         {
            throw new InvalidOperationException("Malformed FMIN.S instruction - expected three parameters; received " + argList.Length);
         }

         int rdIdx = argList[0];
         int rs1Idx = argList[1];
         int rs2Idx = argList[2];

         float minVal = Math.Min(ctx.FloatingPointRegisters[rs1Idx].Value, ctx.FloatingPointRegisters[rs2Idx].Value);

         ctx.FloatingPointRegisters[rdIdx].Value = minVal;

         return false;
      }
   }
}
