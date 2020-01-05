using Assembler.Disassembler;
using Assembler.FormsGui.Commands;
using Assembler.FormsGui.IO;
using Assembler.FormsGui.Messaging;
using Assembler.FormsGui.Services;
using Assembler.FormsGui.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.ViewModels
{
   public class AssemblyEditorViewModel : MessagingViewModel
   {
      public AssemblyEditorViewModel(int viewId, MessageManager msgMgr):
         base(msgMgr)
      {
         m_ViewId = viewId;
         m_Disassembler = new DisassemblyManager();
         m_OpenViewModels = new ObservableCollection<AssemblyFileViewModel>();
         m_OpenViewModels.Add(new AssemblyFileViewModel());

         m_Assembler = new RiscVAssembler();
         m_LoggerVm = new LoggerViewModel();
         m_AssembleFileCmd = new RelayCommand<string>(param => AssembleFile(param), false);
         m_NewFileCmd = new RelayCommand(() => CreateNewFile(), true);
         m_OpenFileCmd = new RelayCommand<string>((fileName) => OpenFile(fileName), true);
         m_SaveFileCmd = new RelayCommand<string>((fileName) => SaveFile(fileName), true);
         m_CloseFileCmd = new RelayCommand<int>(param => CloseFile(param), false);
         m_DisassembleAndImportCmd = new RelayCommand<string>(param => DisassembleAndImportFile(param), true);
         m_ChangeActiveIdxCmd = new RelayCommand<int>(param => ActiveFileIndex = param, true);
         m_OpenPreferencesCmd = new RelayCommand(
            () =>
            {
               BroadcastMessage(new BasicMessage(MessageType.ShowOptionsRequest));
            },
            true
         );
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

      public AssemblyFileViewModel ActiveFile
      {
         get { return m_OpenViewModels[ActiveFileIndex]; }
      }

      public ObservableCollection<AssemblyFileViewModel> AllOpenFiles
      {
         get { return m_OpenViewModels; }
      }

      public LoggerViewModel LoggerModel
      {
         get { return m_LoggerVm; }
      }

      public ICommand NewFileCommand
      {
         get { return m_NewFileCmd; }
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

      public ICommand AssembleFileCmd
      {
         get { return m_AssembleFileCmd; }
      }

      public ICommand DisassembleAndImportCmd
      {
         get { return m_DisassembleAndImportCmd; }
      }

      public ICommand OpenPreferencesCommand
      {
         get { return m_OpenPreferencesCmd; }
      }

      private void CreateNewFile()
      {
         var newVm = new AssemblyFileViewModel(new DataModels.AssemblyFile());
         m_OpenViewModels.Add(newVm);
         ActiveFileIndex = m_OpenViewModels.Count - 1;
         m_CloseFileCmd.CanExecute = true;
      }

      private void ShowDialogAndOpenFile()
      { 
         IDialogService service = DialogServiceFactory.GetServiceInstance();
         try
         {
            var options = new DialogOptions()
            {
               FileFilter = "RISC-V Assembly File (*.asm)|*.asm",
               WindowTitle = "Open File"
            };

            bool okToContinue = service.ShowOpenFileDialog(options, out string filePath);

            if (okToContinue)
            {
               OpenFile(filePath);
            }
         }
         catch (Exception ex)
         {
            service.ShowErrorDialog("Open error", ex.Message);
         }
      }

      private void ShowDialogAndSaveFile()
      {
         IDialogService service = DialogServiceFactory.GetServiceInstance();
         try
         {
            var options = new DialogOptions()
            {
               FileFilter = "RISC-V Assembly File (*.asm)|*.asm",
               WindowTitle = "Save File"
            };

            bool okToContinue = service.ShowSaveFileDialog(options, out string filePath);

            if (okToContinue)
            {
               OpenFile(filePath);
            }
         }
         catch (Exception ex)
         {
            service.ShowErrorDialog("Save error", ex.Message);
         }
      }

      private void OpenFile(string fileName)
      {
         // see if we already have this file open.
         if (!m_OpenViewModels.Contains((vm) => vm.FilePath == fileName))
         {
            DataModels.AssemblyFile newFile = AssemblyFileLoader.LoadFile(fileName);
            var newVm = new AssemblyFileViewModel(newFile);
            m_OpenViewModels.Add(newVm);
            ActiveFileIndex = m_OpenViewModels.Count - 1;
         }
         else
         {
            ActiveFileIndex = m_OpenViewModels.IndexOf(vm => vm.FileName == fileName);
         }
      }

      private void SaveFile(string fileName)
      {
         AssemblyFileViewModel targetVm = m_OpenViewModels[ActiveFileIndex];
         targetVm.SaveFileAs(fileName);
      }

      private void DisassembleAndImportFile(string fileName)
      {
         DataModels.AssemblyFile asmFile = m_Disassembler.DiassembleCompiledFile(fileName, m_LoggerVm.Logger);
         var newVm = new AssemblyFileViewModel(asmFile);
         m_OpenViewModels.Add(newVm);
         ActiveFileIndex = m_OpenViewModels.Count - 1;
      }

      private void CloseFile(int fileIndex)
      {
         m_OpenViewModels.RemoveAt(fileIndex);
         if (m_OpenViewModels.Any())
         {
            m_CloseFileCmd.CanExecute = true;
         }
         else
         {
            m_CloseFileCmd.CanExecute = false;
         }
      }

      private void AssembleFile(string fileName)
      {
         m_LoggerVm.ClearLogCommand.Execute(null);
         // get the file name with no extension, in case we want intermediate files,
         // or for our output.
         string fileNameNoExtension = fileName;
         if (fileName.Contains("."))
         {
            fileNameNoExtension = fileName.Substring(0, fileName.LastIndexOf('.'));
         }

         //TODO: this will def need to change if we implement more filetypes.
         string outputFile = fileNameNoExtension + ".jef";
         Common.AssemblerOptions options = new Common.AssemblerOptions(new[] { fileName }, new[] { outputFile });
         if (m_Assembler.Assemble(options, m_LoggerVm.Logger))
         {
            var fileAssembledCmd = new FileAssembledMessage(outputFile);
            BroadcastMessage(fileAssembledCmd);
         }
      }
      
      private int m_ActiveViewModelIdx;

      private readonly int m_ViewId;      
      private readonly ObservableCollection<AssemblyFileViewModel> m_OpenViewModels;
      private readonly RiscVAssembler m_Assembler;
      private readonly DisassemblyManager m_Disassembler;
      
      private readonly LoggerViewModel m_LoggerVm;

      private readonly RelayCommand m_NewFileCmd;
      private readonly RelayCommand<string> m_OpenFileCmd;
      private readonly RelayCommand<string> m_AssembleFileCmd;
      private readonly RelayCommand<string> m_SaveFileCmd;
      private readonly RelayCommand<int> m_CloseFileCmd;
      private readonly RelayCommand<int> m_ChangeActiveIdxCmd;
      private readonly RelayCommand<string> m_DisassembleAndImportCmd;
      private readonly RelayCommand m_OpenPreferencesCmd;
   }
}
