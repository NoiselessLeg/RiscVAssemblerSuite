using Assembler.CodeGeneration;
using Assembler.Common;
using Assembler.InstructionProcessing;
using Assembler.Output;
using Assembler.Output.OutputWriters;
using Assembler.SymbolTableConstruction;
using Assembler.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Assembler
{
   /// <summary>
   /// Assembles .asm files into machine code that is ready to be linked.
   /// </summary>
   public class RiscVAssembler
   {
      /// <summary>
      /// Assembles a list of files into object files ready to be linked.
      /// </summary>
      /// <param name="inputFileNames">One or more file paths to assembly files.</param>
      /// <param name="logger">A logging implementation to log errors to.</param>
      public bool Assemble(AssemblerOptions options, ILogger logger)
      {
         if (options.InputFileNames.Count() != options.OutputFileNames.Count())
         {
            logger.Log(LogLevel.Critical, "Input file names do not match output file names.");
            return false;
         }

         var stopwatch = new Stopwatch();

         var tasks = new List<Task<bool>>();
         stopwatch.Start();
         for (int idx = 0; idx < options.InputFileNames.Count(); ++idx)
         {
            string inputFileName = options.InputFileNames.ElementAt(idx);
            string outputFileName = options.OutputFileNames.ElementAt(idx);
            var task = new Task<bool>(() => AssembleFile(inputFileName, outputFileName, logger, options));
            tasks.Add(task);
            task.Start();
         }

         // wait for all of our assembler tasks to join.
         Task.WaitAll(tasks.ToArray());
         stopwatch.Stop();

         bool ret = false;
         if (tasks.Any(t => !t.Result))
         {
            logger.Log(LogLevel.Info, "Build completed (with errors) in " + stopwatch.Elapsed.ToString());
         }
         else
         {
            ret = true;
            logger.Log(LogLevel.Info, "Build completed in " + stopwatch.Elapsed.ToString());
         }

         return ret;
      }

      /// <summary>
      /// Task for assembling one individual file.
      /// </summary>
      /// <param name="inputFile">The input file to assemble.</param>
      /// <param name="logger">The logging implementation to log errors/info to.</param>
      /// <param name="options">The options to use while assembling.</param>
      /// <returns>True if the assembler could successfully generate code for the file; otherwise returns false.</returns>
      public bool AssembleFile(string inputFile, string outputFile, ILogger logger, AssemblerOptions options)
      {
         bool success = true;
         logger.Log(LogLevel.Info, "Invoking assembler for file " + inputFile);
         try
         {
            bool furtherProcessingNeeded = true;
            if (File.Exists(inputFile) &&
                File.Exists(outputFile))
            {
               DateTime inputFileWriteTime = File.GetLastWriteTimeUtc(inputFile);
               DateTime outputFileWriteTime = File.GetLastWriteTimeUtc(outputFile);
               if (outputFileWriteTime > inputFileWriteTime)
               {
                  logger.Log(LogLevel.Info, "Nothing to do for file " + inputFile);
                  furtherProcessingNeeded = false;
               }
            }

            if (furtherProcessingNeeded)
            {
               using (var reader = new StreamReader(File.OpenRead(inputFile)))
               {
                  var symTable = new SymbolTable();

                  // build the symbol table
                  var instructionProcFac = new InstructionProcessorFactory(symTable);
                  var symTableBuilder = new SymbolTableBuilder(logger, instructionProcFac);
                  symTableBuilder.GenerateSymbolTableForSegment(reader, SegmentType.Data, symTable);
                  symTableBuilder.GenerateSymbolTableForSegment(reader, SegmentType.Text, symTable);

                  // use the symbol table to generate code with references resolved.
                  var objFile = new BasicObjectFile(symTable);

                  var codeGenerator = new CodeGenerator(logger, symTable, instructionProcFac);
                  codeGenerator.GenerateCode(reader, objFile);

                  // write the object file out.
                  var writerFac = new ObjectFileWriterFactory();
                  IObjectFileWriter writer = writerFac.GetWriterForObjectType(OutputTypes.DirectBinary);
                  writer.WriteObjectFile(outputFile, objFile);
               }
            }
         }
         catch (AggregateException ex)
         {
            foreach (Exception e in ex.InnerExceptions)
            {
               logger.Log(LogLevel.Critical, "In file " + inputFile + ":");
               logger.Log(LogLevel.Critical, e.Message);
            }
            success = false;
         }
         catch (IOException ex)
         {
            logger.Log(LogLevel.Critical, ex.Message);
            if (ex.InnerException != null)
            {
               logger.Log(LogLevel.Critical, ex.InnerException.Message);
            }
            success = false;
         }
         catch (Exception ex)
         {
            logger.Log(LogLevel.Critical, ex.Message);
            if (ex.InnerException != null)
            {
               logger.Log(LogLevel.Critical, ex.InnerException.Message);
            }
            success = false;
         }

         return success;
      }


   }
}
