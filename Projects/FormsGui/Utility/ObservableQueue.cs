using System;
using System.Collections.Generic;

namespace Assembler.FormsGui.Utility
{
   public class ObservableQueue<T> : IBasicQueue<T>
   {
      public ObservableQueue()
      {
         m_Queue = new Queue<T>();
      }

      public event EventHandler<EventArgs> ItemEnqueued;

      public void Enqueue(T elem)
      {
         m_Queue.Enqueue(elem);
         ItemEnqueued?.Invoke(this, new EventArgs());
      }

      public T Dequeue()
      {
         T elem = m_Queue.Dequeue();
         return elem;
      }

      public bool HasElements()
      {
         return m_Queue.Count > 0;
      }

      public T PeekNext()
      {
         return m_Queue.Peek();
      }

      private readonly Queue<T> m_Queue;
   }
}
