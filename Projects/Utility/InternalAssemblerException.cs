using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Common
{
   public class InternalAssemblerException : Exception
   {
      public InternalAssemblerException(Exception innerEx) :
         base("An internal exception occurred in the assembler. " +
              "See the inner exception for more details.", innerEx)
      {
      }
   }
}
