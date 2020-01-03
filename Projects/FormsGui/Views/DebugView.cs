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
         base(viewId, "Program Execution", msgMgr)
      {
         m_ViewModel = new DebugWindowViewModel(viewId, msgMgr);
         InitializeComponent();
         m_Ctx = new MenuBarContext();
         CreateDataBindings(m_ViewModel);
      }

      public override MenuBarContext MenuBarMembers
      {
         get { return m_Ctx; }
      }

      public override IBasicQueue<IBasicMessage> MessageQueue
      {
         get { return m_ViewModel.MessageQueue; }
      }

      private void CreateDataBindings(DebugWindowViewModel vm)
      {
         m_OpenFilesTabCtrl.TabPages.BindToObservableCollection(vm.FilesToExecute,
            (param) => CreateTabPage(param as JefFileViewModel));
      }

      private TabPage CreateTabPage(JefFileViewModel file)
      {
         var newTabPage = new TabPage();
         var tabContent = new FileExecutionView(file);
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
