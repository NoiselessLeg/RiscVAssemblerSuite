using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Assembler.FormsGui.ViewModels;

namespace Assembler.FormsGui.Controls
{
   public partial class FileEditor : UserControl
   {
      public FileEditor()
      {
         InitializeComponent();
      }

      public FileEditor(AssemblyFileViewModel avm,
                        PreferencesViewModel preferences) :
         this()
      {
         this.RecursiveRemove(m_FileTxt);
         m_FileTxt.Dispose();
         m_FileTxt = new AssemblyTextBox(avm, preferences)
         {
            BackColor = Color.DarkSalmon,
            Dock = DockStyle.Fill,
            Location = new Point(0, 0),
            Name = "m_FileTxt",
            Size = new Size(369, 204),
            TabIndex = 0
         };
         splitContainer1.Panel1.Controls.Add(m_FileTxt);
         m_LoggerTxtBox.DataBindings.Add(new Binding(nameof(m_LoggerTxtBox.Text), avm.Logger, nameof(avm.Logger.LogText)));
      }
   }
}
