using System.Collections.Generic;

namespace Assembler.Common
{
    public class AssemblerOptions
    {

        /// <summary>
        /// Creates an instance of the options that the assembler will use.
        /// </summary>
        /// <param name="inputFileNames">The list of input file names to be assembled.</param>
        public AssemblerOptions(IEnumerable<string> inputFileNames)
        {
            m_InputFileNames = inputFileNames;
        }
        /// <summary>
        /// Gets the list of files to assemble.
        /// </summary>
        public IEnumerable<string> InputFileNames
        {
            get { return m_InputFileNames; }
        }

        private readonly IEnumerable<string> m_InputFileNames;
    }
}
