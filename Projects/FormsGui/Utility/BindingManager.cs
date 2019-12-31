using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembler.FormsGui.Utility
{

   public class BindingManager
   {
      public void CreateBinding<ViewModelType, ControlType>(ViewModelType vm, ControlType ctrl,
                                                            Func<ViewModelType, string> boundVmElem, 
                                                            Func<ControlType, string> boundCtrlElem)
         where ViewModelType : NotifyPropertyChangedBase
         where ControlType : Control
      {
         ctrl.DataBindings.Add(new Binding(boundVmElem(vm), vm, boundCtrlElem(ctrl)));
      }
   }
}
