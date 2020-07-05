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
      /// <summary>
      /// If the caller of a method must call the ISynchronizeInvoke.BeginInvoke on the implementing
      /// object, this will invoke the specified action on the object using the UI thread. Otherwise,
      /// this will call the action on the calling thread.
      /// </summary>
      /// <param name="ctrl">The Control object to perform the action on.</param>
      /// <param name="action">The action to perform.</param>
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
