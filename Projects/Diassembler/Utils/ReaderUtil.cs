using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Disassembler.Utils
{
    static class ReaderUtil
    {
        /// <summary>
        /// Determines if the reader has reached the end of its underlying stream.
        /// </summary>
        /// <param name="reader">The reader to check.</param>
        /// <returns>True if the position of the stream is greater than or equal to the length of the stream.</returns>
        public static bool EndOfStream(this BinaryReader reader)
        {
            return reader.BaseStream.Position >= reader.BaseStream.Length;
        }

        public static void ReadUntil(this BinaryReader reader, byte[] buffer)
        {
            if (buffer.Length == 0)
            {
                throw new ArgumentException("Buffer must have at least one element inside it.");
            }

            while (!reader.EndOfStream())
            {
                int val = reader.PeekChar();

                // if the peeked character 
                if (val != buffer[0])
                {

                }
            }
        }

        public static void ReadUntil(this BinaryReader reader, string str)
        {
            reader.ReadUntil(Encoding.ASCII.GetBytes(str));
        }
    }
}
