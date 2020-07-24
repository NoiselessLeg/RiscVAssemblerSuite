using System;

namespace Assembler.Interpreter.InstructionInterpretation
{
   internal class FcvtswInterpreter : IInstructionInterpreter
   {
      public bool InterpretInstruction(RuntimeContext ctx, int[] argList)
      {
         if (argList.Length != 2)
         {
            throw new InvalidOperationException("Malformed FCVT.S.W instruction - expected two parameters; received " + argList.Length);
         }

         int rdIdx = argList[0];
         int rs1Idx = argList[1];

         float convertedVal = ctx.UserRegisters[rs1Idx].Value;

         ctx.FloatingPointRegisters[rdIdx].Value = convertedVal;

         return false;
      }
   }
}
