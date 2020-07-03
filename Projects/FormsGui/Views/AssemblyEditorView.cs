using Assembler.Common;
using Assembler.FormsGui.Controls;
using Assembler.FormsGui.Messaging;
using Assembler.FormsGui.Services;
using Assembler.FormsGui.Utility;
using Assembler.FormsGui.ViewModels;
using Assembler.UICommon.Commands;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Assembler.FormsGui.Views
{
   public partial class AssemblyEditorView : ViewBase
   {
      public AssemblyEditorView()
      {
         // designer requires this - do not call from user code.
         InitializeComponent();
      }

      public AssemblyEditorView(int viewId, 
                                MessageManager msgMgr,
                                PreferencesViewModel preferences) :
         base(viewId, "Assembly Editor", msgMgr)
      {
         m_Preferences = preferences;
         m_EditorVm = new AssemblyEditorViewModel(viewId, msgMgr);
         m_CreateNewFileCmd = new RelayCommand(() => NewFileAction(), true);
         m_OpenFileCmd = new RelayCommand(() => LoadFileAction(), true);
         m_SaveFileAsCmd = new RelayCommand(() => SaveFileAsAction(), true);
         m_SaveFileCmd = new RelayCommand(() => SaveFileAction(), true);
         m_ImportFileCmd = new RelayCommand(() => ImportFileAction(), true);
         m_AssembleFileCmd = new RelayCommand<OutputTypes>((OutputTypes outputType) =>
            AssembleActiveFileAction(outputType), true);

         m_CloseAllFilesCmd = new RelayCommand(() => CloseAllFiles(), true);

         InitializeComponent();
         CreateDataBindings(m_EditorVm);

         SubscribeToMessageType(MessageType.CreateFileRequest, m_CreateNewFileCmd);
         SubscribeToMessageType(MessageType.OpenFileRequest, m_OpenFileCmd);
         SubscribeToMessageType(MessageType.SaveFileRequest, m_SaveFileCmd);
         SubscribeToMessageType(MessageType.SaveFileAsRequest, m_SaveFileAsCmd);
         SubscribeToMessageType(MessageType.DisassembleFileRequest, m_ImportFileCmd);
         SubscribeToMessageType(MessageType.AssembleFileRequest, m_AssembleFileCmd);
         SubscribeToMessageType(MessageType.WindowClosingNotification, m_CloseAllFilesCmd);
      }

      private TabPage CreateNewTabPage(AssemblyFileViewModel viewModel)
      {
         var newTab = new TabPage();
         newTab.DataBindings.Add(new Binding(nameof(newTab.Text), viewModel, nameof(viewModel.FileName)));
         var tabContent = new FileEditor(viewModel, m_Preferences)
         {
            Dock = DockStyle.Fill
         };
         newTab.Controls.Add(tabContent);
         AreAnyFilesOpened = (m_EditorVm.AllOpenFiles.Count > 0);

         return newTab;
      }

      private void CreateDataBindings(AssemblyEditorViewModel viewModel)
      {
         m_OpenFileTabs.TabPages.BindToObservableCollection(m_EditorVm.AllOpenFiles,
                                                            (avm) => CreateNewTabPage(avm));

         m_OpenFileTabs.DataBindings.Add(new Binding(nameof(m_OpenFileTabs.SelectedIndex), m_EditorVm, 
            nameof(m_EditorVm.ActiveFileIndex), true, DataSourceUpdateMode.OnPropertyChanged));
      }

      private void NewFileAction()
      {
         m_EditorVm.NewFileCommand.Execute(null);
      }

      private void LoadFileAction()
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
               Cursor.Current = Cursors.WaitCursor;
               m_EditorVm.LoadFileCommand.Execute(filePath);
            }
         }
         catch (Exception ex)
         {
            service.ShowErrorDialog("Save Error", ex.Message);
         }
         finally
         {
            Cursor.Current = Cursors.Default;
         }
      }

      private void SaveFileAction()
      {
         IDialogService service = DialogServiceFactory.GetServiceInstance();
         try
         {
            bool okToContinue = true;
            string filePath = m_EditorVm.ActiveFile.FilePath;
            if (!m_EditorVm.ActiveFile.IsFileBackedPhysically)
            {
               var options = new DialogOptions()
               {
                  DefaultFileName = "Untitled.asm",
                  FileFilter = "RISC-V Assembly File (*.asm)|*.asm",
                  WindowTitle = "Save File"
               };

               okToContinue = service.ShowSaveFileDialog(options, out filePath);
            }

            if (okToContinue)
            {
               Cursor.Current = Cursors.WaitCursor;
               m_EditorVm.SaveFileCommand.Execute(filePath);
            }
         }
         catch (Exception ex)
         {
            service.ShowErrorDialog("Save Error", ex.Message);
         }
         finally
         {
            Cursor.Current = Cursors.Default;
         }
      }

      private void SaveFileAsAction()
      {
         IDialogService service = DialogServiceFactory.GetServiceInstance();
         try
         {
            string defaultFileName = m_EditorVm.ActiveFile.FileName;
            if (string.IsNullOrEmpty(defaultFileName))
            {
               defaultFileName = "Untitled.asm";
            }

            // if there is an asterisk, strike that prior to printing.
            if (defaultFileName[defaultFileName.Length - 1] == '*')
            {
               defaultFileName = defaultFileName.Remove(defaultFileName.Length - 1);
            }

            var options = new DialogOptions()
            {
               DefaultFileName = defaultFileName,
               FileFilter = "RISC-V Assembly File (*.asm)|*.asm",
               WindowTitle = "Save File"
            };

            bool okToContinue = service.ShowSaveFileDialog(options, out string filePath);

            if (okToContinue)
            {
               Cursor.Current = Cursors.WaitCursor;
               m_EditorVm.SaveFileCommand.Execute(filePath);
            }
         }
         catch (Exception ex)
         {
            service.ShowErrorDialog("Save Error", ex.Message);
         }
         finally
         {
            Cursor.Current = Cursors.Default;
         }
      }

      private void ImportFileAction()
      {
         IDialogService service = DialogServiceFactory.GetServiceInstance();
         try
         {
            var options = new DialogOptions()
            {
               FileFilter = "RISC-V ELF object file (*.o)|*.o|RISC-V JEF object file (*.jef)|*.jef",
               WindowTitle = "Import Compiled File"
            };

            bool okToContinue = service.ShowOpenFileDialog(options, out string filePath);

            if (okToContinue)
            {
               m_EditorVm.DisassembleAndImportCmd.Execute(filePath);
            }
         }
         catch (Exception ex)
         {
            service.ShowErrorDialog("Import Error", ex.Message);
         }
         finally
         {
            Cursor.Current = Cursors.Default;
         }
      }

      private void AssembleActiveFileAction(OutputTypes outputType)
      {
         AssemblyFileViewModel viewModel = m_EditorVm.ActiveFile;
         bool continueAssembling = true;
         if (!viewModel.IsFileBackedPhysically)
         {
            SaveFileAction();

            // if this is still false, the user may have backed out of the save box.
            if (!viewModel.IsFileBackedPhysically)
            {
               continueAssembling = false;
            }
         }
         else if (viewModel.AreAnyChangedUnsaved)
         {
            string fileNameNoAsterisk = viewModel.FileName.Substring(0, viewModel.FileName.LastIndexOf('*'));
            DialogResult dr = MessageBox.Show(fileNameNoAsterisk + " has unsaved changes. Do you wish to save before running assembler?",
                                              "Unsaved Changes",
                                              MessageBoxButtons.YesNoCancel,
                                              MessageBoxIcon.Question);

            switch (dr)
            {
               case DialogResult.Yes:
               {
                  SaveFileAction();
                  break;
               }
               case DialogResult.Cancel:
               {
                  continueAssembling = false;
                  break;
               }
            }
         }

         if (continueAssembling)
         {
            Cursor.Current = Cursors.WaitCursor;
            AssemblyCommandParams cmdParams = default(AssemblyCommandParams);
            cmdParams.InputFileName = viewModel.FilePath;
            string fileNoExt = viewModel.FilePath.Substring(0, viewModel.FilePath.LastIndexOf('.'));
            switch (outputType)
            {
               case OutputTypes.DirectBinary:
               {
                  cmdParams.OutputFileName = fileNoExt + ".jef";
                  break;
               }

               case OutputTypes.ELF:
               {
                  cmdParams.OutputFileName = fileNoExt + ".o";
                  break;
               }

               default:
                  System.Diagnostics.Debug.Assert(false);
                  break;
            }

            m_EditorVm.AssembleFileCmd.Execute(cmdParams);
            Cursor.Current = Cursors.Default;
         }
      }

      private void TabControl_OnMouseUp(object sender, MouseEventArgs e)
      {
         if (e.Button == MouseButtons.Right)
         {
            var ctrl = sender as TabControl;
            for (int tabItr = 0; tabItr < ctrl.TabCount; ++tabItr)
            {
               Rectangle headerRect = ctrl.GetTabRect(tabItr);
               if (headerRect.Contains(e.Location))
               {
                  // store the clicked tab index for retrieval when we handle
                  // the context menu click events.
                  foreach (ToolStripItem menuItem in m_TabRightClickMenu.Items)
                  {
                     menuItem.Tag = tabItr;
                  }

                  m_TabRightClickMenu.Show(ctrl, e.Location);
                  break;
               }
            }
         }
      }

      private void TabControl_OnCurrentTabChanged(object sender, TabControlEventArgs e)
      {
         if (e.TabPageIndex >= 0)
         {
            m_EditorVm.ChangeActiveIndexCommand.Execute(e.TabPageIndex);
         }
      }

      private void OnCloseTabClicked(object sender, EventArgs e)
      {
         var contextMenu = sender as ToolStripMenuItem;
         int tabIdxToClose = (int)contextMenu.Tag;
         CloseTab(tabIdxToClose);
      }

      private void OnCloseAllTabsToRightClicked(object sender, EventArgs e)
      {
         var contextMenu = sender as ToolStripMenuItem;
         int targetTabIdx = (int)contextMenu.Tag;
         ++targetTabIdx;
         while (targetTabIdx < m_OpenFileTabs.TabCount)
         {
            CloseTab(targetTabIdx);
         }
      }

      private void OnCloseAllTabsToLeftClicked(object sender, EventArgs e)
      {
         var contextMenu = sender as ToolStripMenuItem;
         int targetTabIdx = (int)contextMenu.Tag;
         int numTabsToClose = targetTabIdx;
         for (int closeCount = 0; closeCount < numTabsToClose; ++closeCount)
         {
            CloseTab(0);
         }
      }

      private void OnCloseAllTabsClicked(object sender, EventArgs e)
      {
         int numTabsToClose = m_OpenFileTabs.TabCount;
         for (int closeCount = 0; closeCount < numTabsToClose; ++closeCount)
         {
            CloseTab(0);
         }
      }

      private void CloseTab(int index)
      {
         bool continueClosing = true;
         var fileViewModel = m_EditorVm.AllOpenFiles[index];
         if (fileViewModel.AreAnyChangedUnsaved)
         {
            string fileNameNoAsterisk = fileViewModel.FileName.Substring(0, fileViewModel.FileName.LastIndexOf('*'));
            DialogResult dr = MessageBox.Show(fileNameNoAsterisk + " has unsaved changes. Do you wish to save before closing?",
                                              "Unsaved Changes",
                                              MessageBoxButtons.YesNoCancel,
                                              MessageBoxIcon.Question);

            switch (dr)
            {
               case DialogResult.Yes:
               {
                  SaveFileAction();
                  break;
               }
               case DialogResult.Cancel:
               {
                  continueClosing = false;
                  break;
               }
            }
         }

         if (continueClosing)
         {
            m_EditorVm.CloseFileCommand.Execute(index);
         }
      }

      private void CloseAllFiles()
      {
         for (int i = 0; i < m_OpenFileTabs.TabCount; ++i)
         {
            CloseTab(0);
         }
      }

      private readonly AssemblyEditorViewModel m_EditorVm;
      private readonly PreferencesViewModel m_Preferences;

      private readonly RelayCommand m_CreateNewFileCmd;
      private readonly RelayCommand m_OpenFileCmd;
      private readonly RelayCommand m_SaveFileCmd;
      private readonly RelayCommand m_SaveFileAsCmd;
      private readonly RelayCommand m_ImportFileCmd;
      private readonly RelayCommand<OutputTypes> m_AssembleFileCmd;
      private readonly RelayCommand m_CloseAllFilesCmd;
   }
}
