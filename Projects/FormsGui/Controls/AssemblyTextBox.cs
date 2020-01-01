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
                             EditorPreferencesViewModel preferences) :
         this()
      {
         var binding = new Binding(nameof(m_FileTxtBox.Text), avm, nameof(avm.FileText), true, DataSourceUpdateMode.OnPropertyChanged);
         m_FileTxtBox.DataBindings.Add(binding);

         m_FileTxtBox.DataBindings.Add(new Binding(nameof(m_FileTxtBox.ShowLineNumbers), preferences, nameof(preferences.ShowLineNumbers),
            true, DataSourceUpdateMode.OnPropertyChanged));

         m_FileTxtBox.DataBindings.Add(new Binding(nameof(m_FileTxtBox.ConvertTabsToSpaces), preferences, nameof(preferences.ReplaceTabsWithSpaces),
            true, DataSourceUpdateMode.OnPropertyChanged));

         m_FileTxtBox.DataBindings.Add(new Binding(nameof(m_FileTxtBox.TabIndent), preferences, nameof(preferences.NumSpacesToReplaceTabWith),
            true, DataSourceUpdateMode.OnPropertyChanged));


      }
   }
}
