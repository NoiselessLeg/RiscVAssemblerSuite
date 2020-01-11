using System;

namespace Assembler.Common
{
   public class AssemblyException : Exception
   {
      public AssemblyException(int lineNum, string reason) :
          base("Line " + lineNum + ": " + reason)
      {
         m_LineNum = lineNum;
         m_Reason = reason;
      }

      public int LineNumber
      {
         get { return m_LineNum; }
      }

      public string Reason
      {
         get { return m_Reason; }
      }

      private readonly int m_LineNum;
      private readonly string m_Reason;
   }
}
