using Assembler.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Output.MetadataComponents
{
   class StringMetadataComponent : BasicMetadataComponent
   {
      /// <summary>
      /// Creates an instance of the StringMetadataComponent struct with the specified typecode and size.
      /// </summary>
      /// <param name="typeCode">The TypeCode representing this data type.</param>
      /// <param name="size">The size of this data element, in bytes.</param>
      public StringMetadataComponent(ObjectTypeCode typeCode, int size) :
         base(typeCode)
      {
         m_Size = size;
      }

      /// <summary>
      /// Writes the provided typecode and size to a stream.
      /// </summary>
      /// <param name="str">The Stream instance to write to.</param>
      public override void WriteToStream(Stream str)
      {
         base.WriteToStream(str);
         byte[] sizeBytes = BitConverter.GetBytes(m_Size);
         // if the architecture we're assembling on is not our desired endianness,
         // flip the byte array.
         if (!BitConverter.IsLittleEndian)
         {
            Array.Reverse(sizeBytes);
         }

         str.Write(sizeBytes, 0, sizeBytes.Length);
      }
      
      private readonly int m_Size;
   }
}
