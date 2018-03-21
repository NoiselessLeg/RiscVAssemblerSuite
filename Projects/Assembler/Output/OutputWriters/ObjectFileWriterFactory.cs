using Assembler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Output.OutputWriters
{
    static class ObjectFileWriterFactory
    {
        static ObjectFileWriterFactory()
        {
            s_WriterTypes = new Dictionary<OutputTypes, IObjectFileWriter>()
            {
                { OutputTypes.DirectBinary, new BasicBinaryObjectWriter() }
            };
        }

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
