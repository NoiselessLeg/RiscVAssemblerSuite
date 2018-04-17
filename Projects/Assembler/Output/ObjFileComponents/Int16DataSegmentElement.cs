﻿using Assembler.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Output.ObjFileComponents
{
    /// <summary>
    /// Represents a Int16 as a data element in a .obj file.
    /// </summary>
    public class Int16DataElement : IObjectFileComponent
    {
        /// <summary>
        /// Creates an instance of the data element with the provided value.
        /// </summary>
        /// <param name="elem">The value of the element to store in the object file.</param>
        /// <param name="targetEndianness">The target output endianness.</param>
        public Int16DataElement(short elem, Endianness targetEndianness)
        {
            m_Elem = elem;
            m_TargetEndianness = targetEndianness;
            m_Metadata = new Metadata(ObjectTypeCode.Half, sizeof(short), targetEndianness);
        }

        /// <summary>
        /// Writes the bitwise representation of this object to the Stream.
        /// </summary>
        /// <param name="outputStream">The output Stream object to write to.</param>
        public void WriteDataToFile(Stream outputStream)
        {
            byte[] objBytes = ToByteArray(m_Elem, m_TargetEndianness);
            outputStream.Write(objBytes, 0, objBytes.Length);
        }

        /// <summary>
        /// Gets the size of the object file element, in bytes.
        /// </summary>
        public int Size { get { return sizeof(short); } }

        /// <summary>
        /// Gets the provided Int16 as a byte array.
        /// </summary>
        /// <param name="param">The value to convert to bytes.</param>
        private static byte[] ToByteArray(short param, Endianness targetEndianness)
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

        /// <summary>
        /// Writes metadata about this object instance to a Stream.
        /// </summary>
        /// <param name="outputStream">The output Stream object to write to.</param>
        public void WriteMetadataToFile(Stream outputStream)
        {
            m_Metadata.WriteToStream(outputStream);
        }

        private readonly short m_Elem;
        private readonly Endianness m_TargetEndianness;
        private readonly Metadata m_Metadata;
    }
}
