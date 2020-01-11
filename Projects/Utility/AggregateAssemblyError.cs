using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Common
{
   public class AggregateAssemblyError : Exception
   {
      public AggregateAssemblyError(IEnumerable<AssemblyException> exceptions)
      {
         m_ExList = exceptions;
      }

      public IEnumerable<AssemblyException> AssemblyErrors
      {
         get { return m_ExList; }
      }

      private readonly IEnumerable<AssemblyException> m_ExList;
   }
}
