using Assembler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.OutputProcessing.FileReaders
{
   public interface ICompiledFileReader
   {
      DisassembledFileBase ParseFile(string fileName, ILogger logger);
   }
}
