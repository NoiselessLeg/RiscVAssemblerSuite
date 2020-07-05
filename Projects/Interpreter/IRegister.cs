using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Interpreter
{
   /// <summary>
   /// Defines an interface for a 32-bit register that can have
   /// values applied or 
   /// </summary>
   public interface IRegister
   {
      /// <summary>
      /// Gets or sets the value of this specific register.
      /// </summary>
      int Value
      {
         get; set;
      }
   }
}
