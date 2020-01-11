using Assembler.Common;
using Assembler.Output.MetadataComponents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Output.ObjFileComponents
{
   public class AlignmentChangeDataSegmentElement : IObjectFileComponent
   {
      /// <summary>
      /// Creates an instance of the data element with the provided value.
      /// </summary>
      /// <param name="elem">The value of the element to store in the object file.</param>
      public AlignmentChangeDataSegmentElement(int newAlignment)
      {
         m_Metadata = new AlignmentChangeMetadataComponent(ObjectTypeCode.AlignmentChange, newAlignment);
      }

      /// <summary>
      /// Gets the size of the object file element, in bytes.
      /// </summary>
      public virtual int Size { get { return 0; } }

      /// <summary>
      /// Writes the bitwise representation of this object to the Stream.
      /// </summary>
      /// <param name="outputStream">The output Stream object to write to.</param>
      public virtual void WriteDataToFile(Stream outputStream)
      {
         // do nothing, there is no data associated with an alignment change.
         // this is primarily so when disassembling we know to change our alignment.
      }

      /// <summary>
      /// Writes metadata about this object instance to a Stream.
      /// </summary>
      /// <param name="outputStream">The output Stream object to write to.</param>
      public virtual void WriteMetadataToFile(Stream outputStream)
      {
         m_Metadata.WriteToStream(outputStream);
      }
      
      private readonly IMetadataComponent m_Metadata;
   }
}
