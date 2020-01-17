using Assembler.FormsGui.Commands;
using Assembler.FormsGui.Controls;
using Assembler.FormsGui.Messaging;
using Assembler.FormsGui.Services;
using Assembler.FormsGui.Utility;
using Assembler.FormsGui.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembler.FormsGui.Views
{
   public partial class HexExplorerView : ViewBase
   {
      public HexExplorerView()
      {
         // designer requires this - do not call from user code.
         InitializeComponent();
      }

      public HexExplorerView(int viewId, MessageManager msgMgr):
         base(viewId, "Hex Explorer", msgMgr)
      {
         m_ExplorerVm = new HexExplorerViewModel(viewId, msgMgr);
         m_OpenFileCmd = new RelayCommand<string>((filePath) => LoadFileAction(filePath), true);

         
         m_CloseTabsToRightCmd = new RelayCommand<int>(
            (param) =>
            {
               // if we're removing everything to the right, the AllOpenFiles
               // Count value will change. we'll just wait until that Count property
               // is one above our target tab (since that'd be the theoretic last tab).
               int targetCount = param + 1;
               while (m_ExplorerVm.AllOpenFiles.Count > targetCount)
               {
                  CloseTabAction(param + 1);
               }
            }, false);

         m_CloseTabsToLeftCmd = new RelayCommand<int>(
            (param) =>
            {
               for (int i = param - 1; i >= 0; --i)
               {
                  CloseTabAction(i);
               }

               // set the active tab index to 0. the tab changed
               // event doesn't seem to be getting called. it hasn't
               // crashed yet, but that seems like it'd be in a bad state.
               m_ExplorerVm.ChangeActiveIndexCommand.Execute(0);
            }, false);

         // this will be passed either a tab index or a view model.
         // need to differentiate on the fly when we're called.
         m_CloseTabCmd = new RelayCommand<object>(
               (param) =>
               {
                  int? iParm = param as int?;
                  if (iParm.HasValue)
                  {
                     CloseTabAction(iParm.Value);
                  }
                  else
                  {
                     var evm = param as HexExplorerViewModel;
                     System.Diagnostics.Debug.Assert(evm != null);
                     CloseTabAction(evm.ActiveFileIndex);
                  }
               }, false
         );
         InitializeComponent();
         CreateDataBindings(m_ExplorerVm);
         SubscribeToMessageType(MessageType.FileAssembled, m_OpenFileCmd);
      }

      private TabPage CreateNewTabPage(CompiledFileViewModel viewModel)
      {
         var newTab = new TabPage();
         newTab.DataBindings.Add(new Binding(nameof(newTab.Text), viewModel, nameof(viewModel.FileName)));
         var tabContent = new HexValueGrid(viewModel)
         {
            Dock = DockStyle.Fill
         };
         newTab.Controls.Add(tabContent);
         AreAnyFilesOpened = true;
         m_NoFilesAssembledLbl.Visible = false;
         m_FileTabCtrl.Visible = true;

         return newTab;
      }

      private void CreateDataBindings(HexExplorerViewModel viewModel)
      {
         m_FileTabCtrl.TabPages.BindToObservableCollection(viewModel.AllOpenFiles,
                                                           (avm) => CreateNewTabPage(avm));

         m_FileTabCtrl.DataBindings.Add(new Binding(nameof(m_FileTabCtrl.SelectedIndex), viewModel, nameof(viewModel.ActiveFileIndex), 
            true, DataSourceUpdateMode.OnPropertyChanged));
      }

      private void LoadFileAction(string filePath)
      {
         try
         {
            m_ExplorerVm.LoadFileCommand.Execute(filePath);
         }
         catch (Exception ex)
         {
            MessageBox.Show("Failed to load hex representation: " + ex.Message,
                            "Hex Editor Load Failure",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
         }
      }
      
      private void SaveFileAction()
      {
         IDialogService service = DialogServiceFactory.GetServiceInstance();
         try
         {
            bool okToContinue = true;
            string filePath = m_ExplorerVm.ActiveFile.FilePath;
            if (!m_ExplorerVm.ActiveFile.IsFileBackedPhysically)
            {
               var options = new DialogOptions()
               {
                  DefaultFileName = "Untitled.jef",
                  FileFilter = "JEF Compiled File (*.jef)|*.jef",
                  WindowTitle = "Save File"
               };

               okToContinue = service.ShowSaveFileDialog(options, out filePath);
            }

            if (okToContinue)
            {
               m_ExplorerVm.SaveFileCommand.Execute(filePath);
            }
         }
         catch (Exception ex)
         {
            service.ShowErrorDialog("Save Error", ex.Message);
         }
      }

      private void CloseTabAction(int tabIdx)
      {
         bool continueClosing = true;
         CompiledFileViewModel avm = m_ExplorerVm.AllOpenFiles[tabIdx];

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
            m_ExplorerVm.CloseFileCommand.Execute(tabIdx);
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
         while (targetTabIdx < m_FileTabCtrl.TabCount)
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
         int numTabsToClose = m_FileTabCtrl.TabCount;
         for (int closeCount = 0; closeCount < numTabsToClose; ++closeCount)
         {
            CloseTab(0);
         }
      }

      private void CloseTab(int index)
      {
         bool continueClosing = true;
         var fileViewModel = m_ExplorerVm.AllOpenFiles[index];
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
            m_ExplorerVm.CloseFileCommand.Execute(index);

            AreAnyFilesOpened = m_ExplorerVm.AllOpenFiles.Any();
            if (!AreAnyFilesOpened)
            {
               m_NoFilesAssembledLbl.Visible = true;
               m_FileTabCtrl.Visible = false;
            }
         }
      }

      private readonly HexExplorerViewModel m_ExplorerVm;

      private readonly RelayCommand<string> m_OpenFileCmd;

      private readonly RelayCommand<object> m_CloseTabCmd;
      private readonly RelayCommand<int> m_CloseTabsToLeftCmd;
      private readonly RelayCommand<int> m_CloseTabsToRightCmd;
   }
}
