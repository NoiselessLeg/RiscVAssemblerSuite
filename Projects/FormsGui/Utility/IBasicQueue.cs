using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.Utility
{
   public interface IBasicQueue<T>
   {
      T Dequeue();
      void Enqueue(T elem);
      bool HasElements();
      T PeekNext();
   }
}
