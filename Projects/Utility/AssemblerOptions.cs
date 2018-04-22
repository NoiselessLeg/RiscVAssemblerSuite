
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
        /// <param name="runAfterAssembly">If true, the interpreter will execute the assembled file after code is generated.</param>
        public AssemblerOptions(IEnumerable<string> inputFileNames,
                                string logFile = "",
                                bool runAfterAssembly = false)
        {
            m_InputFileNames = inputFileNames;
            m_LogFile = logFile;
            m_RunAfterAssembly = runAfterAssembly;
        }

        /// <summary>
        /// Gets the list of files to assemble.
        /// </summary>
        [Option('i', "input", Required = true, HelpText = "Input assembly files to be assembled.")]
        public IEnumerable<string> InputFileNames
        {
            get { return m_InputFileNames; }
        }


        /// <summary>
        /// The log file that any log data will be written out to.
        /// </summary>
        [Option('l', "logfile", Required = false, HelpText = "Log file to output to (optional).")]
        public string LogFile
        {
            get { return m_LogFile; }
        }

        /// <summary>
        /// Gets a boolean value representing if the file will be executed by the runtime after assembly.
        /// </summary>
        [Option('r', "run-after", Default=false, Required = false, HelpText = "Run this file after successful assembly.")]
        public bool RunAfterAssembly
        {
            get { return m_RunAfterAssembly; }
        }

        private readonly IEnumerable<string> m_InputFileNames;
        private readonly string m_LogFile;
        private readonly bool m_RunAfterAssembly;
    }
}
