using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Output.OutputWriters
{
   class ElfObjectWriter : IObjectFileWriter
   {
      public void WriteObjectFile(string fileName, BasicObjectFile file)
      {
         using (FileStream fs = File.Open(fileName, FileMode.Create))
         {
            using (MemoryStream tmpStrm = new MemoryStream())
            {

            }
         }
      }

      private void WriteELF_Header(Stream strm, BasicObjectFile file)
      {
         byte[] ELF_HEADER = new byte[]{ 0x7F, 0x45, 0x4C, 0x46 };
         strm.Write(ELF_HEADER, 0, ELF_HEADER.Length);

         // indicates 32-bit and little endian format.
         byte[] CLASS_AND_DATA_FIELD = new byte[] { 1, 1 };

         strm.Write(CLASS_AND_DATA_FIELD, 0, CLASS_AND_DATA_FIELD.Length);

         // write the target platform (according to the 'pedia, this is often set to 0 regardless of platform)
         strm.Write(new byte[] { 0 }, 0, 1);


      }
   }
}
