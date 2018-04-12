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
            using (FileStream fs = File.Open(fileName, FileMode.Create))
            {
                // write the actual file header.
                WriteHeader(fs, file);

                // write the .data preamble
                //fs.Write(Encoding.ASCII.GetBytes(".data"), 0, Encoding.ASCII.GetByteCount(".data"));
                foreach (IObjectFileComponent elem in file.DataElements)
                {
                    elem.WriteDataToFile(fs);
                }

                // write the .text preamble
                //fs.Write(Encoding.ASCII.GetBytes(".text"), 0, Encoding.ASCII.GetByteCount(".text"));
                foreach (IObjectFileComponent elem in file.TextElements)
                {
                    elem.WriteDataToFile(fs);
                }

                // write the .extern data.
                //fs.Write(Encoding.ASCII.GetBytes(".extern"), 0, Encoding.ASCII.GetByteCount(".extern"));
                foreach (IObjectFileComponent elem in file.ExternElements)
                {
                    elem.WriteDataToFile(fs);
                }

                // write the symbol table
                //fs.Write(Encoding.ASCII.GetBytes(".symtbl"), 0, Encoding.ASCII.GetByteCount(".symtbl"));
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
        }

        /// <summary>
        /// Writes the JEF file header to the file.
        /// </summary>
        /// <param name="fs">The FileStream object to write to.</param>
        /// <param name="objFile">The BasicObjectFile that contains the data to calculate offsets to.</param>
        private void WriteHeader(FileStream fs, BasicObjectFile objFile)
        {
            // write the magic four bytes
            fs.WriteByte(0x7F);

            // the lamest output format type.
            fs.Write(Encoding.ASCII.GetBytes("JEF"), 0, Encoding.ASCII.GetByteCount("JEF"));

            // we're using 32-bits for now, so set this to one.
            // maybe if we deal with 64 bits, make this 2.
            fs.WriteByte(0x01);

            // write the endianness byte.
            switch (m_TargetEndianness)
            {
                case Endianness.LittleEndian:
                {
                    fs.WriteByte(0x01);
                    break;
                }
                case Endianness.BigEndian:
                {
                    fs.WriteByte(0x02);
                    break;
                }
            }

            // write the runtime starting addresses.
            byte[] dataRuntimeAddr = ToByteArray(CommonConstants.BASE_DATA_ADDRESS, m_TargetEndianness);
            byte[] textRuntimeAddr = ToByteArray(CommonConstants.BASE_TEXT_ADDRESS, m_TargetEndianness);
            byte[] externRuntimeAddr = ToByteArray(CommonConstants.BASE_EXTERN_ADDRESS, m_TargetEndianness);
            fs.Write(dataRuntimeAddr, 0, dataRuntimeAddr.Length);
            fs.Write(textRuntimeAddr, 0, textRuntimeAddr.Length);
            fs.Write(externRuntimeAddr, 0, externRuntimeAddr.Length);

            // calculate the relative offsets to the various segments.
            // the .data segment should start at about offset 0x30, so subtract 0x12 from that.
            // which gives us 0x1E.
            byte[] relativeDataOffset = ToByteArray(0x1E, m_TargetEndianness);
            fs.Write(relativeDataOffset, 0, relativeDataOffset.Length);

            // calculate the relative .text offset
            // the .data segment should start at offset 0x30, so add the size to that to figure
            // out where the .text segment begins.
            int textSegmentOffset = 0x30 + objFile.DataSegmentSize;
            int relativeTxtSegmentOffset = textSegmentOffset - 0x16;
            byte[] relTxtSegmentOffsetBytes = ToByteArray(relativeTxtSegmentOffset, m_TargetEndianness);
            fs.Write(relTxtSegmentOffsetBytes, 0, relTxtSegmentOffsetBytes.Length);

            // calculate the relative .extern offset
            // the .data segment should start at offset 0x30, so add the size to that to figure
            // out where the .text segment begins, then add the .text segment size to determine where the .extern
            // segment begins.
            int externSegmentOffset = textSegmentOffset + objFile.TextSegmentSize;
            int relativeExtSegmentOffset = externSegmentOffset - 0x1A;
            byte[] relExtSegmentOffsetBytes = ToByteArray(relativeExtSegmentOffset, m_TargetEndianness);
            fs.Write(relExtSegmentOffsetBytes, 0, relExtSegmentOffsetBytes.Length);

            // calculate the relative .symtbl offset
            // just like we did before with the others
            int symtblSegmentOffset = externSegmentOffset + objFile.ExternSegmentSize;
            int relativeSymTblSegmentOffset = symtblSegmentOffset - 0x1E;
            byte[] relSymSegmentOffsetBytes = ToByteArray(relativeSymTblSegmentOffset, m_TargetEndianness);
            fs.Write(relSymSegmentOffsetBytes, 0, relSymSegmentOffsetBytes.Length);

            // write the spare values.
            byte[] dummyValues = new byte[14];
            fs.Write(dummyValues, 0, dummyValues.Length);
        }

        /// <summary>
        /// Gets the provided 32 bit as a byte array.
        /// </summary>
        /// <param name="param">The value to convert to bytes.</param>
        private static byte[] ToByteArray(int param, Endianness targetEndianness)
        {
            byte[] byteRep = BitConverter.GetBytes(param);

            // if the architecture we're assembling on is not our desired endianness,
            // flip the byte array.
            if (BitConverter.IsLittleEndian && targetEndianness == Endianness.BigEndian ||
                !BitConverter.IsLittleEndian && targetEndianness == Endianness.LittleEndian)
            {
                Array.Reverse(byteRep);
            }
            return byteRep;
        }

        private readonly Endianness m_TargetEndianness;
        
    }
}
