using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.PrototypeGui.Models
{
    /// <summary>
    /// Models an assembly file that will eventually be assembled.
    /// </summary>
    class AssemblyFile
    {
        /// <summary>
        /// Gets the file name without the prefixed path information.
        /// </summary>
        public string FileName
        {
            get
            {
                string fileName = m_FilePath;
                int pathDelimBeforeFileName = m_FilePath.LastIndexOf('\\');
                if (pathDelimBeforeFileName > 0)
                {
                    int fileNameLen = m_FilePath.Length - pathDelimBeforeFileName;
                    fileName = m_FilePath.Substring(pathDelimBeforeFileName + 1, fileNameLen);
                }

                return fileName;
            }
        }

        /// <summary>
        /// Gets or sets the full path of the file being modeled.
        /// </summary>
        public string FilePath
        {
            get { return m_FilePath; }
            set { m_FilePath = value; }
        }

        /// <summary>
        /// Gets or sets the text of the assembly file.
        /// </summary>
        public string FileText
        {
            get { return m_FileText; }
            set { m_FileText = value; }
        }

        private string m_FilePath;
        private string m_FileText;
    }
}
