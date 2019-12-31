using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assembler.FormsGui.Commands;

namespace Assembler.FormsGui.Messaging
{
   public class FileAssembledMessage : IBasicMessage
   {
      public FileAssembledMessage(string assembledFilePath)
      {
         m_CompiledFilePath = assembledFilePath;
      }

      public MessageType MessageType
      {
         get { return MessageType.FileAssembled; }
      }

      public void HandleMessage(ICommand handlerCmd)
      {
         if (handlerCmd.CanExecute(m_CompiledFilePath))
         {
            handlerCmd.Execute(m_CompiledFilePath);
         }
      }

      private readonly string m_CompiledFilePath;
   }
}
