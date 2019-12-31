using Assembler.FormsGui.Commands;
using Assembler.FormsGui.Controls;
using Assembler.FormsGui.Controls.Custom;
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

      public HexExplorerView(MessageManager msgMgr):
         base("Hex Explorer")
      {
         m_ExplorerVm = new HexExplorerViewModel(msgMgr);
         m_OpenFileCmd = new RelayCommand((param) => LoadFileAction());
         m_SaveFileAsCmd = new RelayCommand((param) => SaveFileAsAction());
         m_SaveFileCmd = new RelayCommand((param) => SaveFileAction());

         
         m_CloseTabsToRightCmd = new RelayCommand(
            (param) =>
            {
               int? iParm = param as int?;
               System.Diagnostics.Debug.Assert(iParm.HasValue);

               // if we're removing everything to the right, the AllOpenFiles
               // Count value will change. we'll just wait until that Count property
               // is one above our target tab (since that'd be the theoretic last tab).
               int targetCount = iParm.Value + 1;
               while (m_ExplorerVm.AllOpenFiles.Count > targetCount)
               {
                  CloseTabAction(iParm.Value + 1);
               }
            });

         m_CloseTabsToLeftCmd = new RelayCommand(
            (param) =>
            {
               int? iParm = param as int?;
               System.Diagnostics.Debug.Assert(iParm.HasValue);
               for (int i = iParm.Value - 1; i >= 0; --i)
               {
                  CloseTabAction(i);
               }

               // set the active tab index to 0. the tab changed
               // event doesn't seem to be getting called. it hasn't
               // crashed yet, but that seems like it'd be in a bad state.
               m_ExplorerVm.ChangeActiveIndexCommand.Execute(0);
            });

         // this will be passed either a tab index or a view model.
         // need to differentiate on the fly when we're called.
         m_CloseTabCmd = new RelayCommand(
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
               },
               (param) =>
               {
                  bool canExecute = false;
                  int? iParm = param as int?;
                  if (iParm.HasValue)
                  {
                     if (iParm.Value < m_ExplorerVm.AllOpenFiles.Count)
                     {
                        canExecute = true;
                     }
                  }
                  else
                  {
                     var evm = param as HexExplorerViewModel;
                     System.Diagnostics.Debug.Assert(evm != null);
                     if (evm.ActiveFileIndex < m_ExplorerVm.AllOpenFiles.Count)
                     {
                        canExecute = true;
                     }
                  }
                  return canExecute;
               });
         m_CloseWindowCmd = new RelayCommand((param) => CloseWindow());
         InitializeComponent();
         m_Ctx = CreateMenuBarContext();
         CreateDataBindings(m_ExplorerVm);
      }

      public override IBasicQueue<IBasicMessage> MessageQueue
      {
         get { return m_ExplorerVm.MessageQueue; }
      }

      private TabPage CreateNewTabPage(CompiledFileViewModel viewModel)
      {
         var newTab = new TabPage();
         newTab.DataBindings.Add(new Binding(nameof(newTab.Text), viewModel, nameof(viewModel.FileName)));
         var tabContent = new HexValueGrid(viewModel);
         tabContent.Dock = DockStyle.Fill;
         newTab.Controls.Add(tabContent);

         return newTab;
      }

      private void CreateDataBindings(HexExplorerViewModel viewModel)
      {
         m_FileTabCtrl.TabPages.BindToObservableCollection(viewModel.AllOpenFiles,
                                                           (avm) => CreateNewTabPage(avm));

         m_FileTabCtrl.DataBindings.Add(new Binding(nameof(m_FileTabCtrl.SelectedIndex), viewModel, nameof(viewModel.ActiveFileIndex), 
            true, DataSourceUpdateMode.OnPropertyChanged));
      }


      public override MenuBarContext MenuBarMembers
      {
         get { return m_Ctx; }
      }

      private MenuBarContext CreateMenuBarContext()
      {
         var ctx = new MenuBarContext();
         
         var fileElementList = new List<BaseMenuBarElement>
         {
            new MenuBarActionElement("Open File", m_OpenFileCmd, Keys.Control | Keys.O),
            new MenuBarActionElement("Save", m_SaveFileCmd, Keys.Control | Keys.S),
            new MenuBarActionElement("Save As", m_SaveFileAsCmd),
            new SeparatorMenuBarElement(),

            // need to pass the whole view model here, so that way the latest ActiveTabIndex will be used
            // (instead of a copy). differentiate the types in the RelayCommand implementation.
            new MenuBarActionElement("Close File", m_CloseTabCmd, m_ExplorerVm, m_ExplorerVm),
            new SeparatorMenuBarElement(),
            new MenuBarActionElement("Exit", m_CloseWindowCmd, Keys.Alt | Keys.F4)
         };

         var fileMenuButton = new CompositeMenuBarElement("File", fileElementList);
         ctx.AddMenuBarElement(fileMenuButton);
         return ctx;
      }



      private void LoadFileAction()
      {
         IDialogService service = DialogServiceFactory.GetServiceInstance();
         try
         {
            var options = new DialogOptions()
            {
               FileFilter = "JEF Compiled File (*.jef)|*.jef",
               WindowTitle = "Open File"
            };

            bool okToContinue = service.ShowOpenFileDialog(options, out string filePath);

            if (okToContinue)
            {
               m_ExplorerVm.LoadFileCommand.Execute(filePath);
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

      private void SaveFileAsAction()
      {
         IDialogService service = DialogServiceFactory.GetServiceInstance();
         try
         {
            string defaultFileName = m_ExplorerVm.ActiveFile.FileName;
            if (string.IsNullOrEmpty(defaultFileName))
            {
               defaultFileName = "Untitled.jef";
            }
            var options = new DialogOptions()
            {
               DefaultFileName = defaultFileName,
               FileFilter = "JEF Compiled File (*.jef)|*.jef",
               WindowTitle = "Save File"
            };

            bool okToContinue = service.ShowSaveFileDialog(options, out string filePath);

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

      private void CloseWindow()
      {
         Application.Exit();
      }

      private readonly MenuBarContext m_Ctx;
      private readonly HexExplorerViewModel m_ExplorerVm;

      private readonly RelayCommand m_OpenFileCmd;
      private readonly RelayCommand m_SaveFileCmd;
      private readonly RelayCommand m_SaveFileAsCmd;

      private readonly RelayCommand m_CloseTabCmd;
      private readonly RelayCommand m_CloseTabsToLeftCmd;
      private readonly RelayCommand m_CloseTabsToRightCmd;

      private readonly RelayCommand m_CloseWindowCmd;
   }
}
