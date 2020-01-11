using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Common
{
   public class UserAssemblerError : Exception
   {
      public UserAssemblerError(string fileName, AggregateAssemblyError ex):
         base(ex.Message, ex)
      {
         m_FileName = fileName;
      }

      public string FileName
      {
         get { return m_FileName; }
      }

      private readonly string m_FileName;
   }
}
