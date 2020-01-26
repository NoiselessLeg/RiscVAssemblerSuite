using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Assembler.FormsGui.Commands;
using Assembler.FormsGui.Messaging;
using Assembler.FormsGui.Utility;

namespace Assembler.FormsGui.Views
{
   public class ViewBase : UserControl, IBasicView, INotifyPropertyChanged
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

      /// <summary>
      /// Allows a view to subscribe itself to various message types to process.
      /// </summary>
      /// <param name="type">The type of message to subscribe to. Only views subscribed to this
      /// type will receive this message.</param>
      /// <param name="handler">An ICommand instance that is able to handle the message and
      /// any arguments that come with this command type.</param>
      protected void SubscribeToMessageType(MessageType type, ICommand handler)
      {
         m_CmdHandlers.Add(type, handler);
      }

      /// <summary>
      /// Gets the view sender ID used by the messaging API.
      /// </summary>
      protected int ViewSenderId
      {
         get { return m_SenderId; }
      }

      /// <summary>
      /// Gets the view ID used by the messaging API to determine
      /// which view is requesting to be the active view.
      /// </summary>
      protected int ViewId
      {
         get { return m_ViewId; }
      }

      /// <summary>
      /// Broadcasts a message to every other view that is subscribed to the input message
      /// type.
      /// </summary>
      /// <param name="msg">The message to broadcast.</param>
      protected void SendMessage(IBasicMessage msg)
      {
         m_ViewMsgMgr.BroadcastMessage(m_SenderId, msg);
      }

      public bool AreAnyFilesOpened
      {
         get { return m_AreAnyFilesOpen; }
         protected set
         {
            if (m_AreAnyFilesOpen != value)
            {
               m_AreAnyFilesOpen = value;
               PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AreAnyFilesOpened)));
            }
         }
      }
         
      /// <summary>
      /// Gets the name of the view.
      /// </summary>
      public string ViewName
      {
         get { return m_ViewName; }
      }

      private void OnExternalMsgReceived(object sender, EventArgs e)
      {
         var queue = sender as ObservableQueue<IBasicMessage>;
         IBasicMessage msg = queue.Dequeue();
         if (m_CmdHandlers.TryGetValue(msg.MessageType, out ICommand handler))
         {
            msg.HandleMessage(handler);
         }
      }

      private bool m_AreAnyFilesOpen;
      private string m_ViewName;

      private readonly int m_ViewId;
      private readonly int m_SenderId;
      private readonly MessageManager m_ViewMsgMgr;
      private readonly ObservableQueue<IBasicMessage> m_MsgQ;
      private readonly Dictionary<MessageType, ICommand> m_CmdHandlers;

      public event PropertyChangedEventHandler PropertyChanged;
   }
}
