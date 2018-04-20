
using CommandLine;
using System;
using System.Collections.Generic;

namespace Assembler.Common
{
    [Verb("assemble", HelpText = "Assembles a series of files.", Hidden = false)]
    public class AssemblerOptions
    {

        /// <summary>
        /// Creates an instance of the options that the assembler will use.
        /// </summary>
        /// <param name="inputFileNames">The list of input file names to be assembled.</param>
        /// <param name="logFile">The name of the log file, if any, to output to.</param>
        public AssemblerOptions(IEnumerable<string> inputFileNames,
                                string logFile = "")
        {
            m_InputFileNames = inputFileNames;
            m_LogFile = logFile;
        }

        /// <summary>
        /// Gets the list of files to assemble.
        /// </summary>
        [Option('i', "input", Required = true, HelpText = "Input assembly files to be assembled.")]
        public IEnumerable<string> InputFileNames
        {
            get { return m_InputFileNames; }
        }

        [Option('l', "logfile", Required = false, HelpText = "Log file to output to (optional).")]
        public string LogFile
        {
            get { return m_LogFile; }
        }

        private readonly IEnumerable<string> m_InputFileNames;
        private readonly string m_LogFile;
    }
}
