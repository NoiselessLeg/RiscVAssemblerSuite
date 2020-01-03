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

namespace Assembler.FormsGui.Controls
{
   public partial class FileExecutionView : UserControl
   {
      public FileExecutionView()
      {
         InitializeComponent();
      }

      public FileExecutionView(JefFileViewModel viewModel)
      {
         InitializeComponent();
         m_FileViewModel = viewModel;
         m_ExConsole = new AssemblerExecutionConsole(m_ConsoleTxt.InputStream, m_ConsoleTxt.OutputStream);
         m_ExViewModel = new ExecutionViewModel(m_ExConsole, viewModel.FileData);

         var bindingWrapper = new BindingList<RegisterViewModel>(m_ExViewModel.Registers);
         m_RegisterData.DataSource = bindingWrapper;
         executionViewModelBindingSource.DataSource = bindingWrapper;
         jefFileViewModelBindingSource.DataSource = m_FileViewModel;
      }

      private void testToolStripMenuItem_Click(object sender, EventArgs e)
      {
         m_ConsoleTxt.ClearConsole();
         m_ExViewModel.ExecuteFileUntilEndCommand.Execute(null);
      }

      private readonly ExecutionViewModel m_ExViewModel;
      private readonly JefFileViewModel m_FileViewModel;
      private readonly AssemblerExecutionConsole m_ExConsole;

      private void OnDataGridMouseClick(object sender, MouseEventArgs e)
      {
         var gridView = sender as DataGridView;
         if (e.Button == MouseButtons.Right)
         {
            var menu = new ContextMenu();
            menu.MenuItems.Add(new MenuItem("Display all values in decimal",
               (s, arg) => m_ExViewModel.ChangeRegisterValueDisplayTypeCommand.Execute(RegisterDisplayType.Decimal)));
            menu.MenuItems.Add(new MenuItem("Display all values in hexadecimal",
               (s, arg) => m_ExViewModel.ChangeRegisterValueDisplayTypeCommand.Execute(RegisterDisplayType.Hexadecimal)));

            menu.Show(gridView, new Point(e.X, e.Y));
         }
      }
   }
}
