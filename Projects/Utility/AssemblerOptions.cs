
using CommandLine;
using System;
using System.Collections.Generic;

namespace Assembler.Common
{
    [Verb("assemble", HelpText = "Assembles a series of files.", Hidden = true)]
    public class AssemblerOptions
    {

        /// <summary>
        /// Creates an instance of the options that the assembler will use.
        /// </summary>
        /// <param name="inputFileNames">The list of input file names to be assembled.</param>
        /// <param name="endianness">The endianness to output with. If unspecified, this will use the architecture's endianness.</param>
        /// <param name="logFile">The name of the log file, if any, to output to.</param>
        public AssemblerOptions(IEnumerable<string> inputFileNames, 
                                Endianness endianness,
                                string logFile = "")
        {
            m_InputFileNames = inputFileNames;
            m_LogFile = logFile;
            if (endianness == Endianness.Unspecified)
            {
                m_Endianness = (BitConverter.IsLittleEndian ? Endianness.LittleEndian : Endianness.BigEndian);
            }
            else
            {
                m_Endianness = endianness;
            }

            // JAL 4/14: override endianness for now and force little endian, since that's the processor spec.
            m_Endianness = Endianness.LittleEndian;
        }

        /// <summary>
        /// Gets the list of files to assemble.
        /// </summary>
        [Option('i', "input", Required = true, HelpText = "Input assembly files to be assembled.")]
        public IEnumerable<string> InputFileNames
        {
            get { return m_InputFileNames; }
        }

        [Option('e', "endianness", SetName = "", Default = Endianness.Unspecified,  Required = false, HelpText = "Desired endianness of output file. If unspecified, this" + 
            " will use the architecture of the compiling computer's endianness.")]
        public Endianness Endianness
        {
            get { return m_Endianness; }
        }

        [Option('l', "logfile", Required = false, HelpText = "Log file to output to (optional).")]
        public string LogFile
        {
            get { return m_LogFile; }
        }

        private readonly IEnumerable<string> m_InputFileNames;
        private readonly Endianness m_Endianness;
        private readonly string m_LogFile;
    }
}
