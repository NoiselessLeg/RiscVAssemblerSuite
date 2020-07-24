using Assembler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.InstructionProcessing
{
   class RemuProcessor : BaseInstructionProcessor
   {
      /// <summary>
      /// Parses an instruction and generates the binary code for it.
      /// </summary>
      /// <param name="address">The address of the instruction being parsed in the .text segment.</param>
      /// <param name="args">An array containing the arguments of the instruction.</param>
      /// <returns>One or more 32-bit integers representing this instruction. If this interface is implemented
      /// for a pseudo-instruction, this may return more than one instruction value.</returns>
      public override IEnumerable<int> GenerateCodeForInstruction(int address, string[] args)
      {
         // we expect three arguments. if not, throw an ArgumentException
         if (args.Length != 3)
         {
            throw new ArgumentException("Invalid number of arguments provided. Expected 3, received " + args.Length + '.');
         }

         IEnumerable<int> returnVal = null;
         int instruction = 0;
         int rdReg = RegisterMap.GetNumericRegisterValue(args[0]);
         int rs1Reg = RegisterMap.GetNumericRegisterValue(args[1]);
         int rs2Reg = 0;
         try
         {
            rs2Reg = RegisterMap.GetNumericRegisterValue(args[2]);

            List<int> instructionList = new List<int>();
            instruction |= 0x2000000;
            instruction |= (rs2Reg << 20);
            instruction |= (rs1Reg << 15);
            instruction |= (0x7 << 12);
            instruction |= (rdReg << 7);
            instruction |= 0x33;
            instructionList.Add(instruction);
            returnVal = instructionList;
         }
         catch (ArgumentException)
         {
            throw;
         }

         return returnVal;
      }
   }
}
