using Assembler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.OutputProcessing.TextOutput.InstructionGenerators
{
   class FcvtswStringifier : IParameterStringifier
   {

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

         retStr += "fcvt.s.w ";
         if (inst.Parameters.Count() != 2)
         {
            throw new ArgumentException("Fcvt.s.w instruction expected 2 arguments, received " + inst.Parameters.Count());
         }

         string rd = ReverseRegisterMap.GetStringifiedFloatingPtRegisterValue(inst.Parameters.ElementAt(0));
         string rs1 = ReverseRegisterMap.GetStringifiedRegisterValue(inst.Parameters.ElementAt(1));

         retStr += rd + ", " + rs1;

         return retStr;
      }
   }
}
