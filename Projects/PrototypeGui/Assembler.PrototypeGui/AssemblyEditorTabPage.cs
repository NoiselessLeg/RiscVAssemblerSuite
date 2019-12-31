using Assembler.PrototypeGui.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assembler.PrototypeGui
{
    public partial class AssemblyEditorTabPage : TabPage
    {
        private readonly AssemblyFileViewModel m_FileViewModel;

        public AssemblyEditorTabPage()
        {
            InitializeComponent();
            m_FileViewModel = new AssemblyFileViewModel();
            m_FileViewModel.PropertyChanged += FileViewModel_PropertyChanged;
            Text = "Untitled";
        }

        public AssemblyEditorTabPage(string filePath)
        {
            InitializeComponent();
            m_FileViewModel = new AssemblyFileViewModel(filePath);
            m_FileViewModel.PropertyChanged += FileViewModel_PropertyChanged;
            Text = m_FileViewModel.FileName;
        }

        public string FilePath
        {
            get { return m_FileViewModel.FilePath; }
        }

        public string FileName
        {
            get { return m_FileViewModel.FileName; }
        }

        public bool IsSavedSinceLastEdit
        {
            get { return m_FileViewModel.IsSavedSinceLastEdit; }
        }

        public void SaveFileContents()
        {
            m_FileViewModel.Save();
        }

        public void SaveFileContentsAs(string fileName)
        {
            m_FileViewModel.SaveAs(fileName);
        }

        private void FileViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(m_FileViewModel.FilePath))
            {
                if (!string.IsNullOrEmpty(m_FileViewModel.FileName))
                {
                    Text = m_FileViewModel.FileName;
                }
                else
                {
                    Text = "Untitled";
                }
            }

            else if (e.PropertyName == nameof(m_FileViewModel.IsSavedSinceLastEdit))
            {
                if (m_FileViewModel.IsSavedSinceLastEdit)
                {
                    Text = m_FileViewModel.FileName;
                }
                else
                {
                    Text = m_FileViewModel.FileName + "*";
                }
            }
        }

        private void m_AsmBox_TextChanged(object sender, EventArgs e)
        {
            var asmBox = sender as TextBox;
            m_FileViewModel.FileText = asmBox.Text;
        }
    }
}
