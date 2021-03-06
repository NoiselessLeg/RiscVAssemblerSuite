﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assembler.UICommon.Commands;

namespace Assembler.FormsGui.Messaging
{
   public class BasicMessage : IBasicMessage
   {
      public BasicMessage(MessageType type)
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
            ExecuteCommand(handlerCmd);
         }
      }

      protected virtual void ExecuteCommand(ICommand handlerCmd)
      {
         handlerCmd.Execute(null);
      }

      private readonly MessageType m_Type;
   }
}
