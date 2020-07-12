using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Interpreter
{
   /// <summary>
   /// Defines an interface for a 32-bit register that can have
   /// values applied or read from.
   /// </summary>
   public interface IRegister<TRegValue>
   {
      /// <summary>
      /// Gets or sets the value of this specific register.
      /// </summary>
      TRegValue Value
      {
         get; set;
      }
   }
}
