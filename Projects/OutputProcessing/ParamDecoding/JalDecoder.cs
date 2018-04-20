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
            int regParam = (instruction & 0xF80) >> 7;
            paramList.Add(regParam);

            int immParam = 0;

            // shift bit 20 into position
            int temp = ((int)(instruction & 0x80000000)) >> 31;
            immParam |= (temp << 20);

            // shift bits 10 - 1 into position
            temp = (instruction & 0x7FE00000) >> 21;
            immParam |= (temp << 1);

            // shift bit 11 into position
            temp = (instruction & 0x100000) >> 20;
            immParam |= (temp << 11);

            // shift bits 19-12 into position
            temp = (instruction & 0xFF000) >> 12;
            immParam |= (temp << 12);
            paramList.Add(immParam);
            
            return paramList;
        }
    }
}