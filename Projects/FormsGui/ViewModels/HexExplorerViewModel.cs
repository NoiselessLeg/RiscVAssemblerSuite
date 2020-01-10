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
   public class HexExplorerViewModel : MessagingViewModel
   {
      public HexExplorerViewModel(int viewId, MessageManager msgMgr):
         base(msgMgr)
      {
         m_ViewId = viewId;
         m_OpenFiles = new ObservableCollection<CompiledFileViewModel>();
         m_OpenFileCmd = new RelayCommand<string>((param) => LoadFile(param), true);
         m_SaveFileCmd = new RelayCommand<string>(param => SaveFile(param), true);
         m_CloseFileCmd = new RelayCommand<int>(param => CloseFile(param), true);
         m_ChangeActiveIdxCmd = new RelayCommand<int>(param => ActiveFileIndex = param, true);
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
         if (fileIndex <= ActiveFileIndex)
         {
            --ActiveFileIndex;
         }
      }


      private int m_ActiveViewModelIdx;
      private readonly int m_ViewId;
      private readonly ObservableCollection<CompiledFileViewModel> m_OpenFiles;
      private readonly RelayCommand<string> m_OpenFileCmd;
      private readonly RelayCommand<string> m_SaveFileCmd;
      private readonly RelayCommand<int> m_CloseFileCmd;
      private readonly RelayCommand<int> m_ChangeActiveIdxCmd;
   }
}
