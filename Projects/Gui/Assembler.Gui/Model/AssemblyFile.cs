using Assembler.Gui.ViewModel;
using System.ComponentModel;
using System.IO;

namespace Assembler.Gui.Model
{
    /// <summary>
    /// Model of an assembly file.
    /// </summary>
    class AssemblyFile
    {
        /// <summary>
        /// Gets the name of the assembly file.
        /// </summary>
        public string FileName
        {
            get { return m_FileName; }
            set
            {
                if (m_FileName != value)
                {
                    m_FileName = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the current text in the assembly file.
        /// </summary>
        public string FileText
        {
            get { return m_FileText; }
            set
            {
                if (m_FileText != value)
                {
                    m_FileText = value;
                }
            }
        }

        private string m_FileName;
        private string m_FileText;
    }
}
