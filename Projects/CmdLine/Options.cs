using Assembler.Common;
using CommandLine;
using System.Collections.Generic;

namespace Assembler.CmdLine
{
    /// <summary>
    /// Represents the command line arguments that can be used with this application.
    /// </summary>
    
    [Verb("assemble", HelpText ="Assembles a series of files.", Hidden = true)]
    class AssembleCmdOptions
    {
        [Option('i', "input", Required =true, HelpText = "Input assembly files to be assembled.")]
        public IEnumerable<string> InputFiles { get; set; }

        [Option('E', Default = true, HelpText = "Write data using big-endian notation. Cannot be combined with -e")]
        public bool UseBigEndian { get; set; }

        [Option('e', Default = true, HelpText = "Write data using little-endian notation. Cannot be combined with -E")]
        public bool UseLittleEndian { get; set; }
    }
}
