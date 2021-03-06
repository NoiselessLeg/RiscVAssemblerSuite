﻿
using Assembler.FormsGui.Messaging;
using Assembler.FormsGui.Utility;
using Assembler.OutputProcessing;
using Assembler.OutputProcessing.FileReaders;
using Assembler.UICommon.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembler.FormsGui.ViewModels
{
   public class DebugWindowViewModel : MessagingViewModel
   {
      public DebugWindowViewModel(int viewId, MessageManager msgMgr):
         base(msgMgr)
      {
         m_ViewId = viewId;
         m_LoggerVm = new LoggerViewModel();
         m_FilesToExecute = new ObservableCollection<DisassembledFileViewModel>();
         m_FileProc = new FileReaderFactory();
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

      public ObservableCollection<DisassembledFileViewModel> FilesToExecute
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

         var fileReader = m_FileProc.GetFileParser(fileName);
         DisassembledFileBase file = fileReader.ParseFile(fileName, m_LoggerVm.Logger);
         m_FilesToExecute.Add(new DisassembledFileViewModel(fileName, file));
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

      private readonly RelayCommand<string> m_LoadFileCmd;
      private readonly RelayCommand<string> m_HandleAssembledFileCmd;
      private readonly FileReaderFactory m_FileProc;
      private readonly LoggerViewModel m_LoggerVm;

      private readonly ObservableCollection<DisassembledFileViewModel> m_FilesToExecute;
   }
}
