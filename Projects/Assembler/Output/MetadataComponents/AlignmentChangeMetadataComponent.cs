using Assembler.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Output.MetadataComponents
{
   class AlignmentChangeMetadataComponent : BasicMetadataComponent
   {
      public AlignmentChangeMetadataComponent(ObjectTypeCode typeCode, int newAlignment):
         base(typeCode)
      {
         m_Alignment = newAlignment;
      }

      public override void WriteToStream(Stream str)
      {
         base.WriteToStream(str);
         byte[] alignmentAsBytes = BitConverter.GetBytes(m_Alignment);
         // if the architecture we're assembling on is not our desired endianness,
         // flip the byte array.
         if (!BitConverter.IsLittleEndian)
         {
            Array.Reverse(alignmentAsBytes);
         }

         str.Write(alignmentAsBytes, 0, alignmentAsBytes.Length);
      }

      private readonly int m_Alignment;
   }
}
