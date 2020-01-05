using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assembler.FormsGui.Commands;

namespace Assembler.FormsGui.Messaging
{
   public class ActiveViewRequestMessage : IBasicMessage
   {
      public ActiveViewRequestMessage(int viewIdx)
      {
         m_ViewIdx = viewIdx;
      }

      public MessageType MessageType
      {
         get { return MessageType.ActiveViewRequest; }
      }

      public void HandleMessage(ICommand handlerCmd)
      {
         if (handlerCmd.CanExecute)
         {
            handlerCmd.Execute(m_ViewIdx);
         }
      }

      private readonly int m_ViewIdx;
   }
}
