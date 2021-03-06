﻿using Assembler.Common;
using Assembler.OutputProcessing;

namespace Assembler.OutputProcessing.TextOutput.InstructionGenerators
{
   internal class EcallStringifier : IParameterStringifier
   {
      public EcallStringifier(string instructionName)
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

         retStr += m_Name;
         return retStr;
      }

      private readonly string m_Name;
   }

}
