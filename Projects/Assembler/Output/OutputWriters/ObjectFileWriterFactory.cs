using Assembler.Common;
using System;
using System.Collections.Generic;

namespace Assembler.Output.OutputWriters
{
    /// <summary>
    /// Provides an easy way to access file writers for supported object formats.
    /// </summary>
    static class ObjectFileWriterFactory
    {
        static ObjectFileWriterFactory()
        {
            s_WriterTypes = new Dictionary<OutputTypes, IObjectFileWriter>()
            {
                { OutputTypes.DirectBinary, new BasicBinaryObjectWriter() }
            };
        }

        /// <summary>
        /// Fetches the appropriate object file writer for the specified object type.
        /// </summary>
        /// <param name="outType">The type of file to output.</param>
        /// <returns>A data writer for that file type.</returns>
        public static IObjectFileWriter GetWriterForObjectType(OutputTypes outType)
        {
            IObjectFileWriter writer = default(IObjectFileWriter);
            if (!s_WriterTypes.TryGetValue(outType, out writer))
            {
                throw new ArgumentException("Unsupported output type passed to GetWriterForObjectType");
            }

            return writer;
        }

        private static readonly Dictionary<OutputTypes, IObjectFileWriter> s_WriterTypes;
    }
}
