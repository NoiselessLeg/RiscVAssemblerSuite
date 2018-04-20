using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Common
{
    /// <summary>
    /// Class that controls access to a program's data segment.
    /// </summary>
    public class DataSegmentAccessor
    {
        /// <summary>
        /// Creates the .data segment accessor for simulating the RISC-V processor.
        /// </summary>
        /// <param name="data">An IEnumerable of raw data bytes arranged in the target platform's endianness</param>
        /// <param name="metadata">An IEnumerable of metadata that assists a disassembler.</param>
        /// <param name="runtimeDataSegmentStart">The address that the .data segment will be simulated to be located during runtime.</param>
        public DataSegmentAccessor(IEnumerable<byte> data, IEnumerable<MetadataElement> metadata, int runtimeDataSegmentStart)
        {
            m_ByteArray = data.ToArray();
            m_Metadata = metadata;
            m_RuntimeDataSegmentOffset = runtimeDataSegmentStart;
        }

        /// <summary>
        /// Gets the data segment as raw bytes in the target platform's endianness.
        /// </summary>
        public IEnumerable<byte> RawData
        {
            get { return m_ByteArray; }
        }

        /// <summary>
        /// Gets the metadata associated with the raw data segment.
        /// </summary>
        public IEnumerable<MetadataElement> Metadata
        {
            get { return m_Metadata; }
        }

        /// <summary>
        /// Gets the starting runtime address of the .data segment.
        /// </summary>
        public int BaseRuntimeDataAddress
        {
            get { return m_RuntimeDataSegmentOffset; }
        }

        /// <summary>
        /// Reads a byte from the data segment.
        /// </summary>
        /// <param name="address">The address in the .data segment to retrieve the byte from.</param>
        /// <returns>The byte stored at the provided address.</returns>
        public byte ReadByte(int address)
        {
            int idx = address - m_RuntimeDataSegmentOffset;
            return m_ByteArray[idx];
        }

        /// <summary>
        /// Reads a 16 bit signed integer from the data segment.
        /// </summary>
        /// <param name="address">The address in the .data segment to retrieve the value from.</param>
        /// <returns>The 16-bit signed integer stored at the provided address.</returns>
        public short ReadShort(int address)
        {
            int idx = address - m_RuntimeDataSegmentOffset;
            return BitConverter.ToInt16(m_ByteArray, idx);
        }

        /// <summary>
        /// Reads a 16 bit unsigned integer from the data segment.
        /// </summary>
        /// <param name="address">The address in the .data segment to retrieve the value from.</param>
        /// <returns>The 16-bit unsigned integer stored at the provided address.</returns>
        public ushort ReadUnsignedShort(int address)
        {
            int idx = address - m_RuntimeDataSegmentOffset;
            return BitConverter.ToUInt16(m_ByteArray, idx);
        }

        /// <summary>
        /// Reads a 32 bit signed integer from the data segment.
        /// </summary>
        /// <param name="address">The address in the .data segment to retrieve the value from.</param>
        /// <returns>The 32-bit signed integer stored at the provided address.</returns>
        public int ReadWord(int address)
        {
            int idx = address - m_RuntimeDataSegmentOffset;
            return BitConverter.ToInt32(m_ByteArray, idx);
        }

        /// <summary>
        /// Reads a 32 bit unsigned integer from the data segment.
        /// </summary>
        /// <param name="address">The address in the .data segment to retrieve the value from.</param>
        /// <returns>The 32-bit unsigned integer stored at the provided address.</returns>
        public uint ReadUnsignedWord(int address)
        {
            int idx = address - m_RuntimeDataSegmentOffset;
            return BitConverter.ToUInt32(m_ByteArray, idx);
        }

        /// <summary>
        /// Reads a 64 bit signed integer from the data segment.
        /// </summary>
        /// <param name="address">The address in the .data segment to retrieve the value from.</param>
        /// <returns>The 64-bit signed integer stored at the provided address.</returns>
        public long ReadLong(int address)
        {
            int idx = address - m_RuntimeDataSegmentOffset;
            return BitConverter.ToInt64(m_ByteArray, idx);
        }

        /// <summary>
        /// Reads a 64 bit unsigned integer from the data segment.
        /// </summary>
        /// <param name="address">The address in the .data segment to retrieve the value from.</param>
        /// <returns>The 64-bit unsigned integer stored at the provided address.</returns>
        public ulong ReadUnsignedLong(int address)
        {
            int idx = address - m_RuntimeDataSegmentOffset;
            return BitConverter.ToUInt64(m_ByteArray, idx);
        }

        /// <summary>
        /// Reads a null-terminated ASCII string from the .data segment.
        /// </summary>
        /// <param name="address">The address to retrieve the string from.</param>
        /// <returns>A string encoded in the ASCII encoding.</returns>
        public string ReadString(int address)
        {
            int idx = address - m_RuntimeDataSegmentOffset;
            int strSize = 0;
            int itr = idx;

            // go until we find a null terminator.
            while (m_ByteArray[itr] != 0)
            {
                ++strSize;
                ++itr;
            }

            return Encoding.ASCII.GetString(m_ByteArray, idx, strSize);
        }

        private readonly byte[] m_ByteArray;
        private readonly IEnumerable<MetadataElement> m_Metadata;
        private readonly int m_RuntimeDataSegmentOffset;
    }
}
