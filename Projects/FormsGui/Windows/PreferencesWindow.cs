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
         m_AllPreferences = new SystemPreferencesViewModel();
      }

      public PreferencesWindow(SystemPreferencesViewModel systemPreferences)
      {
         InitializeComponent();
         m_AllPreferences = systemPreferences;
      }

      private readonly SystemPreferencesViewModel m_AllPreferences;
   }
}
