using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Output.MetadataComponents
{
   public interface IMetadataComponent
   {
      /// <summary>
      /// Writes the provided typecode and size to a stream.
      /// </summary>
      /// <param name="str">The Stream instance to write to.</param>
      void WriteToStream(Stream str);
   }
}
