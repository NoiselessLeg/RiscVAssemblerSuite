using Assembler.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Output.ObjFileComponents
{
    class AsciizDataSegmentElement : AsciiDataSegmentElement
    {
        /// <summary>
        /// Creates an instance of the data element with the provided value.
        /// </summary>
        /// <param name="elem">The value of the element to store in the object file.</param>
        /// <param name="targetEndianness">The target output endianness.</param>
        public AsciizDataSegmentElement(string str):
            base(str)
        {
            m_Metadata = new Metadata(ObjectTypeCode.String, str.Length + 1);
        }

        /// <summary>
        /// Gets the size of the object file element, in bytes.
        /// </summary>
        public override int Size { get { return base.Size + 1; } }

        /// <summary>
        /// Writes the bitwise representation of this object to the Stream.
        /// </summary>
        /// <param name="outputStream">The output Stream object to write to.</param>
        public override void WriteDataToFile(Stream outputStream)
        {
            // write the string, then null terminate it.
            base.WriteDataToFile(outputStream);
            outputStream.Write(new[] { (byte) 0 }, 0, 1);
        }

        /// <summary>
        /// Writes metadata about this object instance to a Stream.
        /// </summary>
        /// <param name="outputStream">The output Stream object to write to.</param>
        public override void WriteMetadataToFile(Stream outputStream)
        {
            m_Metadata.WriteToStream(outputStream);
        }

        private readonly Metadata m_Metadata;
    }
}
