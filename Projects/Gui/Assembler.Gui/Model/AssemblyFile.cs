using System.IO;

namespace Assembler.Gui.Model
{
    class AssemblyFile
    {
        /// <summary>
        /// Opens a file and reads in all text. If a file does not exist,
        /// this will create the file (provided the path has the appropriate permissions).
        /// </summary>
        /// <param name="filePath">The file to open or create.</param>
        /// <returns>An AssemblyFile instance with the read-in data, if any.</returns>
        public static AssemblyFile OpenOrCreateFile(string filePath)
        {
            try
            {
                using (var reader = new StreamReader(File.Open(filePath, FileMode.OpenOrCreate)))
                {
                    string text = reader.ReadToEnd();
                    return new AssemblyFile(filePath, text);
                }
            }
            catch (IOException)
            {
                throw;
            }
        }

        /// <summary>
        /// Common method for saving the assembly file as the same file name.
        /// </summary>
        public void SaveFile()
        {
            SaveFileAs(m_FileName);
        }

        /// <summary>
        /// Saves the file to a chosen file path.
        /// </summary>
        /// <param name="filePath">The file path to save the contents of the file to.</param>
        public void SaveFileAs(string filePath)
        {
            try
            {
                using (var writer = new StreamWriter(File.Open(filePath, FileMode.Create)))
                {
                    writer.Write(m_FileText);
                }
            }
            catch (IOException)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the name of the assembly file.
        /// </summary>
        public string FileName
        {
            get { return m_FileName; }
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

        /// <summary>
        /// Creates a new instance of the AssemblyFile. This is private,
        /// since a file can only be created via opening it from the file system.
        /// </summary>
        /// <param name="filePath">The file name.</param>
        /// <param name="text">The read-in text of the file.</param>
        private AssemblyFile(string filePath, string text)
        {
            m_FileName = filePath;
            m_FileText = text;
        }

        private readonly string m_FileName;
        private string m_FileText;
    }
}
