using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.InstructionProcessing
{
   class EbreakProcessor : BaseInstructionProcessor
   {
      /// <summary>
      /// Parses an instruction and generates the binary code for it.
      /// </summary>
      /// <param name="address">The address of the instruction being parsed in the .text segment.</param>
      /// <param name="instructionArgs">An array containing the arguments of the instruction.</param>
      /// <returns>One or more 32-bit integers representing this instruction. If this interface is implemented
      /// for a pseudo-instruction, this may return more than one instruction value.</returns>
      public override IEnumerable<int> GenerateCodeForInstruction(int address, string[] instructionArgs)
      {
         var instructionList = new List<int>();
         instructionList.Add(0x100073);
         return instructionList;
      }
   }
}
