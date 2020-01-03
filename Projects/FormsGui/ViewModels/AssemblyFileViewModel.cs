using Assembler.FormsGui.Commands;
using Assembler.FormsGui.DataModels;
using Assembler.FormsGui.IO;
using Assembler.FormsGui.Utility;
using ICSharpCode.TextEditor.Document;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.ViewModels
{
   public class AssemblyFileViewModel : BaseViewModel
   {
      public AssemblyFileViewModel(IDocument document)
      {
         m_AreAnyChangesUnsaved = false;
         m_UnderlyingFile = new AssemblyFile();
         m_Document = document;
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
               OnPropertyChanged(nameof(FileName));

            }
         }
      }

      public bool IsFileBackedPhysically
      {
         get { return File.Exists(m_UnderlyingFile.FilePath); }
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

            if (m_AreAnyChangesUnsaved)
            {
               ret += "*";
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
      }

      private bool m_AreAnyChangesUnsaved;

      private readonly AssemblyFile m_UnderlyingFile;
      private readonly IDocument m_Document;
   }
}
