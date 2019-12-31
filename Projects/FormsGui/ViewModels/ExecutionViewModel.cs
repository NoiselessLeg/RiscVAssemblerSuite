using Assembler.FormsGui.Utility;
using Assembler.Interpreter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.ViewModels
{
   public class ExecutionViewModel : NotifyPropertyChangedBase, IRuntimeEnvironment
   {
      public ExecutionViewModel(ExecutionContext ctx)
      {
         m_Registers = new ObservableCollection<RegisterViewModel>();
         m_Ctx = ctx;
         for (int i = 0; i < InterpreterCommon.MAX_REGISTERS; ++i)
         {
            m_Registers.Add(new RegisterViewModel(i, ctx));
         }
      }

      public void Terminate()
      {
         m_IsTerminated = true;
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
