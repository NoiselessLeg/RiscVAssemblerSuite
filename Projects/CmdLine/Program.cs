using Assembler.CmdLine.LoggerTypes;
using Assembler.Common;
using Assembler.Disassembler;
using CommandLine;
using System;
using System.Collections.Generic;

namespace Assembler.CmdLine
{
    class Program
    {
        static void Main(string[] args)
        {
            var parsedArgs = Parser.Default.ParseArguments<AssemblerOptions, DisassemblerOptions>(args)
                                .WithParsed<AssemblerOptions>(options => RunAssembler(options))
                                .WithParsed<DisassemblerOptions>(options => RunDisassembler(options));

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
            assembler.Assemble(options, logger);
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

        private static int HandleErrors()
        {
            return -1;
        }
    }
}
