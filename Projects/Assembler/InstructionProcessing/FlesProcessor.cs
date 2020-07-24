using Assembler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.InstructionProcessing
{
   class FlesProcessor : BaseInstructionProcessor
   {
      public override IEnumerable<int> GenerateCodeForInstruction(int address, string[] instructionArgs)
      {
         // we expect three arguments. if not, throw an ArgumentException
         if (instructionArgs.Length != 3)
         {
            throw new ArgumentException("Invalid number of arguments provided. Expected 3, received " + instructionArgs.Length + '.');
         }

         const int BASE_OPCODE = 0x53;

         int rdReg = RegisterMap.GetNumericRegisterValue(instructionArgs[0]);
         int rs1Reg = RegisterMap.GetNumericFloatingPointRegisterValue(instructionArgs[1]);
         int rs2Reg = RegisterMap.GetNumericFloatingPointRegisterValue(instructionArgs[2]);

         int outputInst = (0x14 << 27) | (rs2Reg << 20) | (rs1Reg << 15) | (rdReg << 7) | BASE_OPCODE;

         var instList = new List<int> { outputInst };
         return instList;

      }
   }
}
