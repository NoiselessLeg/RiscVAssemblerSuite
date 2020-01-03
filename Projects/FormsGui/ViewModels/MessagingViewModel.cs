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
         m_MsgHandlerTable = new Dictionary<MessageType, ICommand>();
         m_MsgQueue = new ObservableQueue<IBasicMessage>();
         m_MsgMgr = msgMgr;
         m_MsgSenderId = m_MsgMgr.RegisterMessageQueue(m_MsgQueue);
         m_MsgQueue.ItemEnqueued += OnExternalMsgReceived;
      }

      /// <summary>
      /// Broadcasts a message to the other views in the GUI.
      /// </summary>
      /// <param name="msg">The message to be sent to the other GUIs for handling.</param>
      protected void SendExternalMessage(IBasicMessage msg)
      {
         m_MsgMgr.BroadcastMessage(m_MsgSenderId, msg);
      }

      /// <summary>
      /// Allows a subclass to subscribe to a message type and handle it
      /// in a defined way.
      /// </summary>
      /// <param name="type">The message type to subscribe to.</param>
      /// <param name="handler">The handler command that will be used to handle the message.</param>
      protected void SubscribeToMessageType(MessageType type, ICommand handler)
      {
         m_MsgHandlerTable.Add(type, handler);
      }

      private void OnExternalMsgReceived(object sender, EventArgs e)
      {
         var msgQ = sender as IBasicQueue<IBasicMessage>;
         IBasicMessage msg = msgQ.Dequeue();
         if (m_MsgHandlerTable.TryGetValue(msg.MessageType, out ICommand handler))
         {
            msg.HandleMessage(handler);
         }
      }

      private readonly int m_MsgSenderId;
      private readonly MessageManager m_MsgMgr;
      private readonly ObservableQueue<IBasicMessage> m_MsgQueue;
      private readonly Dictionary<MessageType, ICommand> m_MsgHandlerTable;
   }
}
