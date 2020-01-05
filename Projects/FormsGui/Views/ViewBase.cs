using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Assembler.FormsGui.Commands;
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
         m_MsgQ = new ObservableQueue<IBasicMessage>();
         m_CmdHandlers = new Dictionary<MessageType, ICommand>();
         m_SenderId = msgMgr.RegisterMessageQueue(m_MsgQ);
         m_MsgQ.ItemEnqueued += OnExternalMsgReceived;
      }

      protected void BroadcastMessage(IBasicMessage msg)
      {
         m_ViewMsgMgr.BroadcastMessage(m_SenderId, msg);
      }

      protected void SubscribeToMessageType(MessageType type, ICommand handler)
      {
         m_CmdHandlers.Add(type, handler);
      }

      protected int ViewSenderId
      {
         get { return m_SenderId; }
      }

      protected int ViewId
      {
         get { return m_ViewId; }
      }

      protected void SendMessage(IBasicMessage msg)
      {
         m_ViewMsgMgr.BroadcastMessage(m_SenderId, msg);
      }
         
      public string ViewName
      {
         get { return m_ViewName; }
      }

      private void OnExternalMsgReceived(object sender, EventArgs e)
      {
         IBasicMessage msg = m_MsgQ.Dequeue();
         if (m_CmdHandlers.TryGetValue(msg.MessageType, out ICommand handler))
         {
            msg.HandleMessage(handler);
         }
      }

      private string m_ViewName;

      private readonly int m_ViewId;
      private readonly int m_SenderId;
      private readonly MessageManager m_ViewMsgMgr;
      private readonly ObservableQueue<IBasicMessage> m_MsgQ;
      private readonly Dictionary<MessageType, ICommand> m_CmdHandlers;

   }
}
