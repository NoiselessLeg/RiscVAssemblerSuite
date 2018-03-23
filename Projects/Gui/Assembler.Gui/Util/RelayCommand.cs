using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Assembler.Gui.Util
{
    class RelayCommand : ICommand
    {
        public RelayCommand(Action<object> execution):
            this(execution, null)
        {
        }

        public RelayCommand(Action<object> execution, Predicate<object> canExecute)
        {
            if (execution == null)
            {
                throw new ArgumentNullException("execution");
            }

            m_Execution = execution;
            m_CanExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameters)
        {
            bool canExecute = true;
            if (m_CanExecute != null)
            {
                canExecute = m_CanExecute(parameters);
            }

            return canExecute;
        }

        public void Execute(object parameters)
        {
            m_Execution(parameters);
        }

        private readonly Action<object> m_Execution;
        private readonly Predicate<object> m_CanExecute;
    }
}
