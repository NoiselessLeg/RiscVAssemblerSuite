using System;

namespace Assembler.Interpreter.InstructionInterpretation
{
   internal class FmulsInterpreter : IInstructionInterpreter
   {
      public bool InterpretInstruction(RuntimeContext ctx, int[] argList)
      {
         if (argList.Length != 3)
         {
            throw new InvalidOperationException("Malformed FMUL.S instruction - expected three parameters; received " + argList.Length);
         }

         int rdIdx = argList[0];
         int rs1Idx = argList[1];
         int rs2Idx = argList[2];

         float result = ctx.FloatingPointRegisters[rs1Idx].Value * ctx.FloatingPointRegisters[rs2Idx].Value;

         ctx.FloatingPointRegisters[rdIdx].Value = result;

         return false;
      }
   }
}
