using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.OutputProcessing
{
   public class InstructionData
   {
      public InstructionData(int rawWord, int programCounterLoc, string instruction)
      {
         m_Instruction = instruction;
         m_RawWord = rawWord;
         m_ProgramCtrLoc = programCounterLoc;
      }

      public string Instruction
      {
         get { return m_Instruction; }
      }

      public int InstructionWord
      {
         get { return m_RawWord; }
      }

      public int ProgramCounterLocation
      {
         get { return m_ProgramCtrLoc; }
      }

      private readonly string m_Instruction;
      private readonly int m_RawWord;
      private readonly int m_ProgramCtrLoc;
   }
}
