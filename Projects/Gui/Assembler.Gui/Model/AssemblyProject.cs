using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Gui.Model
{
    class AssemblyProject
    {
        public AssemblyProject()
        {
            m_AssemblyFiles = new ObservableCollection<AssemblyFile>();
        }

        public void AddNewFile(string filePath)
        {
            AssemblyFile file = AssemblyFile.OpenOrCreateFile(filePath);
            m_AssemblyFiles.Add(file);
        }

        public void SaveAllFiles()
        {
            foreach (AssemblyFile file in m_AssemblyFiles)
            {
                file.SaveFile();
            }
        }
    
        /// <summary>
        /// Fetches an IEnumerable of all file paths to files in this project.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetFileNames()
        {
            foreach (var file in m_AssemblyFiles)
            {
                yield return file.FileName;
            }
        }

        /// <summary>
        /// Returns an ObservableCollection of all member files of the current project.
        /// </summary>
        public ObservableCollection<AssemblyFile> CurrentProjectFiles
        {
            get { return m_AssemblyFiles; }
        }

        private readonly ObservableCollection<AssemblyFile> m_AssemblyFiles;
    }
}
