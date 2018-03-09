using System.Collections.Generic;

namespace Assembler.Common
{
    public class AssemblerOptions
    {

        /// <summary>
        /// Creates an instance of the options that the assembler will use.
        /// </summary>
        /// <param name="inputFileNames">The list of input file names to be assembled.</param>
        /// <param name="baseTextAddress">The base text address.</param>
        /// <param name="baseDataAddress"></param>
        public AssemblerOptions(IEnumerable<string> inputFileNames, 
                                int baseTextAddress, 
                                int baseDataAddress)
        {
            m_InputFileNames = inputFileNames;
            m_BaseTextAddress = baseTextAddress;
            m_BaseDataAddress = baseDataAddress;
        }
        /// <summary>
        /// Gets the list of files to assemble.
        /// </summary>
        public IEnumerable<string> InputFileNames
        {
            get { return m_InputFileNames; }
        }

        /// <summary>
        /// Gets the base address that the .text segment should begin at.
        /// </summary>
        public int BaseTextAddress
        {
            get { return m_BaseTextAddress; }
        }

        /// <summary>
        /// Gets the base address that the .data segment should begin at.
        /// </summary>
        public int BaseDataAddress
        {
            get { return m_BaseDataAddress; }
        }

        private readonly IEnumerable<string> m_InputFileNames;
        private readonly int m_BaseTextAddress;
        private readonly int m_BaseDataAddress;
    }
}
