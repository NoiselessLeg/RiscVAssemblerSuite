using Assembler.FormsGui.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.IO
{
   public class AssemblyFileLoader
   {
      public static AssemblyFile LoadFile(string filePath)
      {
         var file = new AssemblyFile();
         file.FilePath = filePath;

         using (var readStream = new FileStream(filePath, FileMode.Open))
         {
            using (var reader = new StreamReader(readStream))
            {
               string rawFileData = reader.ReadToEnd();
               file.FileText = rawFileData;

               // text box expects this to be a specific new line, let's give it what it wants.
               //file.FileText = rawFileData.Replace("\n", Environment.NewLine);
            }
         }

         return file;
      }
   }
}
