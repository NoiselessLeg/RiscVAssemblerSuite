using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assembler.Common;
using Assembler.FormsGui.Commands;

namespace Assembler.FormsGui.Messaging
{
   public class AssembleFileMessage : BasicMessage
   {
      public AssembleFileMessage(OutputTypes fileOutputType):
         base(MessageType.AssembleFileRequest)
      {
         m_OutputType = fileOutputType;
      }

      protected override void ExecuteCommand(ICommand handlerCmd)
      {
         handlerCmd.Execute(m_OutputType);
      }

      private readonly OutputTypes m_OutputType;
   }
}
