using Assembler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.InstructionProcessing
{
   class FcvtswProcessor : BaseInstructionProcessor
   {
      public override IEnumerable<int> GenerateCodeForInstruction(int address, string[] instructionArgs)
      {
         // we expect three arguments. if not, throw an ArgumentException
         if (instructionArgs.Length != 2)
         {
            throw new ArgumentException("Invalid number of arguments provided. Expected 2, received " + instructionArgs.Length + '.');
         }

         const int BASE_OPCODE = 0x53;

         int rdReg = RegisterMap.GetNumericFloatingPointRegisterValue(instructionArgs[0]);
         int rs1Reg = RegisterMap.GetNumericRegisterValue(instructionArgs[1]);

         int outputInst = (0x1A << 27) | (rs1Reg << 15) | (rdReg << 7) | BASE_OPCODE;

         var instList = new List<int> { outputInst };
         return instList;
      }
   }
}
