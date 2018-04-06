using Assembler.Common;
using Assembler.Output.ObjFileComponents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Output.OutputWriters
{
    /// <summary>
    /// A class that represents a very basic output writer. This writes the data with no formatting,
    /// other than prefixing the type of data it is writing. This should be used for debugging purposes.
    /// </summary>
    class BasicBinaryObjectWriter : IObjectFileWriter
    {
        public BasicBinaryObjectWriter(Endianness targetEndianness)
        {
            m_TargetEndianness = targetEndianness;
        }

        /// <summary>
        /// Outputs all data in the BasicObjectFile to the specified format.
        /// </summary>
        /// <param name="fileName">The file path to generate the output at.</param>
        /// <param name="file">The data that will be written to the file.</param>
        public void WriteObjectFile(string fileName, BasicObjectFile file)
        {
            FileStream fs = File.Open(fileName, FileMode.Create);

            // write the .extern preamble
            fs.Write(Encoding.ASCII.GetBytes(".extern"), 0, Encoding.ASCII.GetByteCount(".extern"));
            foreach (IObjectFileComponent elem in file.ExternElements)
            {
                elem.WriteDataToFile(fs);
            }

            // write the .data preamble
            fs.Write(Encoding.ASCII.GetBytes(".data"), 0, Encoding.ASCII.GetByteCount(".data"));
            foreach (IObjectFileComponent elem in file.DataElements)
            {
                elem.WriteDataToFile(fs);
            }

            // write the .text preamble
            fs.Write(Encoding.ASCII.GetBytes(".text"), 0, Encoding.ASCII.GetByteCount(".text"));
            foreach (IObjectFileComponent elem in file.TextElements)
            {
                elem.WriteDataToFile(fs);
            }

            // write the symbol table
            fs.Write(Encoding.ASCII.GetBytes(".symtbl"), 0, Encoding.ASCII.GetByteCount(".symtbl"));
            foreach (Symbol elem in file.SymbolTable.Symbols)
            {
                fs.Write(Encoding.ASCII.GetBytes(elem.LabelName), 0, Encoding.ASCII.GetByteCount(elem.LabelName));
                byte[] byteRepresentation = BitConverter.GetBytes(elem.Address);
                // if the architecture we're assembling on is not our desired endianness,
                // flip the byte array.
                if (BitConverter.IsLittleEndian && m_TargetEndianness == Endianness.BigEndian ||
                    !BitConverter.IsLittleEndian && m_TargetEndianness == Endianness.LittleEndian)
                {
                    Array.Reverse(byteRepresentation);
                }

                fs.Write(byteRepresentation, 0, byteRepresentation.Length);
            }
        }

        private readonly Endianness m_TargetEndianness;
        
    }
}
