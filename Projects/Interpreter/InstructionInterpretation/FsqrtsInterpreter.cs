using System;

namespace Assembler.Interpreter.InstructionInterpretation
{
   internal class FsqrtsInterpreter : IInstructionInterpreter
   {
      public bool InterpretInstruction(RuntimeContext ctx, int[] argList)
      {
         if (argList.Length != 2)
         {
            throw new InvalidOperationException("Malformed FSQRT.S instruction - expected two parameters; received " + argList.Length);
         }

         int rdIdx = argList[0];
         int rs1Idx = argList[1];

         float result = (float)Math.Sqrt(ctx.FloatingPointRegisters[rs1Idx].Value);

         ctx.FloatingPointRegisters[rdIdx].Value = result;

         return false;
      }
   }
}
