using Assembler.Common;
using Assembler.FormsGui.Commands;
using Assembler.FormsGui.DataModels;
using Assembler.FormsGui.IO;
using Assembler.FormsGui.Messaging;
using Assembler.FormsGui.Utility;
using ICSharpCode.TextEditor.Document;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.ViewModels
{
   public class AssemblyFileViewModel : BaseViewModel
   {
      public AssemblyFileViewModel()
      {
         m_AreAnyChangesUnsaved = false;
         m_UnderlyingFile = new AssemblyFile();
         m_LoggerVm = new LoggerViewModel();
         m_FileErrors = new ObservableCollection<AssemblyException>();
      }

      public AssemblyFileViewModel(AssemblyFile file)
      {
         m_AreAnyChangesUnsaved = false;  
         m_UnderlyingFile = file;
         m_LoggerVm = new LoggerViewModel();
         m_FileErrors = new ObservableCollection<AssemblyException>();
      }

      public AssemblyFileViewModel(string compiledFileName, DisassemblyManager disassembler)
      {
         m_AreAnyChangesUnsaved = false;
         m_LoggerVm = new LoggerViewModel();
         m_FileErrors = new ObservableCollection<AssemblyException>();
         m_UnderlyingFile = disassembler.DiassembleCompiledFile(compiledFileName, m_LoggerVm.Logger);
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

      public bool AssembleFile(RiscVAssembler assembler)
      {
         Logger.ClearLogCommand.Execute(null);
         // get the file name with no extension, in case we want intermediate files,
         // or for our output.
         string outputFilePath = AssembledFilePath;
         var options = new AssemblerOptions(new[] { FilePath }, new[] { outputFilePath });

         // clear any errors beforehand.
         FileErrors.Clear();
         AssemblerResult result = assembler.AssembleFile(FilePath, outputFilePath, Logger.Logger, options);
         if (!result.OperationSuccessful)
         {
            foreach (AssemblyException ex in result.UserErrors)
            {
               FileErrors.Add(ex);
            }
         }

         return result.OperationSuccessful;
      }

      public string AssembledFilePath
      {
         get
         {
            string fileNameNoExtension = FilePath;
            if (fileNameNoExtension.Contains("."))
            {
               fileNameNoExtension = fileNameNoExtension.Substring(0, fileNameNoExtension.LastIndexOf('.'));
            }

            //TODO: this will def need to change if we implement more filetypes.
            string outputFile = fileNameNoExtension + ".jef";
            return outputFile;
         }
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

      public int CurrentFileOffset
      {
         get { return m_CurrFileOffset; }
         set
         {
            if (m_CurrFileOffset != value)
            {
               m_CurrFileOffset = value;
               OnPropertyChanged();
            }
         }
      }

      public LoggerViewModel Logger
      {
         get { return m_LoggerVm; }
      }

      public AssemblyFile UnderlyingFile
      {
         get { return m_UnderlyingFile; }
      }

      public ObservableCollection<AssemblyException> FileErrors
      {
         get { return m_FileErrors; }
      }

      private bool m_AreAnyChangesUnsaved;


      private int m_CurrFileOffset;
      private readonly ObservableCollection<AssemblyException> m_FileErrors;

      private readonly LoggerViewModel m_LoggerVm;
      private readonly AssemblyFile m_UnderlyingFile;
      
   }
}
