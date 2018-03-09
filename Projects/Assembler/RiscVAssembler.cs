using Assembler.Common;
using Assembler.Output;
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
                var task = new Task<bool>(() => AssembleFile(file, options.BaseTextAddress, 
                                                             options.BaseDataAddress, logger));
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
        /// <param name="baseTextAddress">The base .text segment address.</param>
        /// <param name="baseDataAddress">The base .data segment address.</param>
        /// <param name="logger">The logging implementation to log errors/info to.</param>
        /// <returns>True if the assembler could successfully generate code for the file; otherwise returns false.</returns>
        public bool AssembleFile(string inputFile, int baseTextAddress, 
                                 int baseDataAddress, ILogger logger)
        {
            bool success = true;
            logger.Log(LogLevel.Info, "Invoking assembler for file " + inputFile);
            try
            {
                using (var reader = new StreamReader(File.OpenRead(inputFile)))
                {
                    var fp = new FirstPassAssembler(baseTextAddress, baseDataAddress);
                    SymbolTable symbolList = fp.GenerateSymbolTable(reader);
                    var spa = new SecondPassAssembler(symbolList, baseTextAddress);
                    logger.Log(LogLevel.DebugFine, "Found " + symbolList.NumSymbols + " symbols in file " + inputFile +'.');
                    logger.Log(LogLevel.Info, "Generating code for file " + inputFile + '.');
                    IEnumerable<int> instructions = spa.GenerateCode(reader);

                    // For now, we're going to be extremely dumb and dump the code with no formatting to
                    // a file, with the same name, but a .obj extension.
                    // TODO: not be dumb and do this intelligently.
                    string outputFileName = inputFile.Substring(0, inputFile.LastIndexOf('.')) + ".obj";
                    var outputGenerator = new BasicBinaryOutputWriter();
                    outputGenerator.CreateObjFile(instructions, outputFileName);
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
            }

            return success;
        }


    }
}
