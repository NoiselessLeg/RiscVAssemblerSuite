using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.OutputProcessing.ParamDecoding
{
    class JalDecoder : IParameterDecoder
    {
        public IEnumerable<int> DecodeParameters(int instruction)
        {
            var paramList = new List<int>();
            int regParam = (instruction & 0xF80);
            paramList.Add(regParam);

            int immParam = 0;

            // shift the offset 20 bit into position
            immParam |= ((int)(instruction & 0x80000000)) >> 12;

            //shift the 10 - 1 offset bits into position
            immParam |= (instruction & 0x7FE00000) >> 20;

            // shift the 11 offset bit into position
            immParam |= (instruction & 0x100000) >> 9;

            // shift the 19 - 12 offset bits into position.
            immParam |= (instruction & 0xFF000) >> 1;
            paramList.Add(immParam);
            
            return paramList;
        }
    }
}