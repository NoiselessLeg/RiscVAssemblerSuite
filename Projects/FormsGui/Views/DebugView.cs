using Assembler.FormsGui.Controls;
using Assembler.FormsGui.Messaging;
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
   public partial class DebugView : ViewBase
   {
      public DebugView()
      {
         // designer requires this - do not call from user code.
         InitializeComponent();
      }

      public DebugView(int viewId, MessageManager msgMgr) :
         base(viewId, "Execution View", msgMgr)
      {
         m_ViewModel = new DebugWindowViewModel(viewId, msgMgr);
         InitializeComponent();
         CreateDataBindings(m_ViewModel);

         SubscribeToMessageType(MessageType.FileAssembled, m_ViewModel.HandleFileAssembledCommand);
      }

      private void CreateDataBindings(DebugWindowViewModel vm)
      {
         m_OpenFilesTabCtrl.TabPages.BindToObservableCollection(vm.FilesToExecute,
            (param) => CreateTabPage(param as JefFileViewModel));

         m_OpenFilesTabCtrl.DataBindings.Add(
            new Binding(nameof(m_OpenFilesTabCtrl.SelectedIndex), vm, nameof(vm.ActiveTabIdx),
            true, DataSourceUpdateMode.OnPropertyChanged));
      }

      private TabPage CreateTabPage(JefFileViewModel file)
      {
         var newTabPage = new TabPage();
         var tabContent = new FileExecutionTabPage(file)
         {
            Dock = DockStyle.Fill
         };
         newTabPage.DataBindings.Add(new Binding(nameof(newTabPage.Text), file, nameof(file.FileName)));
         newTabPage.Dock = DockStyle.Fill;
         newTabPage.Controls.Add(tabContent);
         AreAnyFilesOpened = m_ViewModel.FilesToExecute.Any();
         if (AreAnyFilesOpened)
         {
            m_NoFileAssembledLbl.Visible = false;
            m_OpenFilesTabCtrl.Visible = true;
         }

         return newTabPage;
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
         while (targetTabIdx < m_OpenFilesTabCtrl.TabCount)
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
         int numTabsToClose = m_OpenFilesTabCtrl.TabCount;
         for (int closeCount = 0; closeCount < numTabsToClose; ++closeCount)
         {
            CloseTab(0);
         }
      }

      private void CloseTab(int index)
      {
         m_ViewModel.FilesToExecute.RemoveAt(index);
         AreAnyFilesOpened = m_ViewModel.FilesToExecute.Any();
         if (!AreAnyFilesOpened)
         {
            m_NoFileAssembledLbl.Visible = true;
            m_OpenFilesTabCtrl.Visible = false;
         }
      }

      private readonly DebugWindowViewModel m_ViewModel;

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
   }
}
