using Assembler.FormsGui.Commands;
using Assembler.FormsGui.Messaging;
using Assembler.FormsGui.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.ViewModels
{
   public class MessagingViewModel : BaseViewModel
   {
      public MessagingViewModel(MessageManager msgMgr)
      {
         m_MsgQ = new ObservableQueue<IBasicMessage>();
         m_MsgMgr = msgMgr;
         m_CmdHandlers = new Dictionary<MessageType, ICommand>();
         m_MsgSenderId = msgMgr.RegisterMessageQueue(m_MsgQ);
         m_MsgQ.ItemEnqueued += OnExternalMsgReceived;
      }

      protected void BroadcastMessage(IBasicMessage msg)
      {
         m_MsgMgr.BroadcastMessage(m_MsgSenderId, msg);
      }

      //protected void SubscribeToMessageType(MessageType type, ICommand handler)
      //{
      //   m_CmdHandlers.Add(type, handler);
      //}

      private void OnExternalMsgReceived(object sender, EventArgs e)
      {
         IBasicMessage msg = m_MsgQ.Dequeue();
         if (m_CmdHandlers.TryGetValue(msg.MessageType, out ICommand handler))
         {
            msg.HandleMessage(handler);
         }
      }

      private readonly int m_MsgSenderId;
      private readonly ObservableQueue<IBasicMessage> m_MsgQ;
      private readonly MessageManager m_MsgMgr;
      private readonly Dictionary<MessageType, ICommand> m_CmdHandlers;
   }
}
