using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Common
{
   public static class StreamReaderUtil
   {
      public static string ReadLineAt(this StreamReader reader, int lineNum)
      {
         long storedPos = reader.BaseStream.Position;
         reader.BaseStream.Position = 0;
         reader.DiscardBufferedData();
         int currLine = 1;
         while (currLine < lineNum && !reader.EndOfStream)
         {
            reader.ReadLine();
            ++currLine;
         }

         if (reader.EndOfStream)
         {
            reader.BaseStream.Position = storedPos;
            reader.DiscardBufferedData();
            throw new InvalidOperationException("Line number was out of range of file.");
         }

         string retStr = reader.ReadLine();
         reader.BaseStream.Position = storedPos;
         reader.DiscardBufferedData();

         // reset the stream position once we're done.
         return retStr;
      }
   }
}
