using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.Services
{
   public static class StreamService
   {
      public static string WriteToTemporaryFile(byte[] data)
      {
         string fileStr = Path.GetTempFileName();
         fileStr = fileStr.Substring(0, fileStr.LastIndexOf('.')) + ".xshd";

         using (var fs = new FileStream(fileStr, FileMode.Create))
         {
            fs.Write(data, 0, data.Length);
         }

         return fileStr;
      }
   }
}
