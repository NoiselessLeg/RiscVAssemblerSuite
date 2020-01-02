using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assembler.FormsGui.Commands;

namespace Assembler.FormsGui.Messaging
{
   public class Message<TPredParam, TExParam> : IBasicMessage
   {
      public Message(MessageType type, TPredParam predParam, TExParam exParam)
      {
         m_Type = type;
         m_PredParam = predParam;
         m_ExParam = exParam;
      }

      public MessageType MessageType
      {
         get { return m_Type; }
      }
      public void HandleMessage(ICommand handlerCmd)
      {
         if (handlerCmd.CanExecute(m_PredParam))
         {
            handlerCmd.Execute(m_ExParam);
         }
      }

      private readonly MessageType m_Type;
      private readonly TPredParam m_PredParam;
      private readonly TExParam m_ExParam;
   }

   public class Message<TExParam> : IBasicMessage
   {
      public Message(MessageType type, TExParam param)
      {
         m_Type = type;
         m_Parm = param;
      }

      public MessageType MessageType
      {
         get { return m_Type; }
      }
      public void HandleMessage(ICommand handlerCmd)
      {
         handlerCmd.Execute(m_Parm);
      }

      private readonly MessageType m_Type;
      private readonly TExParam m_Parm;
   }

   public class Message : IBasicMessage
   {
      public Message(MessageType type)
      {
         m_Type = type;
      }

      public MessageType MessageType
      {
         get { return m_Type; }
      }
      public void HandleMessage(ICommand handlerCmd)
      {
         handlerCmd.Execute(null);
      }

      private readonly MessageType m_Type;
   }
}
