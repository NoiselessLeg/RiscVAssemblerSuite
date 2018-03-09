using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Util
{
    static class StreamReaderUtils
    {
        public static void Seek(this StreamReader reader, long offset, SeekOrigin fromWhere)
        {
            reader.BaseStream.Seek(offset, fromWhere);
            reader.DiscardBufferedData();
        }
    }
}
