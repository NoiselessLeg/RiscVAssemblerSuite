using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.OutputProcessing.Utils
{
    /// <summary>
    /// Provides a common interface between the runtime and static DataAccessor classes.
    /// </summary>
    public class DataSegmentCommon
    {
        /// <summary>
        /// Reads a byte from the data segment.
        /// </summary>
        /// <param name="array">The byte array to read from.</param>
        /// <param name="address">The address in the .data segment to retrieve the byte from.</param>
        /// <param name="runtimeDataSegmentOffset">The starting runtime segment offset.</param>
        /// <returns>The byte stored at the provided address.</returns>
        public static sbyte ReadSignedByte(byte[] array, int address, int runtimeDataSegmentOffset)
        {
            try
            {
                int idx = address - runtimeDataSegmentOffset;
                return (sbyte)array[idx];
            }
            catch (Exception ex) when (ex is ArgumentOutOfRangeException || ex is IndexOutOfRangeException)
            {
                throw new AccessViolationException("Attempted out-of-bounds 8-bit memory read of address 0x" + address.ToString("X2"));
            }
        }

        /// <summary>
        /// Reads an unsigned byte from the data segment.
        /// </summary>
        /// <param name="array">The byte array to read from.</param>
        /// <param name="address">The address in the .data segment to retrieve the byte from.</param>
        /// <param name="runtimeDataSegmentOffset">The starting runtime segment offset.</param>
        /// <returns>The byte stored at the provided address.</returns>
        public static byte ReadUnsignedByte(byte[] array, int address, int runtimeDataSegmentOffset)
        {
            try
            {
                int idx = address - runtimeDataSegmentOffset;
                return array[idx];
            }
            catch (Exception ex) when (ex is ArgumentOutOfRangeException || ex is IndexOutOfRangeException)
            {
                throw new AccessViolationException("Attempted out-of-bounds 8-bit memory read of address 0x" + address.ToString("X2"));
            }
        }

        /// <summary>
        /// Reads a 16 bit signed integer from the data segment.
        /// </summary>
        /// <param name="array">The byte array to read from.</param>
        /// <param name="address">The address in the .data segment to retrieve the value from.</param>
        /// <param name="runtimeDataSegmentOffset">The starting runtime segment offset.</param>
        /// <returns>The 16-bit signed integer stored at the provided address.</returns>
        public static short ReadShort(byte[] array, int address, int runtimeDataSegmentOffset)
        {
            try
            {
                int idx = address - runtimeDataSegmentOffset;
                return BitConverter.ToInt16(array, idx);
            }
            catch (Exception ex) when (ex is ArgumentOutOfRangeException || ex is IndexOutOfRangeException)
            {
                throw new AccessViolationException("Attempted out-of-bounds 16-bit memory read of address 0x" + address.ToString("X2"));
            }
        }

        /// <summary>
        /// Reads a 16 bit unsigned integer from the data segment.
        /// </summary>
        /// <param name="array">The byte array to read from.</param>
        /// <param name="address">The address in the .data segment to retrieve the value from.</param>
        /// <param name="runtimeDataSegmentOffset">The starting runtime segment offset.</param>
        /// <returns>The 16-bit unsigned integer stored at the provided address.</returns>
        public static ushort ReadUnsignedShort(byte[] array, int address, int runtimeDataSegmentOffset)
        {
            try
            {
                int idx = address - runtimeDataSegmentOffset;
                return BitConverter.ToUInt16(array, idx);
            }
            catch (Exception ex) when (ex is ArgumentOutOfRangeException || ex is IndexOutOfRangeException)
            {
                throw new AccessViolationException("Attempted out-of-bounds 16-bit memory read of address 0x" + address.ToString("X2"));
            }
        }

        /// <summary>
        /// Reads a 32 bit signed integer from the data segment.
        /// </summary>
        /// <param name="array">The byte array to read from.</param>
        /// <param name="address">The address in the .data segment to retrieve the value from.</param>
        /// <param name="runtimeDataSegmentOffset">The starting runtime segment offset.</param>
        /// <returns>The 32-bit signed integer stored at the provided address.</returns>
        public static int ReadWord(byte[] array, int address, int runtimeDataSegmentOffset)
        {
            try
            {
                int idx = address - runtimeDataSegmentOffset;
                return BitConverter.ToInt32(array, idx);
            }
            catch (Exception ex) when (ex is ArgumentOutOfRangeException || ex is IndexOutOfRangeException)
            {
                throw new AccessViolationException("Attempted out-of-bounds 32-bit memory read of address 0x" + address.ToString("X2"));
            }
        }

        /// <summary>
        /// Reads a 32 bit unsigned integer from the data segment.
        /// </summary>
        /// <param name="array">The byte array to read from.</param>
        /// <param name="address">The address in the .data segment to retrieve the value from.</param>
        /// <param name="runtimeDataSegmentOffset">The starting runtime segment offset.</param>
        /// <returns>The 32-bit unsigned integer stored at the provided address.</returns>
        public static uint ReadUnsignedWord(byte[] array, int address, int runtimeDataSegmentOffset)
        {
            try
            {
                int idx = address - runtimeDataSegmentOffset;
                return BitConverter.ToUInt32(array, idx);
            }
            catch (Exception ex) when (ex is ArgumentOutOfRangeException || ex is IndexOutOfRangeException)
            {
                throw new AccessViolationException("Attempted out-of-bounds 32-bit memory read of address 0x" + address.ToString("X2"));
            }
        }

        /// <summary>
        /// Reads a 64 bit signed integer from the data segment.
        /// </summary>
        /// <param name="array">The byte array to read from.</param>
        /// <param name="address">The address in the .data segment to retrieve the value from.</param>
        /// <param name="runtimeDataSegmentOffset">The starting runtime segment offset.</param>
        /// <returns>The 64-bit signed integer stored at the provided address.</returns>
        public static long ReadLong(byte[] array, int address, int runtimeDataSegmentOffset)
        {
            try
            {
                int idx = address - runtimeDataSegmentOffset;
                return BitConverter.ToInt64(array, idx);
            }
            catch (Exception ex) when (ex is ArgumentOutOfRangeException || ex is IndexOutOfRangeException)
            {
                throw new AccessViolationException("Attempted out-of-bounds 64-bit memory read of address 0x" + address.ToString("X2"));
            }
        }

        /// <summary>
        /// Reads a 64 bit unsigned integer from the data segment.
        /// </summary>
        /// <param name="array">The byte array to read from.</param>
        /// <param name="address">The address in the .data segment to retrieve the value from.</param>
        /// <param name="runtimeDataSegmentOffset">The starting runtime segment offset.</param>
        /// <returns>The 64-bit unsigned integer stored at the provided address.</returns>
        public static ulong ReadUnsignedLong(byte[] array, int address, int runtimeDataSegmentOffset)
        {
            try
            {
                int idx = address - runtimeDataSegmentOffset;
                return BitConverter.ToUInt64(array, idx);
            }
            catch (Exception ex) when (ex is ArgumentOutOfRangeException || ex is IndexOutOfRangeException)
            {
                throw new AccessViolationException("Attempted out-of-bounds 64-bit memory read of address 0x" + address.ToString("X2"));
            }
        }

        /// <summary>
        /// Reads a null-terminated ASCII string from the .data segment.
        /// </summary>
        /// <param name="array">The byte array to read from.</param>
        /// <param name="address">The address in the .data segment to retrieve the string from.</param>
        /// <param name="runtimeDataSegmentOffset">The starting runtime segment offset.</param>
        /// <returns>A string encoded in the ASCII encoding.</returns>
        public static string ReadString(byte[] array, int address, int runtimeDataSegmentOffset)
        {
            try
            {
                int idx = address - runtimeDataSegmentOffset;
                int strSize = 0;
                int itr = idx;

                // go until we find a null terminator.
                while (array[itr] != 0)
                {
                    ++strSize;
                    ++itr;
                }

                return Encoding.ASCII.GetString(array, idx, strSize);
            }
            catch (Exception ex) when (ex is ArgumentOutOfRangeException || ex is IndexOutOfRangeException)
            {
                throw new Exception("Attempted out of bounds string read from memory at address 0x" + address.ToString("X2"));
            }
        }
    }
}
