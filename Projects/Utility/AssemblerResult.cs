using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Common
{
   public class AssemblerResult
   {
      public AssemblerResult()
      {
         m_UserErrors = new List<AssemblyException>();
         m_InternalErrors = new List<Exception>();
         m_OpSuccessful = true;
      }

      public bool OperationSuccessful
      {
         get
         {
            return m_UserErrors.Count == 0 && 
                   m_InternalErrors.Count == 0 &&
                   m_OpSuccessful == true;
         }
         set
         {
            m_OpSuccessful = value;
         }
      }

      public IEnumerable<AssemblyException> UserErrors
      {
         get { return m_UserErrors; }
      }

      public IEnumerable<Exception> InternalErrors
      {
         get { return m_InternalErrors; }
      }

      public void AddUserAssemblyError(AssemblyException ex)
      {
         m_UserErrors.Add(ex);
      }

      public void AddInternalAssemblerError(Exception ex)
      {
         m_InternalErrors.Add(ex);
      }


      private readonly List<Exception> m_InternalErrors;
      private readonly List<AssemblyException> m_UserErrors;
      private bool m_OpSuccessful;
   }
}
