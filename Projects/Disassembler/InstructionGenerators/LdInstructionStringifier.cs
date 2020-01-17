using Assembler.Common;
using Assembler.OutputProcessing;
using System;
using System.Linq;

namespace Assembler.Disassembler.InstructionGenerators
{
   internal class LdInstructionStringifier : IParameterStringifier
   {
      public LdInstructionStringifier(string instructionName)
      {
         m_Name = instructionName;
      }

      /// <summary>
      /// Formats and stringifies an instruction as well as its parameters.
      /// </summary>
      /// <param name="currPgrmCtr">The value that the program counter would theoretically be at
      /// upon encountering this instruction.</param>
      /// <param name="inst">The disassembled instruction to stringify.</param>
      /// <param name="symTable">A reverse symbol table used to map addresses back to label names.</param>
      /// <returns>A string representing the instruction and its parameters that can be written to a text file.</returns>
      public string GetFormattedInstruction(int currPgrmCtr, DisassembledInstruction inst, ReverseSymbolTable symTable)
      {
         string retStr = string.Empty;
         // first, see if the program counter has a symbol mapped to it.
         if (symTable.ContainsSymbol(currPgrmCtr))
         {
            Symbol sym = symTable.GetSymbol(currPgrmCtr);
            retStr += sym.LabelName + ":\t\t";
         }
         else
         {
            retStr += "\t\t\t";
         }

         retStr += m_Name + ' ';
         if (inst.Parameters.Count() != 3)
         {
            throw new ArgumentException("I instruction expected 3 arguments, received " + inst.Parameters.Count());
         }

         string rd = ReverseRegisterMap.GetStringifiedRegisterValue(inst.Parameters.ElementAt(0));
         string rs1 = ReverseRegisterMap.GetStringifiedRegisterValue(inst.Parameters.ElementAt(1));

         int offset = inst.Parameters.ElementAt(2);

         retStr += rd + ", " + offset + "(" + rs1 + ")";

         return retStr;
      }

      private readonly string m_Name;
   }
}
