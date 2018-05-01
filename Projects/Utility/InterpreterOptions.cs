using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Common
{
    [Verb("run", HelpText = "Runs a .JEF file.")]
    public class InterpreterOptions
    {
        /// <summary>
        /// Creates an instance of the options that the disassembler will use.
        /// </summary>
        /// <param name="inputFileName">The list of input file names to be disassembled.</param>
        /// <param name="logFile">The name of the log file, if any, to output to.</param>
        /// <param name="debugDumpEnabled">If true, SIGSEGV or access violations will trigger a register dump.</param>
        public InterpreterOptions(string inputFileName,
                                  string logFile = "",
                                  bool debugDumpEnabled = false)
        {
            m_InputFileName = inputFileName;
            m_LogFile = logFile;
            m_DebugDumpEnabled = debugDumpEnabled;
        }

        /// <summary>
        /// Gets the list of files to assemble.
        /// </summary>
        [Option('i', "input", Required = true, HelpText = "Input .JEF file to be disassembled.")]
        public string InputFileName
        {
            get { return m_InputFileName; }
        }

        [Option('l', "logfile", Required = false, HelpText = "Log file to output to (optional).")]
        public string LogFile
        {
            get { return m_LogFile; }
        }

        [Option('d', "debug-dumps", Required = false, Default = false,  HelpText = "If enabled, SIGSEGV signals generate register dumps.")]
        public bool DebugDumpEnabled
        {
            get { return m_DebugDumpEnabled; }
        }

        private readonly string m_InputFileName;
        private readonly string m_LogFile;
        private readonly bool m_DebugDumpEnabled;
    }
}
