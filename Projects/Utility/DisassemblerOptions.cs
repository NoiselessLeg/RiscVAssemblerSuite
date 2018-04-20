using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Common
{
    [Verb("disassemble", HelpText = "Diassembles a .JEF file into source assembly.", Hidden = false)]
    public class DisassemblerOptions
    {
        /// <summary>
        /// Creates an instance of the options that the disassembler will use.
        /// </summary>
        /// <param name="inputFileNames">The list of input file names to be disassembled.</param>
        /// <param name="inputFileNames">The list of input file names to be assembled.</param>
        /// <param name="logFile">The name of the log file, if any, to output to.</param>
        public DisassemblerOptions(string inputFile,
                                   string outputFile,
                                   string logFile = "")
        {
            m_InputFileName = inputFile;
            m_OutputFileName = outputFile;
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

        /// <summary>
        /// Gets the name of the file to output the disassembly to.
        /// </summary>
        [Option('o', "output", Required = true, HelpText = "Name of the output file to output to.")]
        public string OutputFileName
        {
            get { return m_OutputFileName; }
        }

        [Option('l', "logfile", Required = false, HelpText = "Log file to output to (optional).")]
        public string LogFile
        {
            get { return m_LogFile; }
        }

        private readonly string m_InputFileName;
        private readonly string m_OutputFileName;
        private readonly string m_LogFile;
    }
}
