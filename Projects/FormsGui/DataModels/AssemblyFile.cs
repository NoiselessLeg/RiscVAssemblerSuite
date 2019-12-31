using Assembler.FormsGui.Utility;
using System.ComponentModel;
using System.IO;

namespace Assembler.FormsGui.DataModels
{
   /// <summary>
   /// Models an assembly file.
   /// </summary>
   public class AssemblyFile
   {
      public AssemblyFile()
      {
         m_FilePath = string.Empty;
         m_FileText = string.Empty;
      }

      /// <summary>
      /// Gets the file name without the prefixed path information.
      /// </summary>
      public string FileName
      {
         get
         {
            string fileName = "";
            if (!string.IsNullOrEmpty(m_FilePath))
            {
               int pathDelimBeforeFileName = m_FilePath.LastIndexOf('\\');
               if (pathDelimBeforeFileName > 0)
               {
                  int fileNameLen = m_FilePath.Length - pathDelimBeforeFileName - 1;
                  fileName = m_FilePath.Substring(pathDelimBeforeFileName + 1, fileNameLen);
               }
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
         set
         {
            if (m_FilePath != value)
            {
               m_FilePath = value;
            }

         }
      }

      /// <summary>
      /// Gets or sets the text of the assembly file.
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

      private string m_FilePath;
      private string m_FileText;
   }
}
