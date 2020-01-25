using Assembler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.OutputProcessing
{
   /// <summary>
   /// Data structure that exposes the program counter location that the
   /// instruction was found in, the raw instruction word, and the original
   /// instruction string.
   /// </summary>
   public class InstructionData
   {
      public InstructionData(int rawWord, int programCounterLoc, string instruction, SourceLineInformation srcData)
      {
         m_Instruction = instruction;
         m_RawWord = rawWord;
         m_ProgramCtrLoc = programCounterLoc;
         m_SrcLineInfo = srcData;
      }

      /// <summary>
      /// Gets the stringified (i.e. original) assembly instruction.
      /// </summary>
      public string Instruction
      {
         get { return m_Instruction; }
      }

      /// <summary>
      /// Gets the raw 32-bit (i.e. assembled) representation of the instruction
      /// </summary>
      public int InstructionWord
      {
         get { return m_RawWord; }
      }

      /// <summary>
      /// Gets the .text location that the instruction was found in.
      /// </summary>
      public int ProgramCounterLocation
      {
         get { return m_ProgramCtrLoc; }
      }

      /// <summary>
      /// Gets the original source instruction from the assembly source file.
      /// </summary>
      public string OriginalSourceInstruction
      {
         get { return m_SrcLineInfo.SourceLine; }
      }

      /// <summary>
      /// Gets the source file line number.
      /// </summary>
      public int SourceLineNumber
      {
         get { return m_SrcLineInfo.SourceFileLineNumber; }
      }

      private readonly string m_Instruction;
      private readonly int m_RawWord;
      private readonly int m_ProgramCtrLoc;
      private readonly SourceLineInformation m_SrcLineInfo;
   }
}
