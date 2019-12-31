using Assembler.FormsGui.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.Messaging
{
   public enum MessageType
   {

   }

   public interface IBasicMessage
   {
      MessageType MessageType
      {
         get;
      }

      void HandleMessage(ICommand handlerCmd);

   }
}
