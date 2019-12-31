using Assembler.PrototypeGui.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.PrototypeGui.ViewModels
{
    class AssemblyFileViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Creates a new, untitled assembly file.
        /// </summary>
        public AssemblyFileViewModel()
        {
            m_WrappedFile = new AssemblyFile();
            m_IsSavedSinceLastEdit = true;
        }

        /// <summary>
        /// Creates a view model of an assembly file from a pre-existing file.
        /// </summary>
        /// <param name="filePath">The path of the assembly file to open.</param>
        public AssemblyFileViewModel(string filePath)
        {
            m_WrappedFile = new AssemblyFile();
            using (var fileStream = File.OpenRead(filePath))
            {
                using (var fileReader = new StreamReader(fileStream))
                {
                    m_WrappedFile.FileText = fileReader.ReadToEnd();
                }
            }
            m_WrappedFile.FilePath = filePath;
            m_IsSavedSinceLastEdit = true;
        }

        /// <summary>
        /// Gets the file name without the prefixed path information.
        /// </summary>
        public string FileName
        {
            get
            {
                return m_WrappedFile.FileName;
            }
        }

        /// <summary>
        /// Gets or sets the full path of the file being modeled.
        /// </summary>
        public string FilePath
        {
            get
            {
                return m_WrappedFile.FilePath;
            }
            set
            {
                if (m_WrappedFile.FilePath != value)
                {
                    m_WrappedFile.FilePath = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the text of the assembly file.
        /// </summary>
        public string FileText
        {
            get
            {
                return m_WrappedFile.FileText;
            }

            set
            {
                if (m_WrappedFile.FileText != value)
                {
                    m_WrappedFile.FileText = value;
                    IsSavedSinceLastEdit = false;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool IsSavedSinceLastEdit
        {
            get { return m_IsSavedSinceLastEdit; }
            private set
            {
                if (m_IsSavedSinceLastEdit != value)
                {
                    m_IsSavedSinceLastEdit = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Saves a file with the same file name.
        /// </summary>
        public void Save()
        {
            SaveAs(m_WrappedFile.FilePath);
        }

        /// <summary>
        /// Saves an assembly file with an arbitrary file name. This will then
        /// rename the assembly file to the new file path.
        /// </summary>
        /// <param name="newFilePath">The new path of the file to output the assembly data to.</param>
        public void SaveAs(string newFilePath)
        {
            using (var fileStream = File.OpenWrite(newFilePath))
            {
                using (var fileWriter = new StreamWriter(fileStream))
                {
                    fileWriter.Write(m_WrappedFile.FileText);
                }
            }

            m_WrappedFile.FilePath = newFilePath;
            IsSavedSinceLastEdit = true;
        }

        /// <summary>
        /// Notifies a subscriber that a given property has changed.
        /// </summary>
        /// <param name="propertyName">The name of the property that triggered the callback.</param>
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        

        private readonly AssemblyFile m_WrappedFile;
        private bool m_IsSavedSinceLastEdit;
    }
}
