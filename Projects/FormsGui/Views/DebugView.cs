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
      }

      public override MenuBarContext MenuBarMembers
      {
         get { return m_Ctx; }
      }

      public override IBasicQueue<IBasicMessage> MessageQueue
      {
         get { return m_ViewModel.MessageQueue; }
      }
      
      private readonly DebugWindowViewModel m_ViewModel;
      private readonly MenuBarContext m_Ctx;
   }
}
