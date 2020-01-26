using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assembler.FormsGui.Commands;

namespace Assembler.FormsGui.Messaging
{
   public class FileAssembledMessage : BasicMessage
   {
      public FileAssembledMessage(string assembledFilePath):
         base(MessageType.FileAssembled)
      {
         m_CompiledFilePath = assembledFilePath;
      }

      protected override void ExecuteCommand(ICommand handlerCmd)
      {
         handlerCmd.Execute(m_CompiledFilePath);
      }

      private readonly string m_CompiledFilePath;
   }
}
