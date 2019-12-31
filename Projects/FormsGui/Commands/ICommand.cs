﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.Commands
{
   public interface ICommand
   {
      bool CanExecute(object parameter);
      void Execute(object parameter);
   }
}
