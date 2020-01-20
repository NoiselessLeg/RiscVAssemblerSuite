using Assembler.Common;
using Assembler.OutputProcessing;
using System;
using System.Linq;

namespace Assembler.OutputProcessing.TextOutput.InstructionGenerators
{
   internal class BranchInstructionStringifier : IParameterStringifier
   {
      public BranchInstructionStringifier(string instructionName)
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
            throw new ArgumentException("sb instruction expected 3 arguments, received " + inst.Parameters.Count());
         }

         string rs1 = ReverseRegisterMap.GetStringifiedRegisterValue(inst.Parameters.ElementAt(0));
         string rs2 = ReverseRegisterMap.GetStringifiedRegisterValue(inst.Parameters.ElementAt(1));

         retStr += rs1 + ", " + rs2 + ", ";

         int offset = inst.Parameters.ElementAt(2);

         int address = currPgrmCtr + offset;
         // see if there's a symbol mapped to it.
         if (symTable.ContainsSymbol(address))
         {
            Symbol sym = symTable.GetSymbol(address);
            retStr += sym.LabelName;
         }
         else
         {
            retStr += "0x" + address.ToString("X2");
         }

         return retStr;
      }

      private readonly string m_Name;
   }
}
