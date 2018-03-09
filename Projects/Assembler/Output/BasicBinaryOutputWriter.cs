using System.Collections.Generic;
using System.IO;

namespace Assembler.Output
{
    /// <summary>
    /// A basic data writer class that writes binary data to a file with no formatting nor structure.
    /// </summary>
    class BasicBinaryOutputWriter
    {
        /// <summary>
        /// Writes binary data in a big-endian format to a file.
        /// </summary>
        /// <param name="instructions">The list of integer instructions to write out.</param>
        /// <param name="outputFileName">The file name that will be written to. If it exists, it will be overwritten.</param>
        public void CreateObjFile(IEnumerable<int> instructions, string outputFileName)
        {
            using (var writer = new BinaryWriter(File.Open(outputFileName, FileMode.Create)))
            {
                foreach (int instruction in instructions)
                {
                    // extension method to invert the endianness.
                    // BinaryWriter uses little-endian, but RISC-V manual shows instruction
                    // format in big-endian.
                    // TODO: determine if this needs to become platform independent.
                    writer.Write(instruction, true);
                }
            }
        }
    }
}
