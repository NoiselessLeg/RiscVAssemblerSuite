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
   public class DebugWindowViewModel : BaseViewModel
   {
      public DebugWindowViewModel(int viewId, MessageManager msgMgr)
      {
         m_LoggerVm = new LoggerViewModel();
         m_ExternalMsgQueue = new ObservableQueue<IBasicMessage>();
         m_ExternalMsgQueue.ItemEnqueued += OnExternalMsgReceived;
         m_FileProc = new JefFileProcessor();
         m_LoadFileCmd = new RelayCommand(
            (param) => LoadFile(param as string)
         );
         m_RunFileCmd = new RelayCommand(
            (param) => RunFile(param as string)
         );

         m_MsgSenderId = msgMgr.RegisterMessageQueue(m_ExternalMsgQueue);
         m_MsgMgr = msgMgr;
      }

      public IBasicQueue<IBasicMessage> MessageQueue
      {
         get { return m_ExternalMsgQueue; }
      }

      public ICommand LoadFileCommand
      {
         get { return m_LoadFileCmd; }
      }

      public ICommand RunFileCommand
      {
         get { return m_RunFileCmd; }
      }

      private void LoadFile(string fileName)
      {
         DisassembledFile file = m_FileProc.ProcessJefFile(fileName, m_LoggerVm.Logger);


      }

      private void RunFile(string fileName)
      {

      }

      private void OnExternalMsgReceived(object sender, EventArgs e)
      {
         var msgQ = sender as IBasicQueue<IBasicMessage>;
         IBasicMessage msg = msgQ.Dequeue();
      }

      private readonly int m_MsgSenderId;
      private readonly MessageManager m_MsgMgr;
      private readonly ObservableQueue<IBasicMessage> m_ExternalMsgQueue;

      private readonly RelayCommand m_LoadFileCmd;
      private readonly RelayCommand m_RunFileCmd;
      private readonly JefFileProcessor m_FileProc;
      private readonly LoggerViewModel m_LoggerVm;


      private readonly ObservableCollection<ExecutionViewModel> m_ExecutingFiles;
   }
}
