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
         m_EditorVm = new AssemblyEditorViewModel(viewId, msgMgr, preferences);
         InitializeComponent();
         m_ViewModelBindingSource.DataSource = m_EditorVm;
         m_OpenFileTabs.TabPages.CreateListBinding(m_EditorVm.OpenTabPages);
         m_OpenFileTabs.DataBindings.Add(new Binding(nameof(m_OpenFileTabs.SelectedIndex), m_EditorVm, nameof(m_EditorVm.SelectedTabIndex)));

         m_Ctx = GenerateMenuContext();


         m_OpenFileCmd = new RelayCommand((param) => LoadFileAction());
         /*
         m_SaveFileAsCmd = new RelayCommand((param) => SaveFileAsAction());
         m_SaveFileCmd = new RelayCommand((param) => SaveFileAction());
         m_ImportFileCmd = new RelayCommand((param) => ImportFileAction());

         m_AssembleFileCmd = new RelayCommand(
            (param) =>
            {
               var evm = param as AssemblyEditorViewModel;
               System.Diagnostics.Debug.Assert(evm != null);
            }
         );


         m_CloseTabsToRightCmd = new RelayCommand(
            (param) =>
            {
               int? iParm = param as int?;
               System.Diagnostics.Debug.Assert(iParm.HasValue);
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
                     var evm = param as AssemblyEditorViewModel;
                     System.Diagnostics.Debug.Assert(evm != null);
                  }
               });
         m_CloseWindowCmd = new RelayCommand((param) => CloseWindow());

         CreateDataBindings(m_EditorVm);
         */

      }

      public override MenuBarContext MenuBarMembers => m_Ctx;

      private MenuBarContext GenerateMenuContext()
      {
         MenuBarContext ctx = new MenuBarContext();

         var importElementList = new List<BaseMenuBarElement>
         {
            new MenuBarActionElement("From Disassembly", m_ImportFileCmd)
         };

         var fileElementList = new List<BaseMenuBarElement>
         {
            new MenuBarActionElement("New File", m_EditorVm.NewFileCommand, Keys.Control | Keys.N),
            new MenuBarActionElement("Open File", m_OpenFileCmd, Keys.Control | Keys.O),
            new MenuBarActionElement("Save", m_SaveFileCmd, Keys.Control | Keys.S),
            new MenuBarActionElement("Save As", m_SaveFileAsCmd),
            new SeparatorMenuBarElement(),

            // need to pass the whole view model here, so that way the latest ActiveTabIndex will be used
            // (instead of a copy). differentiate the types in the RelayCommand implementation.
            new MenuBarActionElement("Close File", m_CloseTabCmd, m_EditorVm, m_EditorVm),
            new SeparatorMenuBarElement(),
            new CompositeMenuBarElement("Import", importElementList),
            new SeparatorMenuBarElement(),
            new MenuBarActionElement("Exit", m_CloseWindowCmd, Keys.Alt | Keys.F4)
         };

         var fileMenuButton = new CompositeMenuBarElement("File", fileElementList);
         ctx.AddMenuBarElement(fileMenuButton);

         var editElementList = new List<BaseMenuBarElement>
         {
            //new MenuBarActionElement("Undo", m_EditorVm.ActiveFile.UndoCommand, Keys.Control | Keys.Z),
            //new MenuBarActionElement("Redo", m_EditorVm.ActiveFile.RedoCommand, Keys.Control | Keys.Y),
            new MenuBarActionElement("Preferences", m_EditorVm.OpenPreferencesCommand),
         };

         var editMenuButton = new CompositeMenuBarElement("Edit", editElementList);
         ctx.AddMenuBarElement(editMenuButton);

         var asmElementList = new List<BaseMenuBarElement>
         {
            new MenuBarActionElement("Assemble Current File", m_AssembleFileCmd, m_EditorVm, m_EditorVm)
         };

         var asmMenuButton = new CompositeMenuBarElement("Assembler", asmElementList);
         ctx.AddMenuBarElement(asmMenuButton);

         return ctx;
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
            /*
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
               */
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
            /*
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
            */
         }
         catch (Exception ex)
         {
            service.ShowErrorDialog("Save Error", ex.Message);
         }
      }

      private void CloseTabAction(int tabIdx)
      {
         /*
         bool continueClosing = true;
         AssemblyFileViewModel avm = m_EditorVm.AllOpenFiles[tabIdx];

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
            m_EditorVm.CloseFileCommand.Execute(tabIdx);
         }
         */
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

      private void AssembleFileAction(AssemblyFileViewModel viewModel)
      {
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

      private void CloseWindow()
      {
         Application.Exit();
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
                  var cm = new ContextMenu();
                  cm.MenuItems.Add(new MenuItem("Close Tab", (s, arg) => { m_CloseTabCmd.Execute(tabItr); }));
                  cm.MenuItems.Add(new MenuItem("Close all tabs to right", (s, arg) => { m_CloseTabsToRightCmd.Execute(tabItr); }));
                  cm.MenuItems.Add(new MenuItem("Close all tabs to left", (s, arg) => { m_CloseTabsToLeftCmd.Execute(tabItr); }));
                  cm.Show(ctrl, e.Location);
                  break;
               }
            }
         }
      }
      
      private void OnTabAdded(object sender, ControlEventArgs e)
      {
         var tabCtrl = sender as TabControl;
         tabCtrl.SelectedTab = e.Control as TabPage;
      }

      private void TabControl_OnCurrentTabChanged(object sender, TabControlEventArgs e)
      {
      }

      private readonly MenuBarContext m_Ctx;
      private readonly AssemblyEditorViewModel m_EditorVm;
      private readonly PreferencesViewModel m_Preferences;

      private readonly RelayCommand m_OpenFileCmd;
      private readonly RelayCommand m_SaveFileCmd;
      private readonly RelayCommand m_SaveFileAsCmd;
      private readonly RelayCommand m_ImportFileCmd;

      private readonly RelayCommand m_CloseTabCmd;
      private readonly RelayCommand m_CloseTabsToLeftCmd;
      private readonly RelayCommand m_CloseTabsToRightCmd;

      private readonly RelayCommand m_CloseWindowCmd;
      private readonly RelayCommand m_AssembleFileCmd;
   }
}
