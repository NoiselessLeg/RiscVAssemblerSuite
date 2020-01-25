using Assembler.Disassembler;
using Assembler.OutputProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.ViewModels
{
   public class ProgramInstructionViewModel : BaseViewModel
   {
      public ProgramInstructionViewModel(InstructionData instance)
      {
         m_InstructionData = instance;
      }

      public bool IsBreakpointApplied
      {
         get { return m_IsBreakpointApplied; }
         set
         {
            if (m_IsBreakpointApplied != value)
            {
               m_IsBreakpointApplied = value;
               OnPropertyChanged();
            }
         }
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

      private bool m_IsBreakpointApplied;
      private readonly InstructionData m_InstructionData;
   }
}
