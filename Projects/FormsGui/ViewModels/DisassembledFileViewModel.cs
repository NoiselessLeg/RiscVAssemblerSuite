using Assembler.Disassembler;
using Assembler.OutputProcessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.ViewModels
{
   public class DisassembledFileViewModel : BaseViewModel
   {
      public DisassembledFileViewModel(string fileName,
                                       DisassembledFileBase runtimeFile)
      {
         m_UnderlyingFile = runtimeFile;
         m_Instructions = new BindingList<ProgramInstructionViewModel>();
         m_FilePath = fileName;
         IEnumerable<InstructionData> programInstructions = 
            DisassemblerServices.GenerateInstructionData(runtimeFile.SymbolTable, runtimeFile.TextSegment, runtimeFile.SourceInformation);
         foreach (InstructionData instructionElem in programInstructions)
         {
            m_Instructions.Add(new ProgramInstructionViewModel(instructionElem));
         }
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

      public DisassembledFileBase FileData
      {
         get { return m_UnderlyingFile; }
      }

      public BindingList<ProgramInstructionViewModel> InstructionList
      {
         get { return m_Instructions; }
      }

      private string m_FilePath;
      private readonly OutputProcessing.DisassembledFileBase m_UnderlyingFile;
      private readonly BindingList<ProgramInstructionViewModel> m_Instructions;
   }
}
