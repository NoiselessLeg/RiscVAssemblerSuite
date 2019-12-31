using Assembler.FormsGui.Controls.Custom;
using Assembler.FormsGui.IO;
using Assembler.FormsGui.Services;
using Assembler.FormsGui.Utility;
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
         CreateDataBindings(m_ViewModel);
      }

      private void CreateDataBindings(WindowViewModel viewModel)
      {
         m_TabCtrl.TabPages.BindToObservableCollection(viewModel.ViewList,
                                                       (param) => CreateTabPage(param as ViewBase));
      }

      private TabPage CreateTabPage(ViewBase view)
      {
         var page = new TabPage();
         page.Text = view.ViewName;
         view.Dock = DockStyle.Fill;
         page.Controls.Add(view);
         return page;
      }

      private void UpdateMenuContext(IBasicView activeView)
      {
         System.Diagnostics.Debug.Assert(activeView != null);
         MenuBarContext ctx = activeView.MenuBarMembers;
         m_MenuStrip.Items.Clear();
         m_MenuStrip.Items.AddRange(ctx.AsToolStripItems().ToArray());
      }

      private void OnTabSelection(object sender, TabControlEventArgs e)
      {
         if (e.TabPageIndex >= 0)
         {
            IBasicView viewPg = m_ViewModel.ViewList[e.TabPageIndex];
            UpdateMenuContext(viewPg);
         }
      }

      private readonly WindowViewModel m_ViewModel;
   }
}
