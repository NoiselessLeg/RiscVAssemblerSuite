using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.Utility
{
   public class BindableProperty<T> : NotifyPropertyChangedBase
   {
      public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
      {
         OnPropertyChanged(propertyName);
      }
   }
}
