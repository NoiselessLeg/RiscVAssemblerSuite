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

        [Option('t', "textaddress", Default = BaseSegmentAddresses.BASE_TEXT_ADDRESS, Required = false, 
            HelpText = "Base .text segment address. This should not be modified unless you know what you're doing.")]
        public int BaseTextAddress { get; set; }

        [Option('d', "dataaddress", Default = BaseSegmentAddresses.BASE_DATA_ADDRESS, Required = false,
            HelpText = "Base .data segment address. This should not be modified unless you know what you're doing.")]
        public int BaseDataAddress { get; set; }
    }
}
