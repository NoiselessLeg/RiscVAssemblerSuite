using Assembler.Common;
using System.IO;

namespace Assembler.Output.ObjFileComponents
{
    /// <summary>
    /// Represents a byte as a data element in a .obj file.
    /// </summary>
    public class ByteDataElement : IObjectFileComponent
    {
        /// <summary>
        /// Creates an instance of the data element with the provided value.
        /// </summary>
        /// <param name="elem">The value of the element to store in the object file.</param>
        public ByteDataElement(byte elem)
        {
            m_Elem = elem;
            m_Metadata = new Metadata(ObjectTypeCode.Byte, 1);
        }

        /// <summary>
        /// Gets the size of the object file element, in bytes.
        /// </summary>
        public int Size { get { return sizeof(byte); } }

        /// <summary>
        /// Writes the bitwise representation of this object to the Stream.
        /// </summary>
        /// <param name="outputStream">The output Stream object to write to.</param>
        public void WriteDataToFile(Stream outputStream)
        {
            outputStream.Write(new[] { m_Elem }, 0, 1);
        }

        /// <summary>
        /// Writes metadata about this object instance to a Stream.
        /// </summary>
        /// <param name="outputStream">The output Stream object to write to.</param>
        public void WriteMetadataToFile(Stream outputStream)
        {
            m_Metadata.WriteToStream(outputStream);
        }

        private readonly byte m_Elem;
        private readonly Metadata m_Metadata;
    }
}
