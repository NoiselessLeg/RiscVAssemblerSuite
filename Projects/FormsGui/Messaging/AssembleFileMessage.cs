using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assembler.Common;
using Assembler.FormsGui.Commands;

namespace Assembler.FormsGui.Messaging
{
   public class AssembleFileMessage : IBasicMessage
   {
      public AssembleFileMessage(OutputTypes fileOutputType)
      {
         m_OutputType = fileOutputType;
      }

      public void HandleMessage(ICommand handlerCmd)
      {
         if (handlerCmd.CanExecute)
         {
            handlerCmd.Execute(m_OutputType);
         }
      }

      public MessageType MessageType => MessageType.AssembleFileRequest;

      private readonly OutputTypes m_OutputType;
   }
}
