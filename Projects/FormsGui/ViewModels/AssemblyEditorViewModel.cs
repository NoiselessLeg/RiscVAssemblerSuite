using Assembler.Common;
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
      /// <summary>
      /// Creates an instance of the AssemblyEditorViewModel. This is the view model associated
      /// with the editing of all assembly files, and contains a list of individual assembly file view models.
      /// </summary>
      /// <param name="viewId">The ID of the parent view. Used to make active view requests.</param>
      /// <param name="msgMgr">The message manager used to send messages to other views.</param>
      public AssemblyEditorViewModel(int viewId, MessageManager msgMgr):
         base(msgMgr)
      {
         m_ViewId = viewId;
         m_Disassembler = new DisassemblyManager();
         m_OpenViewModels = new ObservableCollection<AssemblyFileViewModel>();

         m_Assembler = new RiscVAssembler();
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

         CreateNewFile();
      }

      /// <summary>
      /// Gets the index of the view model that is currently focused by the view's tab control.
      /// It is expected that the selected tab and this index are synchronized.
      /// </summary>
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

      /// <summary>
      /// Gets the view model in the open file view model list that
      /// is pointed to by the ActiveFileIndex property.
      /// </summary>
      public AssemblyFileViewModel ActiveFile
      {
         get { return m_OpenViewModels[ActiveFileIndex]; }
      }

      /// <summary>
      /// Gets a collection of all of the open file view models.
      /// </summary>
      public ObservableCollection<AssemblyFileViewModel> AllOpenFiles
      {
         get { return m_OpenViewModels; }
      }

      /// <summary>
      /// This command will create a blank file in the view model,
      /// and add it to the open file list.
      /// </summary>
      public ICommand NewFileCommand
      {
         get { return m_NewFileCmd; }
      }

      /// <summary>
      /// This command will create an assembly view model file based
      /// on data read from a provided file name. It is expected that
      /// the command user will provide the file name as an argument.
      /// </summary>
      public ICommand LoadFileCommand
      {
         get { return m_OpenFileCmd; }
      }

      /// <summary>
      /// This command will commit an assembly view model file to
      /// the file system. It is expected that the command user will
      /// provide the file name as an argument.
      /// </summary>
      public ICommand SaveFileCommand
      {
         get { return m_SaveFileCmd; }
      }

      /// <summary>
      /// This command will remove the specified view model from the 
      /// view model list. It is expected that the command user will
      /// provide the index of the view model to remove as an argument.
      /// </summary>
      public ICommand CloseFileCommand
      {
         get { return m_CloseFileCmd; }
      }

      /// <summary>
      /// This command will change the active view model index.
      /// It is expected that the command user will provide a valid
      /// new index to switch to as an argument.
      /// </summary>
      public ICommand ChangeActiveIndexCommand
      {
         get { return m_ChangeActiveIdxCmd; }
      }

      /// <summary>
      /// This command will call the assembler and assemble
      /// a given file. It is expected that the command user will
      /// pass the path of the file to assemble as an argument.
      /// </summary>
      public ICommand AssembleFileCmd
      {
         get { return m_AssembleFileCmd; }
      }

      /// <summary>
      /// This command will call the disassembler to disassemble
      /// a pre-compiled JEF file, and import the resultant assembly
      /// file into the editor. It is expected that the command user will
      /// pass the path of the file to disassemble as an argument.
      /// </summary>
      public ICommand DisassembleAndImportCmd
      {
         get { return m_DisassembleAndImportCmd; }
      }

      /// <summary>
      /// Creates a new view model isntance with a blank template file,
      /// and adds it to the list of open view models. In addition,
      /// this will allow the CloseFileCommand to execute (as there
      /// is now guaranteed to be a file in the editor to close).
      /// </summary>
      private void CreateNewFile()
      {
         var newVm = new AssemblyFileViewModel(new DataModels.AssemblyFile());
         m_OpenViewModels.Add(newVm);
         ActiveFileIndex = m_OpenViewModels.Count - 1;
         m_CloseFileCmd.CanExecute = true;
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

         var activeViewRequest = new ActiveViewRequestMessage(m_ViewId);
         BroadcastMessage(activeViewRequest);
      }

      private void SaveFile(string fileName)
      {
         AssemblyFileViewModel targetVm = m_OpenViewModels[ActiveFileIndex];
         targetVm.SaveFileAs(fileName);
      }

      private void DisassembleAndImportFile(string fileName)
      {
         var newVm = new AssemblyFileViewModel(fileName, m_Disassembler);
         m_OpenViewModels.Add(newVm);
         ActiveFileIndex = m_OpenViewModels.Count - 1;
         var activeViewRequest = new ActiveViewRequestMessage(m_ViewId);
         BroadcastMessage(activeViewRequest);
      }

      private void CloseFile(int fileIndex)
      {
         m_OpenViewModels.RemoveAt(fileIndex);

         // if our active file index is greater than the removed
         // index, we need to decrement the index.
         if (fileIndex <= ActiveFileIndex)
         {
            --ActiveFileIndex;
         }


         if (m_OpenViewModels.Any())
         {
            m_CloseFileCmd.CanExecute = true;
         }
         else
         {
            m_CloseFileCmd.CanExecute = false;
         }
      }

      private AssemblyFileViewModel GetViewModelByFilePath(string filePath)
      {
         return m_OpenViewModels.First((avm) => avm.FilePath == filePath);
      }

      private void AssembleFile(string fileName)
      {
         AssemblyFileViewModel assembledFile = GetViewModelByFilePath(fileName);
         if (assembledFile.AssembleFile(m_Assembler))
         {
            var fileAssembledMsg = new FileAssembledMessage(assembledFile.AssembledFilePath);
            BroadcastMessage(fileAssembledMsg);
         }
      }
      
      private int m_ActiveViewModelIdx;

      private readonly int m_ViewId;      
      private readonly ObservableCollection<AssemblyFileViewModel> m_OpenViewModels;
      private readonly RiscVAssembler m_Assembler;
      private readonly DisassemblyManager m_Disassembler;

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
