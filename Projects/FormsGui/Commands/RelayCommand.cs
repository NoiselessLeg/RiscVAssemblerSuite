using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Assembler.FormsGui.Commands
{

   public class RelayCommand : ICommand
    {
        public RelayCommand(Action<object> executionAction) :
            this(executionAction, null)
        {

        }

        public RelayCommand(Action<object> executionAction, Predicate<object> canExecute)
        {
            if (executionAction == null)
            {
                throw new ArgumentNullException("executionAction");
            }

            m_ExecutionAction = executionAction;
            m_CanExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            bool canExecute = true;
            if (m_CanExecute != null)
            {
                canExecute = m_CanExecute(parameter);
            }

            return canExecute;
        }

        public void Execute(object parameter)
        {
            m_ExecutionAction(parameter);
        }

        private readonly Action<object> m_ExecutionAction;
        private readonly Predicate<object> m_CanExecute;
    }
}
