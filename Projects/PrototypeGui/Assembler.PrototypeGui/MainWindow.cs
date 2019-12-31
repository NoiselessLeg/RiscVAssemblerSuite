using Assembler;
using Assembler.Common;
using Assembler.PrototypeGui.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembler.PrototypeGui
{
    public partial class MainWindow : Form
    {
        private readonly RiscVAssembler m_Assembler;

        public MainWindow()
        {
            InitializeComponent();
            m_Assembler = new RiscVAssembler();
            m_TabCtrl.TabPages.Add(new AssemblyEditorTabPage());
        }

        private void assembleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // get the active tab, and assemble the data.
            var activeTab = m_TabCtrl.SelectedTab as AssemblyEditorTabPage;
            bool continueAssembling = true;

            if (!activeTab.IsSavedSinceLastEdit)
            {
                DialogResult promptResult = MessageBox.Show(this,
                                                            "The file \"" + activeTab.FileName + "\" has not been saved since the last edit. Would you like to save your changes?",
                                                            "Save Changes?",
                                                            MessageBoxButtons.YesNoCancel);

                switch (promptResult)
                {
                    case DialogResult.Yes:
                    {
                        SaveCurrentFile(activeTab.FilePath);
                        break;
                    }

                    case DialogResult.Cancel:
                    {
                        continueAssembling = false;
                        break;
                    }
                }
            }

            if (continueAssembling)
            {
                var fileList = new List<string>();
                fileList.Add(activeTab.FilePath);
                var options = new AssemblerOptions(fileList);
                m_Assembler.Assemble(options, m_LogConsole.Logger);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PresentSaveFileDialogBox()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            DialogResult dr = dialog.ShowDialog();
            switch (dr)
            {
                case DialogResult.OK:
                {
                    SaveCurrentFile(dialog.FileName);
                    break;
                }
            }
        }

        private void SaveCurrentFile(string filePath)
        {
            var activeTab = m_TabCtrl.SelectedTab as AssemblyEditorTabPage;
            activeTab.SaveFileContentsAs(filePath);
        }
    }
}
