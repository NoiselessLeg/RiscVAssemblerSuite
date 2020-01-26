using Assembler.FormsGui.Commands;

namespace Assembler.FormsGui.Messaging
{
   public class ActiveViewRequestMessage : BasicMessage
   {
      public ActiveViewRequestMessage(int viewIdx) :
         base(MessageType.ActiveViewRequest)
      {
         m_ViewIdx = viewIdx;
      }

      protected override void ExecuteCommand(ICommand handlerCmd)
      {
         handlerCmd.Execute(m_ViewIdx);
      }

      private readonly int m_ViewIdx;
   }
}
