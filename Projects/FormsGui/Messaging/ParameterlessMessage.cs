using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assembler.FormsGui.Commands;

namespace Assembler.FormsGui.Messaging
{
   public class ParameterlessMessage : IBasicMessage
   {
      public ParameterlessMessage(MessageType type)
      {
         m_Type = type;
      }

      public MessageType MessageType
      {
         get { return m_Type; }
      }

      public void HandleMessage(ICommand handlerCmd)
      {
         if (handlerCmd.CanExecute)
         {
            handlerCmd.Execute(null);
         }
      }

      private readonly MessageType m_Type;
   }
}
