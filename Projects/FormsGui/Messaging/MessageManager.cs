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
      public MessageManager()
      {
         m_MsgQueues = new List<IBasicQueue<IBasicMessage>>();
      }

      public void BroadcastMessage(IBasicMessage message)
      {
         foreach (var queue in m_MsgQueues)
         {
            queue.Enqueue(message);
         }
      }

      public void RegisterMessageQueue(IBasicQueue<IBasicMessage> msgQueue)
      {
         m_MsgQueues.Add(msgQueue);
      }

      private readonly List<IBasicQueue<IBasicMessage>> m_MsgQueues;
   }
}
