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
   public partial class AssemblyTextBox : UserControl
   {
      public AssemblyTextBox()
      {
         InitializeComponent();
      }

      public AssemblyTextBox(AssemblyFileViewModel avm,
                             PreferencesViewModel preferences) :
         this()
      {
         preferencesViewModelBindingSource.DataSource = preferences;
         assemblyFileViewModelBindingSource.DataSource = avm;
      }

      private void OnTextChanged(object sender, EventArgs e)
      {
      }
   }
}
