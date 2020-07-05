using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Assembler.UICommon.Commands
{
   /// <summary>
   /// Implementation of ICommand that allows a user to specify an Action to perform
   /// on execution, and a value determining whether or not a user can execute this command
   /// by default.
   /// </summary>
   public class RelayCommand : ICommand
   {
      public RelayCommand(Action executionAction, bool defaultExecutionState)
      {
         m_ExecutionAction = executionAction ?? throw new ArgumentNullException("executionAction");
         m_CanExecute = defaultExecutionState;
      }

      /// <summary>
      /// Executes a given command with no parameter.
      /// </summary>
      /// <param name="parameter">This overload of the RelayCommand class does not use this parameter.</param>
      public void Execute(object parameter)
      {
         m_ExecutionAction();
      }

      /// <summary>
      /// Gets or sets a boolean indicating that a Control object can execute this command.
      /// </summary>
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

      private readonly Action m_ExecutionAction;
      private bool m_CanExecute;

      public event PropertyChangedEventHandler PropertyChanged;
   }

   /// <summary>
   /// Implementation of ICommand that allows a user to specify an Action to perform
   /// on execution, and a value determining whether or not a user can execute this command
   /// by default. This implementation allows a user to provide a specific parameter to the Action that will
   /// be executed.
   /// </summary>
   /// <typeparam name="TExecArg">The type of parameter that will be passed to the executing Action object.</typeparam>
   public class RelayCommand<TExecArg> : ICommand
   {
      public RelayCommand(Action<TExecArg> executionAction, bool defaultExecutionState)
      {
         m_ExecutionAction = executionAction ?? throw new ArgumentNullException("executionAction");
         m_CanExecute = defaultExecutionState;
      }


      /// <summary>
      /// Gets or sets a boolean indicating that a Control object can execute this command.
      /// </summary>
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

      /// <summary>
      /// Executes a given command with a given parameter.
      /// </summary>
      /// <param name="parameter">The parameter to pass to the action to execute. If this is null, this will
      /// throw an ArgumentNullException.</param>
      public void Execute(object parameter)
      {
         if (parameter == null)
         {
            throw new ArgumentNullException(nameof(parameter));
         }
         m_ExecutionAction((TExecArg) parameter);
      }

      private readonly Action<TExecArg> m_ExecutionAction;
      private bool m_CanExecute;

      public event PropertyChangedEventHandler PropertyChanged;
   }
}
