using Assembler.Common;
using System;
using System.IO;

namespace Assembler.Output
{

    /// <summary>
    /// Struct that describes an element of the .data or .extern segments
    /// </summary>
    class Metadata
    {
        /// <summary>
        /// Creates an instance of the Metadata struct with the specified typecode and size.
        /// </summary>
        /// <param name="typeCode">The TypeCode representing this data type.</param>
        /// <param name="size">The size of this data element, in bytes.</param>
        public Metadata(ObjectTypeCode typeCode, int size)
        {
            m_TypeCode = typeCode;
            m_Size = size;
        }

        /// <summary>
        /// Writes the provided typecode and size to a stream.
        /// </summary>
        /// <param name="str">The Stream instance to write to.</param>
        public void WriteToStream(Stream str)
        {
            byte byteTypeCode = (byte)m_TypeCode;
            str.WriteByte(byteTypeCode);

            // if the typecode is that of a string, output the 4-byte size as well.
            // otherwise, just output the typecode.
            if (m_TypeCode == ObjectTypeCode.String)
            {
                byte[] sizeBytes = BitConverter.GetBytes(m_Size);
                // if the architecture we're assembling on is not our desired endianness,
                // flip the byte array.
                if (!BitConverter.IsLittleEndian)
                {
                    Array.Reverse(sizeBytes);
                }

                str.Write(sizeBytes, 0, sizeBytes.Length);
            }
            
        }
        
        private readonly ObjectTypeCode m_TypeCode;
        private readonly int m_Size;
    }
}
