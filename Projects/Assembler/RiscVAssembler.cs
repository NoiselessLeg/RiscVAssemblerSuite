﻿using Assembler.CodeGeneration;
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
      public void Assemble(AssemblerOptions options, ILogger logger)
      {
         if (options.InputFileNames.Count() != options.OutputFileNames.Count())
         {
            logger.Log(LogLevel.Critical, "Input file names do not match output file names.");
         }
         else
         {
            var stopwatch = new Stopwatch();

            var tasks = new List<Task<AssemblerResult>>();
            stopwatch.Start();
            for (int idx = 0; idx < options.InputFileNames.Count(); ++idx)
            {
               string inputFileName = options.InputFileNames.ElementAt(idx);
               string outputFileName = options.OutputFileNames.ElementAt(idx);
               var task = new Task<AssemblerResult>(() => AssembleFile(inputFileName, outputFileName, logger, options));
               tasks.Add(task);
               task.Start();
            }

            // wait for all of our assembler tasks to join.
            Task.WaitAll(tasks.ToArray());
            stopwatch.Stop();
            
            if (tasks.Any(t => (!t.Result.OperationSuccessful)))
            {
               logger.Log(LogLevel.Info, "Build completed (with errors) in " + stopwatch.Elapsed.ToString());
            }
            else
            {
               logger.Log(LogLevel.Info, "Build completed in " + stopwatch.Elapsed.ToString());
            }
         }
      }

      /// <summary>
      /// Task for assembling one individual file.
      /// </summary>
      /// <param name="inputFile">The input file to assemble.</param>
      /// <param name="logger">The logging implementation to log errors/info to.</param>
      /// <param name="options">The options to use while assembling.</param>
      /// <returns>True if the assembler could successfully generate code for the file; otherwise returns false.</returns>
      public AssemblerResult AssembleFile(string inputFile, string outputFile, ILogger logger, AssemblerOptions options)
      {
         var result = new AssemblerResult();
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
                  var objFile = new BasicObjectFile(inputFile, symTable);

                  var codeGenerator = new CodeGenerator(logger, symTable, instructionProcFac);
                  codeGenerator.GenerateCode(inputFile, reader, objFile);

                  if (!objFile.TextElements.Any())
                  {
                     logger.Log(LogLevel.Warning, "No .text segment to assemble. Stop.");
                     result.OperationSuccessful = false;
                  }
                  else
                  {
                     // write the object file out.
                     var writerFac = new ObjectFileWriterFactory();
                     IObjectFileWriter writer = writerFac.GetWriterForOutputFile(outputFile);
                     writer.WriteObjectFile(outputFile, objFile);
                  }
               }
            }
         }
         catch (AggregateAssemblyError ex)
         {
            foreach (AssemblyException asEx in ex.AssemblyErrors)
            {
               logger.Log(LogLevel.Critical, "In file \"" + inputFile + "\" (line " + asEx.LineNumber + "):\n\t");
               logger.Log(LogLevel.Critical, asEx.Message);
               result.AddUserAssemblyError(asEx);
            }
         }
         catch (Exception ex)
         {
            logger.Log(LogLevel.Critical, ex.StackTrace);
            logger.Log(LogLevel.Critical, ex.Message);
            if (ex.InnerException != null)
            {
               logger.Log(LogLevel.Critical, ex.InnerException.StackTrace);
               logger.Log(LogLevel.Critical, ex.InnerException.Message);
            }
            result.AddInternalAssemblerError(ex);
         }

         return result;
      }
   }
}
