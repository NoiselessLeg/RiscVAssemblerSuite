using Assembler.CmdLine.LoggerTypes;
using Assembler.Common;
using Assembler.UICommon;
using Assembler.Interpreter;
using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assembler.CmdLine
{
   class Program
   {
      static void Main(string[] args)
      {
         var parsedArgs = Parser.Default.ParseArguments<AssemblerOptions, DisassemblerOptions, InterpreterOptions>(args)
                             .WithParsed<AssemblerOptions>(options => RunAssembler(options))
                             .WithParsed<DisassemblerOptions>(options => RunDisassembler(options))
                             .WithParsed<InterpreterOptions>(options => RunInterpreter(options));
      }

      /// <summary>
      /// Runs the assembler with the provided set of arguments.
      /// </summary>
      /// <param name="options">The options provided by the user.</param>
      private static int RunAssembler(AssemblerOptions options)
      {
         ILogger logger = null;
         string logFileName = options.LogFile;
         if (!string.IsNullOrEmpty(logFileName) && !string.IsNullOrWhiteSpace(logFileName))
         {
            logger = new HybridLogger(logFileName.Trim());
         }
         else
         {
            logger = new ConsoleLogger();
         }

         RiscVAssembler assembler = new RiscVAssembler();
         try
         {
            assembler.Assemble(options, logger);
            if (options.RunAfterAssembly)
            {
               string inputFile = options.InputFileNames.ElementAt(0);
               // get the file name with no extension, in case we want intermediate files,
               // or for our output.
               string fileNameNoExtension = inputFile;
               if (inputFile.Contains("."))
               {
                  fileNameNoExtension = inputFile.Substring(0, inputFile.LastIndexOf('.'));
               }

               //TODO: this will def need to change if we implement more filetypes.
               string outputFile = fileNameNoExtension + ".jef";

               var runtimeOps = new InterpreterOptions(outputFile);
               RunInterpreter(runtimeOps);
            }
         }
         catch (Exception)
         {

         }

         return 0;
      }

      /// <summary>
      /// Runs the disassembler with the provided set of arguments.
      /// </summary>
      /// <param name="options">The options provided by the user.</param>
      private static int RunDisassembler(DisassemblerOptions options)
      {
         ILogger logger = null;
         string logFileName = options.LogFile;
         if (!string.IsNullOrEmpty(logFileName) && !string.IsNullOrWhiteSpace(logFileName))
         {
            logger = new HybridLogger(logFileName.Trim());
         }
         else
         {
            logger = new ConsoleLogger();
         }

         var disassembler = new Disassembler();
         disassembler.Disassemble(options, logger);
         return 0;
      }

      private static int RunInterpreter(InterpreterOptions options)
      {
         bool isRestarting = false;

         string inputFileName = options.InputFileName;
         var interpreter = new Simulator.CommandInterpreter();
         do
         {
            var consoleSim = new Simulator.ConsoleSimulation(inputFileName, interpreter);
            consoleSim.RunSimulator();
            isRestarting = consoleSim.IsSimulatorRestarting;
            if (isRestarting)
            {
               inputFileName = consoleSim.AsmFileToLoadOnRestart;
            }
         }
         while (isRestarting);
         return 0;
      }
   }
}
