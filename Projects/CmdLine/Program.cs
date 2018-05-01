using Assembler.CmdLine.LoggerTypes;
using Assembler.Common;
using Assembler.Disassembler;
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

#if DEBUG
            Console.ReadKey();
#endif
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
            bool wasSuccessful = assembler.Assemble(options, logger);
            if (wasSuccessful && options.RunAfterAssembly)
            {
                string fileName = options.InputFileNames.ElementAt(0);
                string outputName = fileName.Substring(0, fileName.LastIndexOf('.'));
                outputName += ".jef";
                var runtimeOps = new InterpreterOptions(outputName);
                RunInterpreter(runtimeOps);
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

            var disassembler = new RiscVDisassembler();
            disassembler.Disassemble(options, logger);
            return 0;
        }

        private static int RunInterpreter(InterpreterOptions options)
        {
            var terminal = new ConsoleTerminal();

            if (options.DebugDumpEnabled)
            {
                var interpreter = new FileDebugger(terminal);
                interpreter.RunJefFile(options.InputFileName, new ConsoleLogger());
            }
            else
            {
                var interpreter = new FileInterpreter(terminal);
                interpreter.RunJefFile(options.InputFileName, new ConsoleLogger());
            }
            return 0;
        }
    }
}
