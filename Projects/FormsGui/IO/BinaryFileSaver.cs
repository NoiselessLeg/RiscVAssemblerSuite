using Assembler.FormsGui.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.IO
{
   public class BinaryFileSaver
   {
      public static bool DoesFileExist(string filePath)
      {
         return File.Exists(filePath);
      }

      public static void SaveFile(CompiledFile file)
      {
         using (var fileStream = new FileStream(file.FilePath, FileMode.OpenOrCreate))
         {
            using (var writer = new BinaryWriter(fileStream))
            {
               foreach (byte val in file.Bytes)
               {
                  writer.Write(val);
               }
            }
         }
      }
   }
}
