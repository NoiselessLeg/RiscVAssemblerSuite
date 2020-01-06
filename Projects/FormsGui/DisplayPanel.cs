using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Assembler.FormsGui.Views;
using Assembler.FormsGui.Messaging;
using Assembler.FormsGui.ViewModels;
using Assembler.FormsGui.Utility;

namespace Assembler.FormsGui
{
   public partial class DisplayPanel : ViewBase
   {
      public DisplayPanel():
         base(0, "TopLevel", MessageManager.GetInstance())
      {
         InitializeComponent();
      }

      public DisplayPanel(WindowViewModel vm) :
         this()
      {
         m_ViewModel = vm;
         CreateDataBindings(vm);
         SubscribeToMessageType(MessageType.ActiveViewRequest, m_ViewModel.ChangeActiveViewCommand);
         SubscribeToMessageType(MessageType.ShowOptionsRequest, m_ViewModel.ShowPreferencesCommand);
      }

      public WindowViewModel ViewModel
      {
         get { return m_ViewModel; }
      }

      private void CreateDataBindings(WindowViewModel viewModel)
      {
         int i = 0;
         foreach (var view in ViewModel.ViewList)
         {
            view.Dock = DockStyle.Fill;
            var tabPageElem = m_TabCtrl.TabPages[i];
            tabPageElem.DataBindings.Add(new Binding(nameof(TabPage.Text), view, nameof(view.ViewName)));
            tabPageElem.Controls.Add(view);
            ++i;
         }

         m_TabCtrl.DataBindings.Add(new Binding(nameof(m_TabCtrl.SelectedIndex),
            viewModel, nameof(viewModel.ActiveViewIndex), true, DataSourceUpdateMode.OnPropertyChanged));

         m_TabCtrl.Refresh();
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

      private WindowViewModel m_ViewModel;
   }
}
