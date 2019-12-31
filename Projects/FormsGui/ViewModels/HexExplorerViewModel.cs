using Assembler.FormsGui.Commands;
using Assembler.FormsGui.IO;
using Assembler.FormsGui.Messaging;
using Assembler.FormsGui.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.ViewModels
{
   public class HexExplorerViewModel : NotifyPropertyChangedBase
   {
      public HexExplorerViewModel(int viewId, MessageManager msgMgr)
      {
         m_ViewId = viewId;
         m_MsgQueue = new ObservableQueue<IBasicMessage>();
         m_MsgQueue.ItemEnqueued += OnExternalMsgReceived;
         m_OpenFiles = new ObservableCollection<CompiledFileViewModel>();
         m_OpenFileCmd = new RelayCommand((param) => LoadFile(param as string));
         m_SaveFileCmd = new RelayCommand(param => SaveFile(param as string));
         m_CloseFileCmd = new RelayCommand(param =>
         {
            int? iParm = param as int?;
            System.Diagnostics.Debug.Assert(iParm.HasValue);
            if (iParm.HasValue)
            {
               CloseFile(iParm.Value);
            }

         });
         m_ChangeActiveIdxCmd = new RelayCommand(param => ActiveFileIndex = (param as int?).Value);

         m_MsgSenderId = msgMgr.RegisterMessageQueue(m_MsgQueue);
         m_MsgMgr = msgMgr;
      }

      public IBasicQueue<IBasicMessage> MessageQueue
      {
         get { return m_MsgQueue; }
      }
      
      public ObservableCollection<CompiledFileViewModel> AllOpenFiles
      {
         get { return m_OpenFiles; }
      }

      public int ActiveFileIndex
      {
         get { return m_ActiveViewModelIdx; }
         private set
         {
            if (m_ActiveViewModelIdx != value)
            {
               m_ActiveViewModelIdx = value;
               OnPropertyChanged();
            }
         }
      }

      public CompiledFileViewModel ActiveFile
      {
         get { return m_OpenFiles[ActiveFileIndex]; }
      }

      public ICommand LoadFileCommand
      {
         get { return m_OpenFileCmd; }
      }

      public ICommand SaveFileCommand
      {
         get { return m_SaveFileCmd; }
      }

      public ICommand CloseFileCommand
      {
         get { return m_CloseFileCmd; }
      }

      public ICommand ChangeActiveIndexCommand
      {
         get { return m_ChangeActiveIdxCmd; }
      }

      private void LoadFile(string fileName)
      {
         // see if we already have this file open.
         if (!m_OpenFiles.Contains((vm) => vm.FilePath == fileName))
         {
            DataModels.CompiledFile newFile = BinaryFileLoader.LoadFile(fileName);
            var newVm = new CompiledFileViewModel(newFile);
            m_OpenFiles.Add(newVm);
            ActiveFileIndex = m_OpenFiles.Count - 1;
         }
         else
         {
            ActiveFileIndex = m_OpenFiles.IndexOf(vm => vm.FileName == fileName);
         }
      }

      private void SaveFile(string fileName)
      {
         CompiledFileViewModel targetVm = m_OpenFiles[ActiveFileIndex];
         targetVm.SaveFileAs(fileName);
      }

      private void CloseFile(int fileIndex)
      {
         m_OpenFiles.RemoveAt(fileIndex);
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

               // once we load the file, set us as the active view.
               var activeRequest = new ActiveViewRequestMessage(m_ViewId);
               m_MsgMgr.BroadcastMessage(m_MsgSenderId, activeRequest);
               break;
            }
         }
      }


      private int m_ActiveViewModelIdx;
      private readonly int m_ViewId;
      private readonly int m_MsgSenderId;
      private readonly MessageManager m_MsgMgr;
      private readonly ObservableQueue<IBasicMessage> m_MsgQueue;
      private readonly ObservableCollection<CompiledFileViewModel> m_OpenFiles;
      private readonly RelayCommand m_OpenFileCmd;
      private readonly RelayCommand m_SaveFileCmd;
      private readonly RelayCommand m_CloseFileCmd;
      private readonly RelayCommand m_ChangeActiveIdxCmd;
   }
}
