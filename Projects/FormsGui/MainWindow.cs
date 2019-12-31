using Assembler.FormsGui.Controls.Custom;
using Assembler.FormsGui.IO;
using Assembler.FormsGui.Services;
using Assembler.FormsGui.ViewModels;
using Assembler.FormsGui.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembler.FormsGui
{
   public partial class MainWindow : Form
   {
      public MainWindow()
      {
         m_ViewModel = new WindowViewModel();
         InitializeComponent();

         IBasicView viewPg = m_TabCtrl.TabPages[0].Controls[0] as IBasicView;
         UpdateMenuContext(viewPg);
      }

      private readonly WindowViewModel m_ViewModel;

      private void UpdateMenuContext(IBasicView activeView)
      {
         System.Diagnostics.Debug.Assert(activeView != null);
         MenuBarContext ctx = activeView.MenuBarMembers;
         m_MenuStrip.Items.Clear();
         m_MenuStrip.Items.AddRange(ctx.AsToolStripItems().ToArray());
      }

      private void OnTabSelection(object sender, TabControlEventArgs e)
      {
         IBasicView viewPg = e.TabPage.Controls[0] as IBasicView;
         UpdateMenuContext(viewPg);
      }
   }
}
