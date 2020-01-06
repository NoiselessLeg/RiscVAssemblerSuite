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
   public class DebugWindowViewModel : MessagingViewModel
   {
      public DebugWindowViewModel(int viewId, MessageManager msgMgr):
         base(msgMgr)
      {
         m_ViewId = viewId;
         m_DisassemblyMgr = new DisassemblyManager();
         m_LoggerVm = new LoggerViewModel();
         m_FilesToExecute = new ObservableCollection<JefFileViewModel>();
         m_FileProc = new JefFileProcessor();
         m_LoadFileCmd = new RelayCommand<string>((param) => LoadFile(param), true);

         m_HandleAssembledFileCmd = new RelayCommand<string>((compiledFileName) => HandleFileAssembledMsg(compiledFileName), true);
      }

      public ICommand LoadFileCommand
      {
         get { return m_LoadFileCmd; }
      }

      public ICommand HandleFileAssembledCommand
      {
         get { return m_HandleAssembledFileCmd; }
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
         // see if we already have this file open. if so, just refresh it
         // by removing the existing version and adding a new one to the model.
         int fileIdx = m_FilesToExecute.IndexOf((vm) => vm.FilePath == fileName);
         if (fileIdx >= 0)
         {
            m_FilesToExecute.RemoveAt(fileIdx);
         }
         DisassembledFile file = m_FileProc.ProcessJefFile(fileName, m_LoggerVm.Logger);
         DataModels.AssemblyFile disassembly = m_DisassemblyMgr.DiassembleCompiledFile(fileName, m_LoggerVm.Logger);
         m_FilesToExecute.Add(new JefFileViewModel(fileName, file));
         ActiveTabIdx = (m_FilesToExecute.Count - 1);
      }

      private void HandleFileAssembledMsg(string compiledFileName)
      {
         LoadFile(compiledFileName);
         var activeViewRequest = new ActiveViewRequestMessage(m_ViewId);
         BroadcastMessage(activeViewRequest);
      }

      private int m_ActiveTabIdx;
      private readonly int m_ViewId;
      private readonly DisassemblyManager m_DisassemblyMgr;

      private readonly RelayCommand<string> m_LoadFileCmd;
      private readonly RelayCommand<string> m_HandleAssembledFileCmd;
      private readonly JefFileProcessor m_FileProc;
      private readonly LoggerViewModel m_LoggerVm;

      private readonly ObservableCollection<JefFileViewModel> m_FilesToExecute;
   }
}
