using Assembler.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.OutputProcessing.Utils
{
    /// <summary>
    /// Reads primitive data types as binary values in a specified encoding with a specified endianness.
    /// </summary>
    class EndianReader : BinaryReader
    {
        /// <summary>
        /// Creates an instance of the EndianReader from the input Stream, and a specified
        /// input endianness.
        /// </summary>
        /// <param name="input">The input Stream to read from.</param>
        /// <param name="inputEndianness">The endianness of the input.</param>
        public EndianReader(Stream input, Endianness inputEndianness) 
            : base(input)
        {
            m_EndiannessSwapRequired = RequiresEndiannessSwap(inputEndianness);
        }

        /// <summary>
        /// Creates an instance of the EndianReader from the input Stream, Encoding, and a specified
        /// input endianness.
        /// </summary>
        /// <param name="input">The input Stream to read from.</param>
        /// <param name="encoding">The Stream's encoding</param>
        /// <param name="inputEndianness">The endianness of the input.</param>
        public EndianReader(Stream input, Encoding encoding, Endianness inputEndianness) 
            : base(input, encoding)
        {
            m_EndiannessSwapRequired = RequiresEndiannessSwap(inputEndianness);
        }

        /// <summary>
        /// Creates an instance of the EndianReader from the input Stream, Encoding, and a specified
        /// input endianness. Can be configured to be left open after the Stream is disposed.
        /// </summary>
        /// <param name="input">The input Stream to read from.</param>
        /// <param name="encoding">The Stream's encoding</param>
        /// <param name="leaveOpen">If true, the Stream object will not be have its Dispose method called after this
        /// reader instance is disposed of. Otherwise, this reader will dispose of the underlying stream.</param>
        /// <param name="inputEndianness">The endianness of the input.</param>
        public EndianReader(Stream input, Encoding encoding, bool leaveOpen, Endianness inputEndianness) 
            : base(input, encoding, leaveOpen)
        {
            m_EndiannessSwapRequired = RequiresEndiannessSwap(inputEndianness);
        }

        /// <summary>
        /// Reads a 16 bit signed integer from the Stream, and advances the current Stream position
        /// by two bytes.
        /// </summary>
        /// <returns>A 16 bit signed integer.</returns>
        public override short ReadInt16()
        {
            short ret = base.ReadInt16();
            if (m_EndiannessSwapRequired)
            {
                byte[] bytes = BitConverter.GetBytes(ret);
                Array.Reverse(bytes);
                ret = BitConverter.ToInt16(bytes, 0);
            }

            return ret;
        }

        /// <summary>
        /// Reads a 32 bit signed integer from the Stream, and advances the current Stream position
        /// by four bytes.
        /// </summary>
        /// <returns>A 32 bit signed integer.</returns>
        public override int ReadInt32()
        {
            int ret = base.ReadInt32();
            if (m_EndiannessSwapRequired)
            {
                byte[] bytes = BitConverter.GetBytes(ret);
                Array.Reverse(bytes);
                ret = BitConverter.ToInt32(bytes, 0);
            }

            return ret;
        }

        /// <summary>
        /// Reads a 64 bit signed integer from the Stream, and advances the current Stream position
        /// by eight bytes.
        /// </summary>
        /// <returns>A 64 bit signed integer.</returns>
        public override long ReadInt64()
        {
            long ret = base.ReadInt64();
            if (m_EndiannessSwapRequired)
            {
                byte[] bytes = BitConverter.GetBytes(ret);
                Array.Reverse(bytes);
                ret = BitConverter.ToInt64(bytes, 0);
            }

            return ret;
        }

        /// <summary>
        /// Reads an 16 bit unsigned integer from the Stream, and advances the current Stream position
        /// by two bytes.
        /// </summary>
        /// <returns>A 16 bit unsigned integer.</returns>
        public override ushort ReadUInt16()
        {
            ushort ret = base.ReadUInt16();
            if (m_EndiannessSwapRequired)
            {
                byte[] bytes = BitConverter.GetBytes(ret);
                Array.Reverse(bytes);
                ret = BitConverter.ToUInt16(bytes, 0);
            }

            return ret;
        }

        /// <summary>
        /// Reads a 32 bit unsigned integer from the Stream, and advances the current Stream position
        /// by four bytes.
        /// </summary>
        /// <returns>A 32 bit unsigned integer.</returns>
        public override uint ReadUInt32()
        {
            uint ret = base.ReadUInt32();
            if (m_EndiannessSwapRequired)
            {
                byte[] bytes = BitConverter.GetBytes(ret);
                Array.Reverse(bytes);
                ret = BitConverter.ToUInt32(bytes, 0);
            }

            return ret;
        }

        /// <summary>
        /// Reads a 64 bit unsigned integer from the Stream, and advances the current Stream position
        /// by eight bytes.
        /// </summary>
        /// <returns>A 64 bit unsigned integer.</returns>
        public override ulong ReadUInt64()
        {
            ulong ret = base.ReadUInt64();
            if (m_EndiannessSwapRequired)
            {
                byte[] bytes = BitConverter.GetBytes(ret);
                Array.Reverse(bytes);
                ret = BitConverter.ToUInt64(bytes, 0);
            }

            return ret;
        }

        /// <summary>
        /// Determines if any future reads of longer than one byte will require a byteswap
        /// to be correctly represented on the runtime architecture, based on the binary input's endianness.
        /// </summary>
        /// <param name="inputEndianness">The endianness of the stream input</param>
        /// <returns>True if a byteswap will be required to correctly represent a value.</returns>
        private static bool RequiresEndiannessSwap(Endianness inputEndianness)
        {
            return  BitConverter.IsLittleEndian && inputEndianness == Endianness.BigEndian ||
                    !BitConverter.IsLittleEndian && inputEndianness == Endianness.LittleEndian;
        }

        private readonly bool m_EndiannessSwapRequired;
    }
}
