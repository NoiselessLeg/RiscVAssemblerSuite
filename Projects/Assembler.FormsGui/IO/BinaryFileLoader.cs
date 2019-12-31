using Assembler.FormsGui.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.IO
{
   public class BinaryFileLoader
   {
      public static CompiledFile LoadFile(string filePath)
      {
         var file = new CompiledFile();
         file.FilePath = filePath;

         using (var readStream = new FileStream(filePath, FileMode.Open))
         {
            using (var reader = new BinaryReader(readStream))
            {
               while (reader.BaseStream.Position != reader.BaseStream.Length)
               {
                  file.Bytes.Add(reader.ReadByte());
               }
            }
         }

         return file;
      }
   }
}