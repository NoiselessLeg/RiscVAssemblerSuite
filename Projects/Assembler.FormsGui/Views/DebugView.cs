using Assembler.FormsGui.Controls.Custom;
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
   public partial class DebugView : UserControl, IBasicView
   {
      public DebugView()
      {
         InitializeComponent();
         m_Ctx = new MenuBarContext();

      }

      public MenuBarContext MenuBarMembers
      {
         get { return m_Ctx; }
      }

      private readonly MenuBarContext m_Ctx;
   }
}
