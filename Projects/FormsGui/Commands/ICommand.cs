using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.Commands
{
   public interface ICommand : INotifyPropertyChanged
   {
      void Execute(object parameter);
      bool CanExecute { get; }
   }
}
