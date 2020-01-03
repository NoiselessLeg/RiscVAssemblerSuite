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
      int Value
      {
         get; set;
      }
   }
}
