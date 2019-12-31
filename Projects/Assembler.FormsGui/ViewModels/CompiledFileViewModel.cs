using Assembler.FormsGui.DataModels;
using Assembler.FormsGui.IO;
using Assembler.FormsGui.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.ViewModels
{
   public class CompiledFileViewModel : NotifyPropertyChangedBase
   {
      public CompiledFileViewModel()
      {
         m_UnderlyingFile = new CompiledFile();
         m_AreAnyChangesUnsaved = false;
      }

      public CompiledFileViewModel(CompiledFile underlying)
      {
         m_UnderlyingFile = underlying;
         m_AreAnyChangesUnsaved = false;
      }

      public void SaveFileAs(string filePath)
      {
         FilePath = filePath;
         SaveFile();
      }

      public void SaveFile()
      {
         BinaryFileSaver.SaveFile(m_UnderlyingFile);
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

      public IList<byte> Data
      {
         get
         {
            return m_UnderlyingFile.Bytes;
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

      private bool m_AreAnyChangesUnsaved;
      private CompiledFile m_UnderlyingFile;
   }
}
