using Assembler.Gui.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Gui.ViewModel
{
    class FileViewModel : ObservableObject
    {
        /// <summary>
        /// Creates a default view model, with the name "Untitled.asm" and
        /// no text.
        /// </summary>
        public FileViewModel()
        {
            m_WrappedFile = new AssemblyFile();
            m_WrappedFile.FileName = "Untitled.asm";
        }

        /// <summary>
        /// Gets or sets the file name of the file currently being edited.
        /// </summary>
        public string FileName
        {
            get { return m_WrappedFile.FileName; }
            set
            {
                if (m_WrappedFile.FileName != value)
                {
                    m_WrappedFile.FileName = value;
                    RaisePropertyChangedEvent();
                }
            }
        }

        /// <summary>
        /// Gets or sets the text of the file currently being edited.
        /// </summary>
        public string Text
        {
            get { return m_WrappedFile.FileText; }
            set
            {
                if (m_WrappedFile.FileText != value)
                {
                    m_WrappedFile.FileText = value;
                    RaisePropertyChangedEvent();
                }
            }
        }

        private readonly AssemblyFile m_WrappedFile;
    }
}
