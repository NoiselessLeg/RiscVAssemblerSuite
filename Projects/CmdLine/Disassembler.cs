using Assembler.Common;
using Assembler.OutputProcessing;
using Assembler.OutputProcessing.FileReaders;
using Assembler.OutputProcessing.TextOutput;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Assembler
{
   /// <summary>
   /// Disassembles a compiled file into an approximation of its assembly source
   /// representation
   /// </summary>
   public class Disassembler
   {
      /// <summary>
      /// Creates a new Disassembler instance.
      /// </summary>
      public Disassembler()
      {
         m_FileParserFac = new FileReaderFactory();
      }

      /// <summary>
      /// Disassembles a .JEF file into instructions and data.
      /// </summary>
      /// <param name="options">The options used to disassemble the file</param>
      /// <param name="logger">A logging implementation to log errors to.</param>
      public void Disassemble(DisassemblerOptions options, ILogger logger)
      {
         var stopwatch = new Stopwatch();

         var tasks = new List<Task<bool>>();
         stopwatch.Start();
         DisassembleFile(logger, options);
         stopwatch.Stop();

         if (tasks.Any(t => !t.Result))
         {
            logger.Log(LogLevel.Info, "Disassembly completed (with errors) in " + stopwatch.Elapsed.ToString());
         }
         else
         {
            logger.Log(LogLevel.Info, "Disassembly completed in " + stopwatch.Elapsed.ToString());
         }

      }

      /// <summary>
      /// Task for disassembling one individual file.
      /// </summary>
      /// <param name="logger">The logging implementation to log errors/info to.</param>
      /// <param name="options">The options to use while disassembling.</param>
      /// <returns>True if the disassembler could successfully disassemble the file; otherwise returns false.</returns>
      public bool DisassembleFile(ILogger logger, DisassemblerOptions options)
      {
         bool success = true;
         logger.Log(LogLevel.Info, "Invoking disassembler for file " + options.InputFileName);
         try
         {
            ICompiledFileReader fileParser = m_FileParserFac.GetFileParser(options.InputFileName);
            DisassembledFileBase fileBase = fileParser.ParseFile(options.InputFileName, logger);
            IAssemblyFileWriter fileWriter = fileBase.AssemblyTextFileWriter;
            fileWriter.GenerateOutputFile(options.OutputFileName);
         }
         catch (IOException ex)
         {
            logger.Log(LogLevel.Critical, ex.Message);
            success = false;
         }
         catch (Exception ex)
         {
            logger.Log(LogLevel.Critical, "In file " + options.InputFileName + ":");
            logger.Log(LogLevel.Critical, ex.Message);
            success = false;
         }

         return success;
      }

      private readonly FileReaderFactory m_FileParserFac;
   }
}
