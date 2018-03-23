using Assembler.Gui.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Assembler.Gui.Util
{
    /// <summary>
    /// A class that saves a project file
    /// </summary>
    class XmlOutputWriter : IDisposable
    {
        public XmlOutputWriter(string filePath)
        {
            m_Writer = new XmlTextWriter(File.Open(filePath, FileMode.Create), Encoding.UTF8);
            m_Writer.Formatting = Formatting.Indented;
            m_Writer.Indentation = 4;
            m_Writer.IndentChar = ' ';
        }

        public void WriteProjectFile(AssemblyProject project)
        {
            m_Writer.WriteStartDocument();
            m_Writer.WriteStartElement("version");
            m_Writer.WriteAttributeString("major", ((project.Version & 0xFF000000) >> 24).ToString());
            m_Writer.WriteAttributeString("minor", ((project.Version & 0x00FF0000) >> 16).ToString());
            m_Writer.WriteAttributeString("release", ((project.Version & 0x0000FF00) >> 8).ToString());
            m_Writer.WriteAttributeString("maintenance", (project.Version & 0x000000FF).ToString());
            m_Writer.WriteEndElement();

            m_Writer.WriteStartElement("includedFiles");

            foreach (AssemblyFile file in project.CurrentProjectFiles)
            {
                m_Writer.WriteStartElement("file");
                m_Writer.WriteAttributeString("path", file.FileName);

                m_Writer.WriteEndElement();
            }

            m_Writer.WriteEndDocument();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                m_Writer.Dispose();
            }
        }

        ~XmlOutputWriter()
        {
            Dispose(false);
        }

        private readonly XmlTextWriter m_Writer;
    }
}
