﻿using Assembler.Disassembler;
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
   public class AssemblyEditorViewModel : NotifyPropertyChangedBase
   {
      public AssemblyEditorViewModel(MessageManager msgMgr)
      {
         m_ExternalMsgQueue = new ObservableQueue<IBasicMessage>();
         m_Disassembler = new DisassemblyManager();
         m_OpenViewModels = new ObservableCollection<AssemblyFileViewModel>();
         m_OpenViewModels.Add(new AssemblyFileViewModel());

         m_Assembler = new RiscVAssembler();
         m_LoggerVm = new LoggerViewModel();
         m_AssembleFileCmd = new RelayCommand(param => AssembleFile(param as string));
         m_NewFileCmd = new RelayCommand(param => CreateNewFile());
         m_OpenFileCmd = new RelayCommand(param => OpenFile(param as string));
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
         m_DisassembleAndImportCmd = new RelayCommand(param => DisassembleAndImportFile(param as string));
         m_ChangeActiveIdxCmd = new RelayCommand(param => ActiveFileIndex = (param as int?).Value);
         m_MsgMgr = msgMgr;
         m_MsgMgr.RegisterMessageQueue(m_ExternalMsgQueue);
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

      public IBasicQueue<IBasicMessage> MessageQueue
      {
         get { return m_ExternalMsgQueue; }
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

      private void CreateNewFile()
      {
         var newVm = new AssemblyFileViewModel(new DataModels.AssemblyFile());
         m_OpenViewModels.Add(newVm);
         ActiveFileIndex = m_OpenViewModels.Count - 1;
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
      }

      private void AssembleFile(string fileName)
      {
         Common.AssemblerOptions options = new Common.AssemblerOptions(new[] { fileName });
         m_Assembler.Assemble(options, m_LoggerVm.Logger);
      }

      private int m_ActiveViewModelIdx;

      private readonly MessageManager m_MsgMgr;
      private readonly ObservableQueue<IBasicMessage> m_ExternalMsgQueue;
      private readonly ObservableCollection<AssemblyFileViewModel> m_OpenViewModels;
      private readonly RiscVAssembler m_Assembler;
      private readonly DisassemblyManager m_Disassembler;
      
      private readonly LoggerViewModel m_LoggerVm;

      private readonly RelayCommand m_NewFileCmd;
      private readonly RelayCommand m_OpenFileCmd;
      private readonly RelayCommand m_AssembleFileCmd;
      private readonly RelayCommand m_SaveFileCmd;
      private readonly RelayCommand m_CloseFileCmd;
      private readonly RelayCommand m_ChangeActiveIdxCmd;
      private readonly RelayCommand m_DisassembleAndImportCmd;
   }
}
