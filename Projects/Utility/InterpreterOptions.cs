using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Common
{
    [Verb("run", HelpText = "Runs a .JEF file.", Hidden = false)]
    public class InterpreterOptions
    {
        /// <summary>
        /// Creates an instance of the options that the disassembler will use.
        /// </summary>
        /// <param name="inputFileName">The list of input file names to be disassembled.</param>
        /// <param name="logFile">The name of the log file, if any, to output to.</param>
        public InterpreterOptions(string inputFileName,
                                  string logFile = "")
        {
            m_InputFileName = inputFileName;
            m_LogFile = logFile;
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


        private readonly string m_InputFileName;
        private readonly string m_LogFile;
    }
}
