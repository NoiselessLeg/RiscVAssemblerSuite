using Assembler.Disassembler;
using Assembler.FormsGui.Commands;
using Assembler.FormsGui.Controls;
using Assembler.FormsGui.IO;
using Assembler.FormsGui.Messaging;
using Assembler.FormsGui.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembler.FormsGui.ViewModels
{
   public class AssemblyEditorViewModel : MessagingViewModel
   {
      public AssemblyEditorViewModel(int viewId, MessageManager msgMgr, PreferencesViewModel systemPrefs):
         base(msgMgr)
      {
         m_ViewId = viewId;
         m_SystemPrefsVm = systemPrefs;

         m_TabPageList = new BindingList<TabPage>
         {
            new ViewTabPage<AssemblyTextBox>(new AssemblyTextBox(systemPrefs))
         };

         m_Disassembler = new DisassemblyToTextHelper();

         m_Assembler = new RiscVAssembler();
         m_LoggerVm = new LoggerViewModel();

         m_AssembleFileCmd = new RelayCommand(param => AssembleFile(param as string));
         m_NewFileCmd = new RelayCommand(param => CreateNewFile());
         m_OpenFileCmd = new RelayCommand(param => OpenFile(param as string));
         m_SaveFileCmd = new RelayCommand(param => SaveFile(param as string));
         m_CloseFileCmd = new RelayCommand(param =>
         {
            int iParm = (int)param;
            CloseFile(iParm);
         });

         m_DisassembleAndImportCmd = new RelayCommand(param => DisassembleAndImportFile(param as string));
         m_OpenPreferencesCmd = new RelayCommand(
            (param) =>
            {
               SendExternalMessage(new ParameterlessMessage(MessageType.ShowOptionsRequest));
            }
         );
      }

      public BindingList<TabPage> OpenTabPages
      {
         get { return m_TabPageList; }
      }

      public int SelectedTabIndex
      {
         get { return m_SelectedTabIdx; }
         set
         {
            if (m_SelectedTabIdx != value)
            {
               m_SelectedTabIdx = value;
               OnPropertyChanged();
            }
         }
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
         var tabPage = new ViewTabPage<AssemblyTextBox>(new AssemblyTextBox(m_SystemPrefsVm));
         m_TabPageList.Add(tabPage);
         SelectedTabIndex = m_TabPageList.Count - 1;
      }

      private void OpenFile(string fileName)
      {
         if (TryFindViewModelForFile(fileName, out AssemblyFileViewModel assemblyFile
         /*
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
         */
      }

      private void SaveFile(string fileName)
      {
         /*
         AssemblyFileViewModel targetVm = m_OpenViewModels[ActiveFileIndex];
         targetVm.SaveFileAs(fileName);
         */
      }

      private void DisassembleAndImportFile(string fileName)
      {
         /*
         DataModels.AssemblyFile asmFile = m_Disassembler.DiassembleCompiledFile(fileName, m_LoggerVm.Logger);
         var newVm = new AssemblyFileViewModel(asmFile);
         m_OpenViewModels.Add(newVm);
         ActiveFileIndex = m_OpenViewModels.Count - 1;
         */
      }

      private void CloseFile(int fileIndex)
      {
         /*
         m_OpenViewModels.RemoveAt(fileIndex);
         */
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
         Assembler.Common.AssemblerOptions options = new Assembler.Common.AssemblerOptions(new[] { fileName }, new[] { outputFile });
         if (m_Assembler.Assemble(options, m_LoggerVm.Logger))
         {
            var fileAssembledCmd = new FileAssembledMessage(outputFile);
            SendExternalMessage(fileAssembledCmd);
         }
      }

      private bool TryFindTabIndexForFile(string fileName, out int tabIdx)
      {
         bool found = false;
         fileModel = default(AssemblyFileViewModel);
         for (int i = 0; i < m_TabPageList.Count && !found; ++i)
         {
            var castedTabPage = m_TabPageList[i] as ViewTabPage<AssemblyTextBox>;
            if (castedTabPage.PrimaryControl.ViewModel.FilePath == fileName)
            {
               fileModel = castedTabPage.PrimaryControl.ViewModel;
               found = true;
            }
         }

         return found;
      }

      private readonly int m_ViewId;
      
      private readonly BindingList<TabPage> m_TabPageList;
      
      private readonly RiscVAssembler m_Assembler;
      private readonly DisassemblyToTextHelper m_Disassembler;

      private readonly PreferencesViewModel m_SystemPrefsVm;
      private readonly LoggerViewModel m_LoggerVm;

      private readonly RelayCommand m_NewFileCmd;
      private readonly RelayCommand m_OpenFileCmd;
      private readonly RelayCommand m_AssembleFileCmd;
      private readonly RelayCommand m_SaveFileCmd;
      private readonly RelayCommand m_CloseFileCmd;
      private readonly RelayCommand m_DisassembleAndImportCmd;
      private readonly RelayCommand m_OpenPreferencesCmd;

      private int m_SelectedTabIdx;
   }
}
