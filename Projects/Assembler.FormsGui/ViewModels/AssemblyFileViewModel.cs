using Assembler.FormsGui.DataModels;
using Assembler.FormsGui.IO;
using Assembler.FormsGui.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.ViewModels
{
   public class AssemblyFileViewModel : NotifyPropertyChangedBase
   {
      public AssemblyFileViewModel()
      {
         m_AreAnyChangesUnsaved = false;
         m_UnderlyingFile = new AssemblyFile();
      }

      public AssemblyFileViewModel(AssemblyFile file)
      {
         m_AreAnyChangesUnsaved = false;  
         m_UnderlyingFile = file;
      }

      public void SaveFileAs(string filePath)
      {
         FilePath = filePath;
         SaveFile();
      }

      public void SaveFile()
      {
         AssemblyFileSaver.SaveFile(m_UnderlyingFile);
         AreAnyChangedUnsaved = false;
      }

      public bool AreAnyChangedUnsaved
      {
         get { return m_AreAnyChangesUnsaved; }
         private set
         {
            if (m_AreAnyChangesUnsaved != value)
            {
               m_AreAnyChangesUnsaved = value;
               OnPropertyChanged();

            }
         }
      }

      public bool IsFileBackedPhysically
      {
         get { return !string.IsNullOrEmpty(m_UnderlyingFile.FilePath); }
      }

      public string FileName
      {
         get
         {
            string ret;
            if (string.IsNullOrEmpty(m_UnderlyingFile.FileName))
            {
               ret = "Untitled";
            }
            else
            {
               ret = m_UnderlyingFile.FileName;
            }
            return ret;
         }
      }

      public string FilePath
      {
         get { return m_UnderlyingFile.FilePath; }
         private set
         {
            if (m_UnderlyingFile.FilePath != value)
            {
               m_UnderlyingFile.FilePath = value;
               OnPropertyChanged();
               OnPropertyChanged(nameof(FileName));
            }
         }
      }

      public string FileText
      {
         get { return m_UnderlyingFile.FileText; }
         set
         {
            if (m_UnderlyingFile.FileText != value)
            {
               m_UnderlyingFile.FileText = value;
               OnPropertyChanged();
               AreAnyChangedUnsaved = true;
            }
         }
      }

      public AssemblyFile UnderlyingFile
      {
         get { return m_UnderlyingFile; }
         set
         {
            if (m_UnderlyingFile != value)
            {
               m_UnderlyingFile = value;
               OnPropertyChanged();
               OnPropertyChanged(nameof(FilePath));
               OnPropertyChanged(nameof(FileText));
            }
         }
      }

      private bool m_AreAnyChangesUnsaved;
      private AssemblyFile m_UnderlyingFile;
      
   }
}
