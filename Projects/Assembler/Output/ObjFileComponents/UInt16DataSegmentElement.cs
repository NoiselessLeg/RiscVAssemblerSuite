﻿using Assembler.Common;
using Assembler.Output.MetadataComponents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Output.ObjFileComponents
{
    /// <summary>
    /// Represents a UInt16 as a data element in a .obj file.
    /// </summary>
    public class UInt16DataElement : IObjectFileComponent
    {
        /// <summary>
        /// Creates an instance of the data element with the provided value.
        /// </summary>
        /// <param name="elem">The value of the element to store in the object file.</param>
        public UInt16DataElement(ushort elem)
        {
            m_Elem = elem;
            m_Metadata = new BasicMetadataComponent(ObjectTypeCode.Half);
        }

        /// <summary>
        /// Writes the bitwise representation of this object to the Stream.
        /// </summary>
        /// <param name="outputStream">The output Stream object to write to.</param>
        public void WriteDataToFile(Stream outputStream)
        {
            byte[] objBytes = ToByteArray(m_Elem);
            outputStream.Write(objBytes, 0, objBytes.Length);
        }

        /// <summary>
        /// Gets the size of the object file element, in bytes.
        /// </summary>
        public int Size { get { return sizeof(ushort); } }

        /// <summary>
        /// Gets the provided UInt16 as a byte array.
        /// </summary>
        /// <param name="param">The value to convert to bytes.</param>
        private static byte[] ToByteArray(ushort param)
        {
            byte[] byteRep = BitConverter.GetBytes(param);

            // if the architecture we're assembling on is not our desired endianness,
            // flip the byte array.
            if (!BitConverter.IsLittleEndian)
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

        private readonly ushort m_Elem;
        private readonly IMetadataComponent m_Metadata;
    }
}
