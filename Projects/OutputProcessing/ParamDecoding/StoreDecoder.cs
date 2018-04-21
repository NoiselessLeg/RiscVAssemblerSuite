using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.OutputProcessing.ParamDecoding
{
    class StoreDecoder : IParameterDecoder
    {
        public IEnumerable<int> DecodeParameters(int instruction)
        {
            var paramList = new List<int>();

            int rs2 = (instruction & 0x1F00000) >> 20;
            paramList.Add(rs2);

            int rs1 = (instruction & 0xF8000) >> 15;
            paramList.Add(rs1);

            int offset = 0;

            // get the 11 - 5 bit offsets of this immediate and shift them to the correct position
            offset |= ((int)(instruction & 0xFE000000)) >> 20;

            // get the 4 - 0 bit offsets of this immediate and shift them to the correct position
            offset |= ((instruction & 0xF80) >> 7);

            paramList.Add(offset);

            return paramList;
        }
    }
}
