using Assembler.CmdLine.LoggerTypes;
using Assembler.Common;
using CommandLine;
using System;

namespace Assembler.CmdLine
{
    class Program
    {
        static void Main(string[] args)
        {
            var parsedArgs = Parser.Default.ParseArguments<AssemblerOptions>(args)
                                .WithParsed(options => RunAssembler(options));

#if DEBUG
            Console.ReadKey();
#endif
        }

        /// <summary>
        /// Runs the assembler with the provided set of arguments.
        /// </summary>
        /// <param name="options"></param>
        private static void RunAssembler(AssemblerOptions options)
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
        }
    }
}
