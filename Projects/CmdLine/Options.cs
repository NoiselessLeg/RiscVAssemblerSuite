using Assembler.Common;
using CommandLine;
using System.Collections.Generic;

namespace Assembler.CmdLine
{
    /// <summary>
    /// Represents the command line arguments that can be used with this application.
    /// </summary>
    class Options
    {
        [Option('i', "input", Required =true, HelpText = "Input assembly files to be assembled.")]
        public IEnumerable<string> InputFiles { get; set; }

        [Option('l', "logfile", Required = false, HelpText = "Log file to output to (optional).")]
        public string LogFile { get; set; }
    }
}
