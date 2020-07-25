using Assembler.Interpreter;
using Assembler.Interpreter.InstructionInterpretation;
using System;

namespace Assembler.Simulation.InstructionInterpretation
{
   class FlwInterpreter : IInstructionInterpreter
   {
      public bool InterpretInstruction(RuntimeContext ctx, int[] argList)
      {
         if (argList.Length != 3)
         {
            throw new InvalidOperationException("Malformed FLW instruction - expected three parameters; received " + argList.Length);
         }

         int rdIdx = argList[0];
         int rs1Idx = argList[1];
         int offset = argList[2];

         int addressToLoad = ctx.UserRegisters[rs1Idx].Value + offset;

         ctx.FloatingPointRegisters[rdIdx].Value = ctx.ReadSinglePrecisionFloat(addressToLoad);

         return false;
      }
   }
}
