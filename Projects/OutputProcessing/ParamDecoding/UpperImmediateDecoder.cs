using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.OutputProcessing.ParamDecoding
{
    class UpperImmediateDecoder : IParameterDecoder
    {
        public IEnumerable<int> DecodeParameters(int instruction)
        {
            var paramList = new List<int>();
            int regParam = (instruction & 0xF80);
            paramList.Add(regParam);

            int immParam = ((int)(instruction & 0xFFFFF000)) >> 12;
            paramList.Add(immParam);

            return paramList;
        }
    }
}
