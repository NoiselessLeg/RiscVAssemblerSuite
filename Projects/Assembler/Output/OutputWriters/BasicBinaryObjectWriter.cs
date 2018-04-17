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
                using (MemoryStream tmpStrm = new MemoryStream())
                {
                    var sizeList = new List<int>();
                    // write the .data segment metadata
                    int dataMDataSize = WriteMetadataToFile(tmpStrm, file.DataElements);
                    // write the actual .data segment
                    int dataSegmentLength = WriteDataToFile(tmpStrm, file.DataElements);

                    int textSegmentLength = WriteDataToFile(tmpStrm, file.TextElements);
                    // write the .extern segment metadata
                    int externMdataLength = WriteMetadataToFile(tmpStrm, file.ExternElements);
                    // write the .extern segment proper.
                    int externSegmentLength = WriteDataToFile(tmpStrm, file.ExternElements);
                    
                    // write the symbol table
                    int startPos = (int)tmpStrm.Position;
                    foreach (Symbol elem in file.SymbolTable.Symbols)
                    {
                        // write the length of the symbol name, first.
                        tmpStrm.WriteByte((byte)elem.LabelName.Length);

                        // write the symbol name itself.
                        tmpStrm.Write(Encoding.ASCII.GetBytes(elem.LabelName), 0, Encoding.ASCII.GetByteCount(elem.LabelName));
                        byte[] byteRepresentation = ToByteArray(elem.Address, m_TargetEndianness);

                        tmpStrm.Write(byteRepresentation, 0, byteRepresentation.Length);
                    }
                    int newPos = (int)tmpStrm.Position;

                    int symTblLength = newPos - startPos;

                    // add the various sizes.
                    // the order matters, as this is the order in which they are written to the header.
                    sizeList.Add(dataMDataSize);
                    sizeList.Add(dataSegmentLength);
                    sizeList.Add(textSegmentLength);
                    sizeList.Add(externMdataLength);
                    sizeList.Add(externSegmentLength);
                    sizeList.Add(symTblLength);

                    // write the actual file header, now that we know our absolute offsets
                    WriteHeader(fs, sizeList);

                    // copy the temp stream to the actual file stream.
                    tmpStrm.Seek(0, SeekOrigin.Begin);
                    tmpStrm.CopyTo(fs);
                }
            }
        }

        /// <summary>
        /// Writes the actual elements of a segment to a stream.
        /// </summary>
        /// <param name="stream">The Stream object to write to.</param>
        /// <param name="elements">The IEnumerable of elements to write out.</param>
        /// <returns>The number of bytes written.</returns>
        private int WriteDataToFile(Stream stream, IEnumerable<IObjectFileComponent> elements)
        {
            long startPos = stream.Position;
            foreach (IObjectFileComponent elem in elements)
            {
                elem.WriteDataToFile(stream);
            }
            long newPos = stream.Position;

            return (int)(newPos - startPos);
        }

        /// <summary>
        /// Writes metadata about a segment to a stream.
        /// </summary>
        /// <param name="stream">The Stream object to write to.</param>
        /// <param name="elements">The IEnumerable of elements to write out the metadata of.</param>
        /// <returns>The number of bytes written.</returns>
        private int WriteMetadataToFile(Stream stream, IEnumerable<IObjectFileComponent> elements)
        {
            long startPos = stream.Position;
            foreach (IObjectFileComponent elem in elements)
            {
                elem.WriteMetadataToFile(stream);
            }
            long newPos = stream.Position;

            return (int)(newPos - startPos);
        }

        /// <summary>
        /// Writes the JEF file header to the file.
        /// </summary>
        /// <param name="fs">The FileStream object to write to.</param>
        /// <param name="segmentSizes">An IEnumerable of segment sizes, in the order that they appear in the file by the standard.</param>
        private void WriteHeader(Stream fs, IEnumerable<int> segmentSizes)
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
            // TODO: eventually make this configurable
            byte[] dataRuntimeAddr = ToByteArray(CommonConstants.BASE_DATA_ADDRESS, m_TargetEndianness);
            byte[] textRuntimeAddr = ToByteArray(CommonConstants.BASE_TEXT_ADDRESS, m_TargetEndianness);
            byte[] externRuntimeAddr = ToByteArray(CommonConstants.BASE_EXTERN_ADDRESS, m_TargetEndianness);
            fs.Write(dataRuntimeAddr, 0, dataRuntimeAddr.Length);
            fs.Write(textRuntimeAddr, 0, textRuntimeAddr.Length);
            fs.Write(externRuntimeAddr, 0, externRuntimeAddr.Length);

            // write the offset of the first segment after the header, since this is known.
            byte[] dataMetadataOffset = ToByteArray(0x30 - 0x12, m_TargetEndianness);
            fs.Write(dataMetadataOffset, 0, dataMetadataOffset.Length);

            // calculate the relative offsets to the various segments.
            // the first segment should begin at 0x30, and we should start writing
            // this section at offset 0x16. 
            int startingOffset = 0x30;
            int writePosition = 0x16;
            foreach (int size in segmentSizes)
            {
                int absoluteOffset = startingOffset + size;
                int relativeOffset = absoluteOffset - writePosition;

                byte[] relativeOffsetBytes = ToByteArray(relativeOffset, m_TargetEndianness);
                fs.Write(relativeOffsetBytes, 0, relativeOffsetBytes.Length);

                startingOffset = absoluteOffset;
                writePosition += sizeof(int);
            }

            // write the spare values.
            System.Diagnostics.Debug.Assert(startingOffset - writePosition > 0);
            byte[] dummyValues = new byte[startingOffset - writePosition];
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
