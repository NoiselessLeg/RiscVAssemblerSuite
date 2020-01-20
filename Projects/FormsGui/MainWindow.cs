using Assembler.Common;
using Assembler.FormsGui.IO;
using Assembler.FormsGui.Messaging;
using Assembler.FormsGui.Services;
using Assembler.FormsGui.Utility;
using Assembler.FormsGui.ViewModels;
using Assembler.FormsGui.Views;
using Assembler.FormsGui.Windows;
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
         m_DisplayPanel.Dispose();

         m_DisplayPanel = new DisplayPanel(m_ViewModel);
         m_LayoutPanel.Controls.Remove(m_DisplayPanel);
         m_DisplayPanel.Dock = DockStyle.Fill;
         m_DisplayPanel.Location = new Point(0, 24);
         m_DisplayPanel.Name = "displayPanel1";
         m_DisplayPanel.Size = new Size(800, 426);
         m_DisplayPanel.TabIndex = 1;
         m_LayoutPanel.Controls.Add(m_DisplayPanel, 0, 1);
      }

      private void newAssemblerFileToolStripMenuItem_Click(object sender, EventArgs e)
      {
         m_ViewModel.CreateNewFileCommand.Execute(null);
      }

      private readonly WindowViewModel m_ViewModel;

      private void openAssemblerFileToolStripMenuItem_Click(object sender, EventArgs e)
      {
         m_ViewModel.OpenFileCommand.Execute(null);
      }

      private void saveAssemblerFileToolStripMenuItem_Click(object sender, EventArgs e)
      {
         m_ViewModel.SaveFileCommand.Execute(null);
      }

      private void saveAssemblerFileAsToolStripMenuItem_Click(object sender, EventArgs e)
      {
         m_ViewModel.SaveFileAsCommand.Execute(null);
      }

      private void importToolStripMenuItem_Click(object sender, EventArgs e)
      {
         m_ViewModel.DisassembleCommand.Execute(null);
      }

      private void exitToolStripMenuItem_Click(object sender, EventArgs e)
      {
         m_ViewModel.CloseWindowCommand.Execute(null);
         Application.Exit();
      }

      private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
      {
         m_ViewModel.ShowPreferencesCommand.Execute(null);
      }

      private void assembleElfFileToolStripMenuItem_Click(object sender, EventArgs e)
      {
         m_ViewModel.AssembleFileCommand.Execute(OutputTypes.ELF);
      }

      private void fromCompiledFileToolStripMenuItem_Click(object sender, EventArgs e)
      {
         m_ViewModel.DisassembleCommand.Execute(null);
      }

      private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
      {
         using (var aboutWin = new AboutWindow())
         {
            aboutWin.ShowDialog();
         }
      }

      private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
      {
         m_ViewModel.CloseWindowCommand.Execute(null);
      }

      private void assembleJefFileToolStripMenuItem_Click(object sender, EventArgs e)
      {
         m_ViewModel.AssembleFileCommand.Execute(OutputTypes.DirectBinary);
      }
   }
}
