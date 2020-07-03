using Assembler.OutputProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.CmdLine.Simulator
{
   public class SourceLineInformation
   {
      public SourceLineInformation(InstructionData instance)
      {
         m_InstructionData = instance;
      }

      public string SourceFilePath
      {
         get { return m_InstructionData.SourceFilePath; }
      }

      public string InstructionText
      {
         get
         {
            string str = m_InstructionData.Instruction;
            if (m_InstructionData.Instruction.Contains(':'))
            {
               str = m_InstructionData.Instruction.Substring(m_InstructionData.Instruction.IndexOf(':') + 1);
               str = str.Trim();
            }

            return str;
         }
      }

      public int SourceLineNumber
      {
         get
         {
            return m_InstructionData.SourceLineNumber;
         }
      }

      public string OriginalInstructionSourceText
      {
         get { return m_InstructionData.OriginalSourceInstruction; }
      }

      public string ProgramCounterLocationStr
      {
         get { return "0x" + ProgramCounterLocation.ToString("x8"); }
      }

      public string RawBytesStr
      {
         get { return "0x" + RawBytes.ToString("x8"); }
      }

      public int ProgramCounterLocation
      {
         get { return m_InstructionData.ProgramCounterLocation; }
      }

      public int RawBytes
      {
         get { return m_InstructionData.InstructionWord; }
      }
      

      private readonly InstructionData m_InstructionData;
   }
}
