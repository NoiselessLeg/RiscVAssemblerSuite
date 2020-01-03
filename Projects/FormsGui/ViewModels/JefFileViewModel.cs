using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.ViewModels
{
   public class JefFileViewModel : BaseViewModel
   {
      public JefFileViewModel(string fileName,
                              DataModels.AssemblyFile disassembly,
                              OutputProcessing.DisassembledFile runtimeFile)
      {
         m_FilePath = fileName;
         m_AssemblyFile = disassembly;
         m_UnderlyingFile = runtimeFile;
      }
      
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

      public string FilePath
      {
         get { return m_FilePath; }
         private set
         {
            if (m_FilePath != value)
            {
               m_FilePath = value;
               OnPropertyChanged();
               OnPropertyChanged(nameof(FileName));
            }
         }
      }

      public string DisassembledText
      {
         get { return m_AssemblyFile.FileText; }
      }

      public OutputProcessing.DisassembledFile FileData
      {
         get { return m_UnderlyingFile; }
      }

      private string m_FilePath;
      private readonly DataModels.AssemblyFile m_AssemblyFile;
      private readonly OutputProcessing.DisassembledFile m_UnderlyingFile;
   }
}
