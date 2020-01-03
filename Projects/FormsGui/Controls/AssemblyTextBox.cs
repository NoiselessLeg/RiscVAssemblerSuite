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
using Assembler.FormsGui.Common;

namespace Assembler.FormsGui.Controls
{
   public partial class AssemblyTextBox : UserControl, IFileBindable
   {
      public AssemblyTextBox()
      {
         InitializeComponent();
         m_FileViewModel = new AssemblyFileViewModel(m_FileTxtBox.Document);
      }

      public AssemblyTextBox(PreferencesViewModel preferences) :
         this()
      {
         preferencesViewModelBindingSource.DataSource = preferences;
         assemblyFileViewModelBindingSource.DataSource = m_FileViewModel;
      }

      public AssemblyFileViewModel ViewModel
      {
         get { return m_FileViewModel; }
      }

      public string FileName
      {
         get { return m_FileViewModel.FileName; }
      }

      private readonly AssemblyFileViewModel m_FileViewModel;
   }
}
