using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Assembler.Gui.Commands
{
    class AssembleCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public AssembleCommand(Action<object> executor,
                               Func<object, bool> canExecuteMethod)
        {
            m_ExecutionDelegate = executor;
            m_CanExecuteDelegate = canExecuteMethod;
        }

        public bool CanExecute(object parameter)
        {
            return m_CanExecuteDelegate(parameter);
        }

        public void Execute(object parameter)
        {
            m_ExecutionDelegate(parameter);
        }

        private readonly Action<object> m_ExecutionDelegate;
        private readonly Func<object, bool> m_CanExecuteDelegate;
    }
}
