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
         m_ViewId = viewId;
         m_DisassemblyMgr = new DisassemblyToTextHelper();
         m_LoggerVm = new LoggerViewModel();
         m_FilesToExecute = new ObservableCollection<JefFileViewModel>();
         m_ExternalMsgQueue = new ObservableQueue<IBasicMessage>();
         m_ExternalMsgQueue.ItemEnqueued += OnExternalMsgReceived;
         m_FileProc = new JefFileProcessor();
         m_LoadFileCmd = new RelayCommand(
            (param) => LoadFile(param as string)
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

      public int ActiveTabIdx
      {
         get { return m_ActiveTabIdx; }
         set
         {
            if (m_ActiveTabIdx != value)
            {
               m_ActiveTabIdx = value;
               OnPropertyChanged();
            }
         }
      }

      public ObservableCollection<JefFileViewModel> FilesToExecute
      {
         get { return m_FilesToExecute; }
      }

      private void LoadFile(string fileName)
      {
         DisassembledFile file = m_FileProc.ProcessJefFile(fileName, m_LoggerVm.Logger);
         DataModels.AssemblyFile disassembly = m_DisassemblyMgr.DiassembleCompiledFile(fileName, m_LoggerVm.Logger);
         m_FilesToExecute.Add(new JefFileViewModel(fileName, disassembly, file));
         ActiveTabIdx = (m_FilesToExecute.Count - 1);
      }

      private void OnExternalMsgReceived(object sender, EventArgs e)
      {
         var msgQ = sender as IBasicQueue<IBasicMessage>;
         IBasicMessage msg = msgQ.Dequeue();
         switch (msg.MessageType)
         {
            case MessageType.FileAssembled:
            {
               msg.HandleMessage(LoadFileCommand);
               var activeViewRequest = new ActiveViewRequestMessage(m_ViewId);
               m_MsgMgr.BroadcastMessage(m_MsgSenderId, activeViewRequest);
               break;
            }
         }
      }

      private int m_ActiveTabIdx;
      private readonly int m_ViewId;
      private readonly int m_MsgSenderId;
      private readonly MessageManager m_MsgMgr;
      private readonly DisassemblyToTextHelper m_DisassemblyMgr;
      private readonly ObservableQueue<IBasicMessage> m_ExternalMsgQueue;

      private readonly RelayCommand m_LoadFileCmd;
      private readonly RelayCommand m_RunFileCmd;
      private readonly JefFileProcessor m_FileProc;
      private readonly LoggerViewModel m_LoggerVm;

      private readonly ObservableCollection<JefFileViewModel> m_FilesToExecute;
   }
}
