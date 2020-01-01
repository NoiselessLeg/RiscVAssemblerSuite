using Assembler.Common;
using Assembler.FormsGui.Utility;
using Assembler.Interpreter;
using Assembler.OutputProcessing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.ViewModels
{
   public class ExecutionViewModel : BaseViewModel, IRuntimeEnvironment
   {
      public ExecutionViewModel(ITerminal terminal, DisassembledFile file)
      {
         m_Ctx = new ExecutionContext(this, terminal, file);
         m_Registers = new ObservableCollection<RegisterViewModel>();
         for (int i = 0; i < InterpreterCommon.MAX_REGISTERS; ++i)
         {
            m_Registers.Add(new RegisterViewModel(i, m_Ctx));
         }
      }

      public void Terminate()
      {
         m_IsTerminated = true;
      }

      public int CurrentProgramCounter
      {
         get { return Registers[InterpreterCommon.PC_REGISTER].RegisterValue; }
      }

      public ObservableCollection<RegisterViewModel> Registers
      {
         get { return m_Registers; }
      }

      private readonly ObservableCollection<RegisterViewModel> m_Registers;
      private readonly ExecutionContext m_Ctx;
      
      private bool m_IsTerminated;
   }
}
