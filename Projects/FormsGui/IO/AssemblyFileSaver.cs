using Assembler.FormsGui.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.IO
{
   public class AssemblyFileSaver
   {
      public static bool DoesFileExist(string filePath)
      {
         return File.Exists(filePath);
      }

      public static void SaveFile(AssemblyFile file)
      {
         using (var fileStream = new FileStream(file.FilePath, FileMode.Create))
         {
            using (var writer = new StreamWriter(fileStream))
            {
               writer.Write(file.FileText);
            }
         }
      }
   }
}
