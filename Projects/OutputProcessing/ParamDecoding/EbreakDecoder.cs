using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.OutputProcessing.ParamDecoding
{
   class EbreakDecoder : IParameterDecoder
   {
      public IEnumerable<int> DecodeParameters(int instruction)
      {
         return new List<int>();
      }
   }
}
