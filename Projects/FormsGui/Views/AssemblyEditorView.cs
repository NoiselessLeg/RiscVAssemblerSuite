using Assembler.FormsGui.Commands;
using Assembler.FormsGui.Controls;
using Assembler.FormsGui.Controls.Custom;
using Assembler.FormsGui.Messaging;
using Assembler.FormsGui.Services;
using Assembler.FormsGui.Utility;
using Assembler.FormsGui.ViewModels;
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
         m_AssembleFileCmd = new RelayCommand(() => AssembleActiveFileAction(), true);

         InitializeComponent();
         CreateDataBindings(m_EditorVm);

         SubscribeToMessageType(MessageType.CreateFileRequest, m_CreateNewFileCmd);
         SubscribeToMessageType(MessageType.OpenFileRequest, m_OpenFileCmd);
         SubscribeToMessageType(MessageType.SaveFileRequest, m_SaveFileCmd);
         SubscribeToMessageType(MessageType.SaveFileAsRequest, m_SaveFileAsCmd);
         SubscribeToMessageType(MessageType.DisassembleFileRequest, m_ImportFileCmd);
         SubscribeToMessageType(MessageType.AssembleFileRequest, m_AssembleFileCmd);
      }

      private TabPage CreateNewTabPage(AssemblyFileViewModel viewModel)
      {
         var newTab = new TabPage();
         newTab.DataBindings.Add(new Binding(nameof(newTab.Text), viewModel, nameof(viewModel.FileName)));
         var tabContent = new AssemblyTextBox(viewModel, m_Preferences);
         tabContent.Dock = DockStyle.Fill;
         newTab.Controls.Add(tabContent);

         return newTab;
      }

      private void CreateDataBindings(AssemblyEditorViewModel viewModel)
      {
         m_OpenFileTabs.TabPages.BindToObservableCollection(m_EditorVm.AllOpenFiles,
                                                            (avm) => CreateNewTabPage(avm));

         m_OpenFileTabs.DataBindings.Add(new Binding(nameof(m_OpenFileTabs.SelectedIndex), m_EditorVm, 
            nameof(m_EditorVm.ActiveFileIndex), true, DataSourceUpdateMode.OnPropertyChanged));
         m_LogTxt.DataBindings.Add(new Binding(nameof(m_LogTxt.Text), m_EditorVm.LoggerModel, 
            nameof(m_EditorVm.LoggerModel.LogText), true, DataSourceUpdateMode.OnPropertyChanged));

         m_NumericValue.DataBindings.Add(new Binding(nameof(m_NumericValue.Text), m_EditorVm, 
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
               m_EditorVm.LoadFileCommand.Execute(filePath);
            }
         }
         catch (Exception ex)
         {
            service.ShowErrorDialog("Save Error", ex.Message);
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
               m_EditorVm.SaveFileCommand.Execute(filePath);
            }
         }
         catch (Exception ex)
         {
            service.ShowErrorDialog("Save Error", ex.Message);
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
            var options = new DialogOptions()
            {
               DefaultFileName = defaultFileName,
               FileFilter = "RISC-V Assembly File (*.asm)|*.asm",
               WindowTitle = "Save File"
            };

            bool okToContinue = service.ShowSaveFileDialog(options, out string filePath);

            if (okToContinue)
            {
               m_EditorVm.SaveFileCommand.Execute(filePath);
            }
         }
         catch (Exception ex)
         {
            service.ShowErrorDialog("Save Error", ex.Message);
         }
      }

      private void CloseTabAction()
      {
         bool continueClosing = true;
         AssemblyFileViewModel avm = m_EditorVm.ActiveFile;
         int activeTabIdx = m_EditorVm.ActiveFileIndex;

         if (avm.AreAnyChangedUnsaved)
         {
            DialogResult dr = MessageBox.Show(avm.FileName + " has unsaved changes. Do you wish to save before closing?",
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
            m_EditorVm.CloseFileCommand.Execute(activeTabIdx);
         }
      }

      private void ImportFileAction()
      {
         IDialogService service = DialogServiceFactory.GetServiceInstance();
         try
         {
            var options = new DialogOptions()
            {
               FileFilter = "RISC-V Compiled File (*.jef)|*.jef",
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
      }

      private void AssembleActiveFileAction()
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
            DialogResult dr = MessageBox.Show(viewModel.FileName + " has unsaved changes. Do you wish to save before running assembler?",
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
            m_EditorVm.AssembleFileCmd.Execute(viewModel.FilePath);
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
                  m_EditorVm.ChangeActiveIndexCommand.Execute(tabItr);

                  var cm = new ContextMenu();
                  //cm.MenuItems.Add(new MenuItem("Close Tab", (s, arg) => { m_CloseTabCmd.Execute(tabItr); }));
                  //cm.MenuItems.Add(new MenuItem("Close all tabs to right", (s, arg) => { m_CloseTabsToRightCmd.Execute(tabItr); }));
                  //cm.MenuItems.Add(new MenuItem("Close all tabs to left", (s, arg) => { m_CloseTabsToLeftCmd.Execute(tabItr); }));
                  cm.Show(ctrl, e.Location);
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

      private readonly MenuBarContext m_Ctx;
      private readonly AssemblyEditorViewModel m_EditorVm;
      private readonly PreferencesViewModel m_Preferences;

      private readonly RelayCommand m_CreateNewFileCmd;
      private readonly RelayCommand m_OpenFileCmd;
      private readonly RelayCommand m_SaveFileCmd;
      private readonly RelayCommand m_SaveFileAsCmd;
      private readonly RelayCommand m_ImportFileCmd;

      private readonly RelayCommand m_CloseWindowCmd;
      private readonly RelayCommand m_AssembleFileCmd;
   }
}
