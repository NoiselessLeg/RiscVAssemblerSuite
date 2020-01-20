using Assembler.Common;
using Assembler.OutputProcessing;
using Assembler.OutputProcessing.TextOutput;
using Assembler.OutputProcessing.TextOutput.InstructionGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.OutputProcessing
{
   public class DisassemblerServices
   {
      public static IEnumerable<InstructionData> GenerateInstructionData(ReverseSymbolTable symTable, TextSegmentAccessor textSegment)
      {
         var instructions = new List<InstructionData>();
         int currPgrmCtr = textSegment.StartingSegmentAddress;
         foreach (DisassembledInstruction inst in textSegment.RawInstructions)
         {
            IParameterStringifier stringifier = InstructionTextMap.GetParameterStringifier(inst.InstructionType);
            string formattedInstruction = stringifier.GetFormattedInstruction(currPgrmCtr, inst, symTable);
            var instructionElem = new InstructionData(inst.InstructionWord, currPgrmCtr, formattedInstruction);
            instructions.Add(instructionElem);
            currPgrmCtr += sizeof(int);
         }

         return instructions;
      }
   }
}
