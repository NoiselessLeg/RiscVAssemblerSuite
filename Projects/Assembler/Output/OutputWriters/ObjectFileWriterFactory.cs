using Assembler.Common;
using System;
using System.Collections.Generic;

namespace Assembler.Output.OutputWriters
{
   /// <summary>
   /// Provides an easy way to access file writers for supported object formats.
   /// </summary>
   internal class ObjectFileWriterFactory
   {
      /// <summary>
      /// Creates a new instance of the ObjectFileWriterFactory with the target endianness.
      /// </summary>
      public ObjectFileWriterFactory()
      {
         m_WriterTypes = new Dictionary<OutputTypes, IObjectFileWriter>()
         {
            { OutputTypes.DirectBinary, new BasicBinaryObjectWriter() },
            { OutputTypes.ELF, new ElfObjectWriter() }
         };
      }

      /// <summary>
      /// Fetches the appropriate object file writer for the specified object type.
      /// </summary>
      /// <param name="outType">The type of file to output.</param>
      /// <returns>A data writer for that file type.</returns>
      public IObjectFileWriter GetWriterForObjectType(OutputTypes outType)
      {
         if (!m_WriterTypes.TryGetValue(outType, out IObjectFileWriter writer))
         {
            throw new ArgumentException("Unsupported output type passed to GetWriterForObjectType");
         }

         return writer;
      }

      private readonly Dictionary<OutputTypes, IObjectFileWriter> m_WriterTypes;
   }
}
