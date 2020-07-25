using Assembler.OutputProcessing.Utils;
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
      /// <param name="runtimeDataSegmentStart">The address that the .data segment will be simulated to be located during runtime.</param>
      /// <param name="segmentSize">The total size of the segment, in bytes.</param>
      public DataSegmentAccessor(IEnumerable<byte> data, int runtimeDataSegmentStart, int segmentSize)
      {
         m_ByteArray = data.ToArray();
         m_RuntimeDataSegmentOffset = runtimeDataSegmentStart;
         m_SegmentSize = segmentSize;
      }

      /// <summary>
      /// Gets the data segment as raw bytes in the target platform's endianness.
      /// </summary>
      public IEnumerable<byte> RawData
      {
         get { return m_ByteArray; }
      }

      /// <summary>
      /// Gets the total size of the data segment, in bytes.
      /// </summary>
      public int SegmentSize
      {
         get { return m_SegmentSize; }
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
      /// Reads a 32 bit single precision floating point value from the data segment.
      /// </summary>
      /// <param name="address">The address in the .data segment to retrieve the value from.</param>
      /// <returns>The 32-bit float value stored at the provided address.</returns>
      public float ReadSinglePrecisionFloat(int address)
      {
         return DataSegmentCommon.ReadSinglePrecisionFloat(m_ByteArray, address, m_RuntimeDataSegmentOffset);
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

      private readonly byte[] m_ByteArray;
      private readonly int m_RuntimeDataSegmentOffset;
      private readonly int m_SegmentSize;
   }
}
