﻿using Assembler.Common;
using Assembler.Disassembler;
using Assembler.FormsGui.DataModels;
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
         m_Dsm = new RiscVDisassembler();
      }

      public AssemblyFile DiassembleCompiledFile(string inputFile, ILogger loggerInput)
      {
         try
         {
            string tmpPath = Path.GetTempFileName();
            DisassemblerOptions options = new DisassemblerOptions(inputFile, tmpPath);
            m_Dsm.Disassemble(options, loggerInput);

            var asmFile = new AssemblyFile();
            using (var fileStream = new FileStream(tmpPath, FileMode.Open))
            {
               using (var reader = new StreamReader(fileStream))
               {
                  string fileTxt = reader.ReadToEnd();
                  asmFile.FileText = fileTxt;// fileTxt.Replace("\n", Environment.NewLine);
               }
            }

            File.Delete(tmpPath);
            return asmFile;
         }
         catch (Exception ex)
         {
            loggerInput.Log(LogLevel.Critical, "Failed to disassemble file: " + ex.Message);
            return new AssemblyFile();
         }
      }

      private readonly RiscVDisassembler m_Dsm;
   }
}
