using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assembler.FormsGui.Commands;

namespace Assembler.FormsGui.Messaging
{
   public struct DebugActivity
   {
      int ObjId;
      bool IsActivelyDebugging;
   }

   public class DebugActivityNotification : BasicMessage
   {
      public DebugActivityNotification(DebugActivity activity):
         base(MessageType.DebugActivityNotification)
      {
         m_DbgStatus = activity;
      }
      
      protected override void ExecuteCommand(ICommand handlerCmd)
      {
         handlerCmd.Execute(m_DbgStatus);
      }

      private readonly DebugActivity m_DbgStatus;

   }
}
