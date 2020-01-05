using Assembler.FormsGui.Controls;
using Assembler.FormsGui.Controls.Custom;
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
         m_Ctx = new MenuBarContext();
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
         var tabContent = new FileExecutionTabPage(file);
         tabContent.Dock = DockStyle.Fill;
         newTabPage.DataBindings.Add(new Binding(nameof(newTabPage.Text), file, nameof(file.FileName)));
         newTabPage.Dock = DockStyle.Fill;
         newTabPage.Controls.Add(tabContent);
         return newTabPage;
      }

      private readonly DebugWindowViewModel m_ViewModel;
      private readonly MenuBarContext m_Ctx;
   }
}
