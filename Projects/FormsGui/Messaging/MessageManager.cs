using Assembler.FormsGui.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.Messaging
{
   public class MessageManager
   {
      public static MessageManager GetInstance()
      {
         if (s_Instance == null)
         {
            s_Instance = new MessageManager();
         }

         return s_Instance;
      }

      private MessageManager()
      {
         m_MsgQueues = new List<IBasicQueue<IBasicMessage>>();
      }

      /// <summary>
      /// Broadcasts a message to every sender. This overload allows a sender to send
      /// a loopback message (i.e. will be received by the same sender), or allows a sender
      /// who is not registered to receive messages to send a message.
      /// </summary>
      /// <param name="message">The message to send.</param>
      public void BroadcastMessage(IBasicMessage message)
      {
         foreach (var queue in m_MsgQueues)
         {
            queue.Enqueue(message);
         }
      }

      /// <summary>
      /// Broadcasts a message to every sender. This overload takes care to not
      /// send a message to a receiver with the same ID (i.e. does not loopback messages).
      /// </summary>
      /// <param name="senderId">The ID of the sender. This ID should match the value returned from 
      /// RegisterMessageQueue.</param>
      /// <param name="message">The message to send.</param>
      public void BroadcastMessage(int senderId, IBasicMessage message)
      {
         int index = 0;
         foreach (var queue in m_MsgQueues)
         {
            if (senderId != index)
            {
               queue.Enqueue(message);
            }

            ++index;
         }
      }

      /// <summary>
      /// Registers a message queue into the MessageManager for reception of broadcast messages.
      /// </summary>
      /// <param name="msgQueue">The message queue to register.</param>
      /// <returns>A unique ID that should be used by a sender. This will prevent messages
      /// sent by sender A from being received by sender A.</returns>
      public int RegisterMessageQueue(IBasicQueue<IBasicMessage> msgQueue)
      {
         m_MsgQueues.Add(msgQueue);
         return m_NextId++;
      }

      private static MessageManager s_Instance;
      private readonly List<IBasicQueue<IBasicMessage>> m_MsgQueues;
      private int m_NextId;
   }
}
