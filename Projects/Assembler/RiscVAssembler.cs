using Assembler.Common;
using Assembler.Output;
using Assembler.Output.OutputWriters;
using Assembler.SymbolTableConstruction;
using Assembler.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            var stopwatch = new Stopwatch();

            var tasks = new List<Task<bool>>();
            stopwatch.Start();
            foreach (string file in options.InputFileNames)
            {
                var task = new Task<bool>(() => AssembleFile(file, logger, options));
                tasks.Add(task);
                task.Start();
            }

            // wait for all of our assembler tasks to join.
            Task.WaitAll(tasks.ToArray());
            stopwatch.Stop();
            
            logger.Log(LogLevel.Info, "Build completed in " + stopwatch.Elapsed.ToString());
        }

        /// <summary>
        /// Task for assembling one individual file.
        /// </summary>
        /// <param name="inputFile">The input file to assemble.</param>
        /// <param name="logger">The logging implementation to log errors/info to.</param>
        /// <param name="options">The options to use while assembling.</param>
        /// <returns>True if the assembler could successfully generate code for the file; otherwise returns false.</returns>
        public bool AssembleFile(string inputFile, ILogger logger, AssemblerOptions options)
        {
            bool success = true;
            logger.Log(LogLevel.Info, "Invoking assembler for file " + inputFile);
            try
            {
                // get the file name with no extension, in case we want intermediate files,
                // or for our output.
                string fileNameNoExtension = inputFile;
                if (inputFile.Contains("."))
                {
                    fileNameNoExtension = inputFile.Substring(0, inputFile.LastIndexOf('.'));
                }

                using (var reader = new StreamReader(File.OpenRead(inputFile)))
                {
                    var symTable = new SymbolTable();
                    var symTableBuilder = new SymbolTableBuilder();
                    symTableBuilder.GenerateSymbolTableForSegment(reader, SegmentType.Data, symTable);
                    symTableBuilder.GenerateSymbolTableForSegment(reader, SegmentType.Text, symTable);

                    var objFile = new BasicObjectFile(symTable);


                    IObjectFileWriter writer = ObjectFileWriterFactory.GetWriterForObjectType(OutputTypes.DirectBinary);
                    string outputFile = fileNameNoExtension + ".obj";
                    writer.WriteObjectFile(outputFile, objFile);
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
                logger.Log(LogLevel.Critical, ex.InnerException?.Message);
                success = false;
            }

            return success;
        }


    }
}
