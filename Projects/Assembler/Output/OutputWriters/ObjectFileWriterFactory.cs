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
      /// Gets the appropriate file output format based on the file extension
      /// of the output file.
      /// </summary>
      /// <param name="filePath">The file path or name of the output file.</param>
      /// <returns>An appropriate instance of IObjectFileWriter for that particular
      /// file extension.</returns>
      public IObjectFileWriter GetWriterForOutputFile(string filePath)
      {
         string fileExtension = filePath.Substring(filePath.LastIndexOf('.'));

         IObjectFileWriter writer = default(IObjectFileWriter);
         if (fileExtension == ".jef")
         {
            writer = GetWriterForObjectType(OutputTypes.DirectBinary);
         }
         else
         {
            writer = GetWriterForObjectType(OutputTypes.ELF);
         }

         return writer;
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
