using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.Utility
{
   public class ObservableQueue<T> : IBasicQueue<T>
   {
      public ObservableQueue()
      {
         m_Queue = new Queue<T>();
      }

      public event EventHandler<CollectionChangeEventArgs> QueueChanged;

      public void Enqueue(T elem)
      {
         m_Queue.Enqueue(elem);
         QueueChanged?.Invoke(this, new CollectionChangeEventArgs(CollectionChangeAction.Add, elem));
      }

      public T Dequeue()
      {
         T elem = m_Queue.Dequeue();
         QueueChanged?.Invoke(this, new CollectionChangeEventArgs(CollectionChangeAction.Remove, elem));
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
