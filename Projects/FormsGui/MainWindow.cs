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

         m_MenuStrip.CreateBinding(viewModel, ctrl => ctrl.Items, nameof(m_ViewModel.ActiveView),
            (ctrlItems, vm) =>
            {
               IBasicView activeView = vm.ActiveView;
               ctrlItems.Clear();
               ctrlItems.AddRange(activeView.MenuBarMembers.AsToolStripItems().ToArray());
            }
         );

         m_TabCtrl.DataBindings.Add(new Binding(nameof(m_TabCtrl.SelectedIndex), 
            viewModel, nameof(viewModel.ActiveViewIndex), true, DataSourceUpdateMode.OnPropertyChanged));
      }

      private TabPage CreateTabPage(ViewBase view)
      {
         var page = new TabPage();
         page.Text = view.ViewName;
         view.Dock = DockStyle.Fill;
         page.Controls.Add(view);
         return page;
      }

      private void OnTabSelection(object sender, TabControlEventArgs e)
      {
         if (e.TabPageIndex >= 0)
         {
            m_ViewModel.ChangeActiveViewCommand.Execute(e.TabPageIndex);
         }
      }

      private readonly WindowViewModel m_ViewModel;
   }
}
