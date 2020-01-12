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
   public partial class AboutWindow : Form
   {
      public AboutWindow()
      {
         InitializeComponent();
         string version = Properties.Settings.Default.Version;
         m_VersionLbl.Text = "GUI Version: " + version;
      }

      private void m_OkBtn_Click(object sender, EventArgs e)
      {
         Close();
      }
   }
}
