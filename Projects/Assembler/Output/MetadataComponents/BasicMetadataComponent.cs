using Assembler.Common;
using System;
using System.IO;

namespace Assembler.Output.MetadataComponents
{

    /// <summary>
    /// Struct that describes an element of the .data or .extern segments
    /// </summary>
    class BasicMetadataComponent : IMetadataComponent
    {
        /// <summary>
        /// Creates an instance of the BasicMetadataComponent struct with the specified typecode.
        /// </summary>
        /// <param name="typeCode">The TypeCode representing this data type.</param>
        public BasicMetadataComponent(ObjectTypeCode typeCode)
        {
            m_TypeCode = typeCode;
        }

        /// <summary>
        /// Writes the provided typecode to a stream.
        /// </summary>
        /// <param name="str">The Stream instance to write to.</param>
        public virtual void WriteToStream(Stream str)
        {
            byte byteTypeCode = (byte)m_TypeCode;
            str.WriteByte(byteTypeCode);
        }
        
        private readonly ObjectTypeCode m_TypeCode;
    }
}
