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
         byte[] dataBytes;

         // "trick" our object file into writing data into our arrays.
         using (var strm = new MemoryStream())
         {
            foreach (var dataElement in file.DataElements)
            {
               dataElement.WriteDataToFile(strm);
            }

            dataBytes = strm.ToArray();
         }

         byte[] textBytes;
         using (var strm = new MemoryStream())
         {
            foreach (var textElement in file.TextElements)
            {
               textElement.WriteDataToFile(strm);
            }

            textBytes = strm.ToArray();
         }

         var underlyingWriter = new ELF_Wrapper.ELF_Writer();
         underlyingWriter.AddDataSection(dataBytes, Common.CommonConstants.BASE_DATA_ADDRESS);
         underlyingWriter.AddTextSection(textBytes, Common.CommonConstants.BASE_TEXT_ADDRESS);
         underlyingWriter.AddSymbolTable(file.SymbolTable);
         underlyingWriter.WriteFile(fileName);
      }
   }
}
