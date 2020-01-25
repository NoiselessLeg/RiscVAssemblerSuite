using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Common
{
   /// <summary>
   /// Class that stores data relating to an instruction, its initial assembly source,
   /// line number, and where it can be found in the program.
   /// </summary>
   public class SourceLineInformation
   {
      public SourceLineInformation(int lineNum, int pgmCtrLoc):
         this(lineNum, pgmCtrLoc, string.Empty)
      {
      }

      public SourceLineInformation(int lineNum, int pgmCtrLoc, string text)
      {
         m_SrcLineNum = lineNum;
         m_TextSegmentAddr = pgmCtrLoc;
         m_SrcLine = text;
      }

      /// <summary>
      /// Gets the line number of the source file that this instruction was found at.
      /// </summary>
      public int SourceFileLineNumber
      {
         get { return m_SrcLineNum; }
      }

      /// <summary>
      /// Gets the program counter address that the instruction exists at when executing.
      /// </summary>
      public int TextSegmentAddress
      {
         get { return m_TextSegmentAddr; }
      }

      /// <summary>
      /// Gets the unsynthesized source line from the assembly source file.
      /// </summary>
      public string SourceLine
      {
         get { return m_SrcLine; }
      }
      
      private readonly string m_SrcLine;
      private readonly int m_SrcLineNum;
      private readonly int m_TextSegmentAddr;
   }
}
