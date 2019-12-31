using Assembler.Disassembler;
using Assembler.FormsGui.Commands;
using Assembler.FormsGui.Messaging;
using Assembler.FormsGui.Utility;
using Assembler.OutputProcessing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.ViewModels
{
   public class DebugWindowViewModel : NotifyPropertyChangedBase
   {
      public DebugWindowViewModel(MessageManager msgMgr)
      {
         m_ExternalMsgQueue = new ObservableQueue<IBasicMessage>();
         m_FileProc = new JefFileProcessor();
         m_RunFileCmd = new RelayCommand(
            (param) => LoadFile(param as string)
         );

         msgMgr.RegisterMessageQueue(m_ExternalMsgQueue);
         m_MsgMgr = msgMgr;
      }

      public IBasicQueue<IBasicMessage> MessageQueue
      {
         get { return m_ExternalMsgQueue; }
      }

      public ICommand RunFileCommand
      {
         get { return m_RunFileCmd; }
      }

      private void LoadFile(string fileName)
      {
         DisassembledFile file = m_FileProc.ProcessJefFile(fileName, m_LoggerVm.Logger);
         

      }



      private readonly MessageManager m_MsgMgr;
      private readonly ObservableQueue<IBasicMessage> m_ExternalMsgQueue;

      private readonly RelayCommand m_RunFileCmd;
      private readonly JefFileProcessor m_FileProc;
      private readonly LoggerViewModel m_LoggerVm;

      private readonly ObservableCollection<ExecutionViewModel> m_ExecutingFiles;
   }
}
