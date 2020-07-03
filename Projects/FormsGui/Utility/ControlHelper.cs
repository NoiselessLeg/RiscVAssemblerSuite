using Assembler.UICommon.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembler.FormsGui.Utility
{
   public static class ControlHelper
   {

      public static void InvokeIfRequired(this ISynchronizeInvoke ctrl, MethodInvoker action)
      {
         if (ctrl.InvokeRequired)
         {
            var dummyArgs = new object[0];
            ctrl.BeginInvoke(action, dummyArgs);
         }
         else
         {
            action();
         }
      }
   }
}
