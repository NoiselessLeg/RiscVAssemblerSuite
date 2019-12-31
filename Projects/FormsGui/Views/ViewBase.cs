using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Assembler.FormsGui.Controls.Custom;
using Assembler.FormsGui.Messaging;
using Assembler.FormsGui.Utility;

namespace Assembler.FormsGui.Views
{
   public class ViewBase : UserControl, IBasicView
   {
      public ViewBase()
      {
      }

      public ViewBase(string viewName)
      {
         m_ViewName = viewName;
      }

      // these are virtual primarily so the designer doesn't barf while trying
      // to display them.
      // they really should be abstract.
      public virtual MenuBarContext MenuBarMembers
      {
         get
         {
            throw new NotImplementedException("MenuBarMembers was not overriden in derived class.");
         }
      }

      public virtual IBasicQueue<IBasicMessage> MessageQueue
      {
         get
         {
            throw new NotImplementedException("MessageQueue was not overriden in derived class.");
         }
      }

      public string ViewName
      {
         get { return m_ViewName; }
      }

      private string m_ViewName;
   }
}
