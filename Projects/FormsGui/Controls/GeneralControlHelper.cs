using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembler.FormsGui.Controls
{
   public static class GeneralControlHelper
   {
      /// <summary>
      /// Performs a depth-first search for a control in the target control's child components,
      /// and removes it if the target is found.
      /// </summary>
      /// <param name="ctrl">The control to perform the operation on.</param>
      /// <param name="target">The control to remove.</param>
      public static void RecursiveRemove(this Control ctrl, Control target)
      {
         for (int i = 0; i < ctrl.Controls.Count; ++i)
         {
            Control child = ctrl.Controls[i];

            if (child != target)
            {
               RecursiveRemove(child, target);
            }
            else
            {
               ctrl.Controls.Remove(target);
            }
         }
      }
   }
}
