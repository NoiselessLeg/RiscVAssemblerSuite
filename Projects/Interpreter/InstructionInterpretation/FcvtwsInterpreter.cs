using System;

namespace Assembler.Interpreter.InstructionInterpretation
{
   internal class FcvtwsInterpreter : IInstructionInterpreter
   {
      public bool InterpretInstruction(RuntimeContext ctx, int[] argList)
      {
         if (argList.Length != 2)
         {
            throw new InvalidOperationException("Malformed FCVT.W.S instruction - expected two parameters; received " + argList.Length);
         }

         int rdIdx = argList[0];
         int rs1Idx = argList[1];

         int convertedVal = (int)ctx.FloatingPointRegisters[rs1Idx].Value;

         ctx.UserRegisters[rdIdx].Value = convertedVal;

         return false;
      }
   }
}
