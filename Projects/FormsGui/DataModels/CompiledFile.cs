using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.DataModels
{
   public class CompiledFile
   {
      public CompiledFile()
      {
         m_Bytes = new List<byte>();

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

      public IList<byte> Bytes
      {
         get { return m_Bytes; }
      }

      private string m_FilePath;
      private readonly IList<byte> m_Bytes;
   }
}
