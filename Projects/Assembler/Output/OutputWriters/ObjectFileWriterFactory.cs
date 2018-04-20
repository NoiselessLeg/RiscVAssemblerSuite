using Assembler.Common;
using System;
using System.Collections.Generic;

namespace Assembler.Output.OutputWriters
{
    /// <summary>
    /// Provides an easy way to access file writers for supported object formats.
    /// </summary>
    class ObjectFileWriterFactory
    {
        /// <summary>
        /// Creates a new instance of the ObjectFileWriterFactory with the target endianness.
        /// </summary>
        /// <param name="targetEndianness">The endianness that should be used to generate output.</param>
        public ObjectFileWriterFactory()
        {
            m_WriterTypes = new Dictionary<OutputTypes, IObjectFileWriter>()
            {
                { OutputTypes.DirectBinary, new BasicBinaryObjectWriter() }
            };
        }

        /// <summary>
        /// Fetches the appropriate object file writer for the specified object type.
        /// </summary>
        /// <param name="outType">The type of file to output.</param>
        /// <returns>A data writer for that file type.</returns>
        public IObjectFileWriter GetWriterForObjectType(OutputTypes outType)
        {
            IObjectFileWriter writer = default(IObjectFileWriter);
            if (!m_WriterTypes.TryGetValue(outType, out writer))
            {
                throw new ArgumentException("Unsupported output type passed to GetWriterForObjectType");
            }

            return writer;
        }

        private readonly Dictionary<OutputTypes, IObjectFileWriter> m_WriterTypes;
    }
}
