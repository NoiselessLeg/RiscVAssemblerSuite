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


            // binary coded integer,
            // -- LSB is maintenance version
            // -- next LSB is release
            // -- next LSB is minor version
            // -- MSB is build
            const int NEW_VERSION = 0x01000000;
            m_Version = NEW_VERSION;
        }

        //public void AddNewFile(string filePath)
        //{
        //    AssemblyFile file = AssemblyFile.OpenOrCreateFile(filePath);
        //    m_AssemblyFiles.Add(file);
        //}

        //public void SaveAllFiles()
        //{
        //    foreach (AssemblyFile file in m_AssemblyFiles)
        //    {
        //        file.SaveFile();
        //    }
        //}

        /// <summary>
        /// Returns the version of this project.
        /// </summary>
        public int Version
        {
            get { return m_Version; }
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

        private readonly int m_Version;
        private readonly ObservableCollection<AssemblyFile> m_AssemblyFiles;
    }
}
