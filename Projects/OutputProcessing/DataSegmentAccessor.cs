using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.OutputProcessing
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

        public void InitRuntimeData()
        {

        }

        /// <summary>
        /// Reads a byte from the data segment.
        /// </summary>
        /// <param name="address">The address in the .data segment to retrieve the byte from.</param>
        /// <returns>The byte stored at the provided address.</returns>
        public sbyte ReadSignedByte(int address)
        {
            try
            {
                int idx = address - m_RuntimeDataSegmentOffset;
                return (sbyte)m_ByteArray[idx];
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Attempted out-of-bounds 8-bit memory read of address 0x" + address.ToString("X2"));
            }
        }

        /// <summary>
        /// Reads an unsigned byte from the data segment.
        /// </summary>
        /// <param name="address">The address in the .data segment to retrieve the byte from.</param>
        /// <returns>The byte stored at the provided address.</returns>
        public byte ReadUnsignedByte(int address)
        {
            try
            {
                int idx = address - m_RuntimeDataSegmentOffset;
                return m_ByteArray[idx];
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Attempted out-of-bounds 8-bit memory read of address 0x" + address.ToString("X2"));
            }
        }

        /// <summary>
        /// Reads a 16 bit signed integer from the data segment.
        /// </summary>
        /// <param name="address">The address in the .data segment to retrieve the value from.</param>
        /// <returns>The 16-bit signed integer stored at the provided address.</returns>
        public short ReadShort(int address)
        {
            try
            {
                int idx = address - m_RuntimeDataSegmentOffset;
                return BitConverter.ToInt16(m_ByteArray, idx);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Attempted out-of-bounds 16-bit memory read of address 0x" + address.ToString("X2"));
            }
        }

        /// <summary>
        /// Reads a 16 bit unsigned integer from the data segment.
        /// </summary>
        /// <param name="address">The address in the .data segment to retrieve the value from.</param>
        /// <returns>The 16-bit unsigned integer stored at the provided address.</returns>
        public ushort ReadUnsignedShort(int address)
        {
            try
            {
                int idx = address - m_RuntimeDataSegmentOffset;
                return BitConverter.ToUInt16(m_ByteArray, idx);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Attempted out-of-bounds 16-bit memory read of address 0x" + address.ToString("X2"));
            }
        }

        /// <summary>
        /// Reads a 32 bit signed integer from the data segment.
        /// </summary>
        /// <param name="address">The address in the .data segment to retrieve the value from.</param>
        /// <returns>The 32-bit signed integer stored at the provided address.</returns>
        public int ReadWord(int address)
        {
            try
            {
                int idx = address - m_RuntimeDataSegmentOffset;
                return BitConverter.ToInt32(m_ByteArray, idx);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Attempted out-of-bounds 32-bit memory read of address 0x" + address.ToString("X2"));
            }
        }

        /// <summary>
        /// Reads a 32 bit unsigned integer from the data segment.
        /// </summary>
        /// <param name="address">The address in the .data segment to retrieve the value from.</param>
        /// <returns>The 32-bit unsigned integer stored at the provided address.</returns>
        public uint ReadUnsignedWord(int address)
        {
            try
            {
                int idx = address - m_RuntimeDataSegmentOffset;
                return BitConverter.ToUInt32(m_ByteArray, idx);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Attempted out-of-bounds 32-bit memory read of address 0x" + address.ToString("X2"));
            }
        }

        /// <summary>
        /// Reads a 64 bit signed integer from the data segment.
        /// </summary>
        /// <param name="address">The address in the .data segment to retrieve the value from.</param>
        /// <returns>The 64-bit signed integer stored at the provided address.</returns>
        public long ReadLong(int address)
        {
            try
            {
                int idx = address - m_RuntimeDataSegmentOffset;
                return BitConverter.ToInt64(m_ByteArray, idx);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Attempted out-of-bounds 64-bit memory read of address 0x" + address.ToString("X2"));
            }
        }

        /// <summary>
        /// Reads a 64 bit unsigned integer from the data segment.
        /// </summary>
        /// <param name="address">The address in the .data segment to retrieve the value from.</param>
        /// <returns>The 64-bit unsigned integer stored at the provided address.</returns>
        public ulong ReadUnsignedLong(int address)
        {
            try
            {
                int idx = address - m_RuntimeDataSegmentOffset;
                return BitConverter.ToUInt64(m_ByteArray, idx);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Attempted out-of-bounds 64-bit memory read of address 0x" + address.ToString("X2"));
            }
        }

        /// <summary>
        /// Reads a null-terminated ASCII string from the .data segment.
        /// </summary>
        /// <param name="address">The address to retrieve the string from.</param>
        /// <returns>A string encoded in the ASCII encoding.</returns>
        public string ReadString(int address)
        {
            try
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
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Attempted out of bounds string read from memory at address 0x" + address.ToString("X2"));
            }
        }

        /// <summary>
        /// Writes a signed byte into the specified .data segment offset.
        /// </summary>
        /// <param name="address">The address in the .data segment to write to.</param>
        /// <param name="value">The value to write to the address.</param>
        public void WriteSignedByte(int address, sbyte value)
        {
            try
            {
                int idx = address - m_RuntimeDataSegmentOffset;
                m_ByteArray[idx] = (byte)value;
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Attempted out of bounds 8 bit memory write to address 0x" + address.ToString("X2"));
            }
        }

        /// <summary>
        /// Writes an unsigned byte into the specified .data segment offset.
        /// </summary>
        /// <param name="address">The address in the .data segment to write to.</param>
        /// <param name="value">The value to write to the address.</param>
        public void WriteUnsignedByte(int address, byte value)
        {
            try
            {
                int idx = address - m_RuntimeDataSegmentOffset;
                m_ByteArray[idx] = value;
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Attempted out of bounds 8 bit memory write to address 0x" + address.ToString("X2"));
            }
        }

        /// <summary>
        /// Writes a 16-bit signed integer into the specified .data segment offset.
        /// </summary>
        /// <param name="address">The address in the .data segment to write to.</param>
        /// <param name="value">The value to write to the address.</param>
        public void WriteShort(int address, short value)
        {
            try
            {
                int idx = address - m_RuntimeDataSegmentOffset;
                byte[] shortBytes = BitConverter.GetBytes(value);
                CopyBytes(shortBytes, idx);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Attempted out of bounds 16 bit memory write to address 0x" + address.ToString("X2"));
            }
        }

        /// <summary>
        /// Writes a 16-bit unsigned integer into the specified .data segment offset.
        /// </summary>
        /// <param name="address">The address in the .data segment to write to.</param>
        /// <param name="value">The value to write to the address.</param>
        public void WriteUnsignedShort(int address, ushort value)
        {
            try
            {
                int idx = address - m_RuntimeDataSegmentOffset;
                byte[] shortBytes = BitConverter.GetBytes(value);
                CopyBytes(shortBytes, idx);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Attempted out of bounds 16 bit memory write to address 0x" + address.ToString("X2"));
            }
        }

        /// <summary>
        /// Writes a 32-bit signed integer into the specified .data segment offset.
        /// </summary>
        /// <param name="address">The address in the .data segment to write to.</param>
        /// <param name="value">The value to write to the address.</param>
        public void WriteWord(int address, int value)
        {
            try
            {
                int idx = address - m_RuntimeDataSegmentOffset;
                byte[] wordBytes = BitConverter.GetBytes(value);
                CopyBytes(wordBytes, idx);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Attempted out of bounds 32 bit memory write to address 0x" + address.ToString("X2"));
            }
        }

        /// <summary>
        /// Writes a 32-bit unsigned integer into the specified .data segment offset.
        /// </summary>
        /// <param name="address">The address in the .data segment to write to.</param>
        /// <param name="value">The value to write to the address.</param>
        public void WriteUnsignedWord(int address, uint value)
        {
            try
            {
                int idx = address - m_RuntimeDataSegmentOffset;
                byte[] wordBytes = BitConverter.GetBytes(value);
                CopyBytes(wordBytes, idx);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Attempted out of bounds 32 bit memory write to address 0x" + address.ToString("X2"));
            }
        }

        /// <summary>
        /// Writes a 64-bit signed integer into the specified .data segment offset.
        /// </summary>
        /// <param name="address">The address in the .data segment to write to.</param>
        /// <param name="value">The value to write to the address.</param>
        public void WriteLong(int address, long value)
        {
            try
            {
                int idx = address - m_RuntimeDataSegmentOffset;
                byte[] longBytes = BitConverter.GetBytes(value);
                CopyBytes(longBytes, idx);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Attempted out of bounds 64 bit memory write to address 0x" + address.ToString("X2"));
            }
        }

        /// <summary>
        /// Writes a 64-bit unsigned integer into the specified .data segment offset.
        /// </summary>
        /// <param name="address">The address in the .data segment to write to.</param>
        /// <param name="value">The value to write to the address.</param>
        public void WriteUnsignedLong(int address, ulong value)
        {
            try
            {
                int idx = address - m_RuntimeDataSegmentOffset;
                byte[] longBytes = BitConverter.GetBytes(value);
                CopyBytes(longBytes, idx);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Attempted out of bounds 64 bit memory write to address 0x" + address.ToString("X2"));
            }
        }

        /// <summary>
        /// Reads a null-terminated ASCII string from the .data segment.
        /// </summary>
        /// <param name="address">The address in the .data segment to write to.</param>
        /// <param name="value">The value to write to the address.</param>
        public void WriteString(int address, string str)
        {
            try
            {
                int idx = address - m_RuntimeDataSegmentOffset;
                byte[] strBytes = Encoding.ASCII.GetBytes(str);
                CopyBytes(strBytes, idx);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Attempted out of bounds memory write to address 0x" + address.ToString("X2"));
            }
        }
        
        /// <summary>
        /// Copies a byte array into the main .data segment.
        /// </summary>
        /// <param name="bytes">The bytes to be copied into the data array.</param>
        /// <param name="offset">Where in the main .data segment the bytes shall be copied to.</param>
        private void CopyBytes(byte[] bytes, int offset)
        {
            for (int i = offset; i < bytes.Length; ++i)
            {
                m_ByteArray[i] = bytes[i - offset];
            }
        }

        private readonly byte[] m_ByteArray;
        private readonly IEnumerable<MetadataElement> m_Metadata;
        private readonly int m_RuntimeDataSegmentOffset;
        private readonly int m_StackStartAddress;
        private readonly int m_HeapStartAddress;
    }
}
