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

      public ViewBase(int viewId,
                      string viewName,
                      MessageManager msgMgr)
      {
         m_ViewId = viewId;
         m_ViewName = viewName;
         m_ViewMsgMgr = msgMgr;
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

      protected int ViewId
      {
         get { return m_ViewId; }
      }

      protected void SendMessage(IBasicMessage msg)
      {
         m_ViewMsgMgr.BroadcastMessage(msg);
      }
         
      public string ViewName
      {
         get { return m_ViewName; }
      }

      private string m_ViewName;

      private readonly int m_ViewId;
      private readonly MessageManager m_ViewMsgMgr;
   }
}
