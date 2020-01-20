using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.OutputProcessing.TextOutput
{
   public interface IAssemblyFileWriter
   {
      void GenerateOutputFile(string outputFileName);
   }
}
