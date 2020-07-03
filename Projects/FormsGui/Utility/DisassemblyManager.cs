using Assembler.Common;
using Assembler.FormsGui.DataModels;
using Assembler.OutputProcessing;
using Assembler.OutputProcessing.FileReaders;
using Assembler.OutputProcessing.TextOutput;
using Assembler.UICommon.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.Utility
{
   public class DisassemblyManager
   {
      public DisassemblyManager()
      {
         m_FileParserFac = new FileReaderFactory();
      }

      public AssemblyFile DiassembleCompiledFile(string inputFile, ILogger loggerInput)
      {
         AssemblyFile asmFile = new AssemblyFile();
         try
         {
            string tmpPath = DisassembleToTemporaryFile(inputFile, loggerInput);
            
            using (var fileStream = new FileStream(tmpPath, FileMode.Open))
            {
               using (var reader = new StreamReader(fileStream))
               {
                  string fileTxt = reader.ReadToEnd();
                  asmFile.FileText = fileTxt;// fileTxt.Replace("\n", Environment.NewLine);
               }
            }

            File.Delete(tmpPath);
         }
         catch (Exception ex)
         {
            loggerInput.Log(LogLevel.Critical, "Failed to disassemble file: " + ex.Message);
         }

         return asmFile;
      }

      private string DisassembleToTemporaryFile(string inputFileName, ILogger logger)
      {

         string outputFileName = Path.GetTempFileName();
         logger.Log(LogLevel.Info, "Invoking disassembler for file " + inputFileName);
         try
         {
            ICompiledFileReader fileParser = m_FileParserFac.GetFileParser(inputFileName);
            DisassembledFileBase fileBase = fileParser.ParseFile(inputFileName, logger);
            IAssemblyFileWriter fileWriter = fileBase.AssemblyTextFileWriter;
            fileWriter.GenerateOutputFile(outputFileName);
         }
         catch (IOException ex)
         {
            logger.Log(LogLevel.Critical, ex.Message);
            throw;
         }
         catch (Exception ex)
         {
            logger.Log(LogLevel.Critical, "In file " + inputFileName + ":");
            logger.Log(LogLevel.Critical, ex.Message);
            throw;
         }

         return outputFileName;
      }

      private readonly FileReaderFactory m_FileParserFac;
   }
}
