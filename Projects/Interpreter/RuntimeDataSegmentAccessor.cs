using Assembler.Common;
using Assembler.OutputProcessing;
using Assembler.OutputProcessing.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Interpreter
{
    class RuntimeDataSegmentAccessor
    {
        public RuntimeDataSegmentAccessor(DataSegmentAccessor segmentAccessor)
        {
            m_ByteArray = new byte[CommonConstants.MAX_SEGMENT_SIZE];
            m_StackBytes = new byte[CommonConstants.MAX_SEGMENT_SIZE];
            m_HeapBytes = new byte[0];

            m_RuntimeDataSegmentOffset = segmentAccessor.BaseRuntimeDataAddress;
            Array.Copy(segmentAccessor.RawData.ToArray(), m_ByteArray, segmentAccessor.RawData.Count());
        }
        
        /// <summary>
        /// Reads a byte from the data segment.
        /// </summary>
        /// <param name="address">The address in the .data segment to retrieve the byte from.</param>
        /// <returns>The byte stored at the provided address.</returns>
        public sbyte ReadSignedByte(int address)
        {
            return DataSegmentCommon.ReadSignedByte(m_ByteArray, address, m_RuntimeDataSegmentOffset);
        }

        /// <summary>
        /// Reads an unsigned byte from the data segment.
        /// </summary>
        /// <param name="address">The address in the .data segment to retrieve the byte from.</param>
        /// <returns>The byte stored at the provided address.</returns>
        public byte ReadUnsignedByte(int address)
        {
            return DataSegmentCommon.ReadUnsignedByte(m_ByteArray, address, m_RuntimeDataSegmentOffset);
        }

        /// <summary>
        /// Reads a 16 bit signed integer from the data segment.
        /// </summary>
        /// <param name="address">The address in the .data segment to retrieve the value from.</param>
        /// <returns>The 16-bit signed integer stored at the provided address.</returns>
        public short ReadShort(int address)
        {
            return DataSegmentCommon.ReadShort(m_ByteArray, address, m_RuntimeDataSegmentOffset);
        }

        /// <summary>
        /// Reads a 16 bit unsigned integer from the data segment.
        /// </summary>
        /// <param name="address">The address in the .data segment to retrieve the value from.</param>
        /// <returns>The 16-bit unsigned integer stored at the provided address.</returns>
        public ushort ReadUnsignedShort(int address)
        {
            return DataSegmentCommon.ReadUnsignedShort(m_ByteArray, address, m_RuntimeDataSegmentOffset);
        }

        /// <summary>
        /// Reads a 32 bit signed integer from the data segment.
        /// </summary>
        /// <param name="address">The address in the .data segment to retrieve the value from.</param>
        /// <returns>The 32-bit signed integer stored at the provided address.</returns>
        public int ReadWord(int address)
        {
            return DataSegmentCommon.ReadWord(m_ByteArray, address, m_RuntimeDataSegmentOffset);
        }

        /// <summary>
        /// Reads a 32 bit unsigned integer from the data segment.
        /// </summary>
        /// <param name="address">The address in the .data segment to retrieve the value from.</param>
        /// <returns>The 32-bit unsigned integer stored at the provided address.</returns>
        public uint ReadUnsignedWord(int address)
        {
            return DataSegmentCommon.ReadUnsignedWord(m_ByteArray, address, m_RuntimeDataSegmentOffset);
        }

        /// <summary>
        /// Reads a 64 bit signed integer from the data segment.
        /// </summary>
        /// <param name="address">The address in the .data segment to retrieve the value from.</param>
        /// <returns>The 64-bit signed integer stored at the provided address.</returns>
        public long ReadLong(int address)
        {
            return DataSegmentCommon.ReadLong(m_ByteArray, address, m_RuntimeDataSegmentOffset);
        }

        /// <summary>
        /// Reads a 64 bit unsigned integer from the data segment.
        /// </summary>
        /// <param name="address">The address in the .data segment to retrieve the value from.</param>
        /// <returns>The 64-bit unsigned integer stored at the provided address.</returns>
        public ulong ReadUnsignedLong(int address)
        {
            return DataSegmentCommon.ReadUnsignedLong(m_ByteArray, address, m_RuntimeDataSegmentOffset);
        }

        /// <summary>
        /// Reads a null-terminated ASCII string from the .data segment.
        /// </summary>
        /// <param name="address">The address to retrieve the string from.</param>
        /// <returns>A string encoded in the ASCII encoding.</returns>
        public string ReadString(int address)
        {
            return DataSegmentCommon.ReadString(m_ByteArray, address, m_RuntimeDataSegmentOffset);
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
            for (int i = offset; i < offset + bytes.Length; ++i)
            {
                m_ByteArray[i] = bytes[i - offset];
            }
        }

        /// <summary>
        /// Simulates an "sbrk" system call by shifting the heap size around
        /// </summary>
        /// <param name="numBytes">The number of bytes to move the program break up or down by.</param>
        void Sbrk(int numBytes)
        {
            byte[] temp = new byte[m_HeapBytes.Length + numBytes];
            Array.Copy(m_HeapBytes, temp, Math.Min(m_HeapBytes.Length, temp.Length));
            m_HeapBytes = temp;
        }
        
        private readonly int m_RuntimeDataSegmentOffset;
        private readonly byte[] m_ByteArray;
        private readonly byte[] m_StackBytes;
        private byte[] m_HeapBytes;
    }
}
