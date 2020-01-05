using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Assembler.FormsGui.Commands
{
   public class RelayCommand : ICommand
   {
      public RelayCommand(Action<object> executionAction, bool defaultExecutionState)
      {
         m_ExecutionAction = executionAction ?? throw new ArgumentNullException("executionAction");
         m_CanExecute = defaultExecutionState;
      }

      public bool CanExecute
      {
         get { return m_CanExecute; }
         set
         {
            if (m_CanExecute != value)
            {
               m_CanExecute = value;
               PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CanExecute)));
            }
         }
      }

      public void Execute(object parameter)
      {
         m_ExecutionAction(parameter);
      }

      private readonly Action<object> m_ExecutionAction;
      private bool m_CanExecute;

      public event PropertyChangedEventHandler PropertyChanged;

   }

   public class RelayCommand<TExecArg> : ICommand
   {
      public RelayCommand(Action<TExecArg> executionAction, bool defaultExecutionState)
      {
         m_ExecutionAction = executionAction ?? throw new ArgumentNullException("executionAction");
         m_CanExecute = defaultExecutionState;
      }

      public bool CanExecute
      {
         get { return m_CanExecute; }
         set
         {
            if (m_CanExecute != value)
            {
               m_CanExecute = value;
               PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CanExecute)));
            }
         }
      }

      public void Execute(object parameter)
      {
         m_ExecutionAction((TExecArg) parameter);
      }

      private readonly Action<TExecArg> m_ExecutionAction;
      private bool m_CanExecute;

      public event PropertyChangedEventHandler PropertyChanged;
   }
}
