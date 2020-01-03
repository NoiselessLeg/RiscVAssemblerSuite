using Assembler.FormsGui.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembler.FormsGui.Windows
{
   public partial class PreferencesWindow : Form
   {
      public PreferencesWindow()
      {
         InitializeComponent();
         m_SystemPreferences = new PreferencesViewModel();
      }

      public PreferencesWindow(PreferencesViewModel systemPreferences)
      {
         InitializeComponent();

         m_SystemPreferences = new PreferencesViewModel();
         m_SystemPreferences.CloneValues(systemPreferences);
         preferencesViewModelBindingSource.DataSource = m_SystemPreferences;
      }

      public PreferencesViewModel ActivePreferences
      {
         get { return m_SystemPreferences; }
      }

      private void m_UseSpacesChkBox_CheckedChanged(object sender, EventArgs e)
      {
         var chkBox = sender as CheckBox;
         m_SystemPreferences.ReplaceTabsWithSpaces = chkBox.Checked;
      }

      private void m_NumSpacesTxtBox_Validating(object sender, CancelEventArgs e)
      {
         var txtBox = sender as TextBox;
         if (!int.TryParse(txtBox.Text, out int dummy))
         {
            e.Cancel = true;
            m_ErrorProvider.SetError(txtBox, "Value is not an integer.");
         }
         else
         {
            if (dummy > 0)
            {
               m_ErrorProvider.SetError(txtBox, string.Empty);
            }
            else
            {
               e.Cancel = true;
               m_ErrorProvider.SetError(txtBox, "Value must be greater than zero.");
            }
         }
      }

      private readonly PreferencesViewModel m_SystemPreferences;
   }
}
