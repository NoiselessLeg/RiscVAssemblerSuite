using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.OutputProcessing.ParamDecoding
{
    class BranchDecoder : IParameterDecoder
    {
        public IEnumerable<int> DecodeParameters(int instruction)
        {
            var paramList = new List<int>();
            int rs1 = (instruction & 0xF8000) >> 15;
            paramList.Add(rs1);

            int rs2 = (instruction & 0x1F00000) >> 20;
            paramList.Add(rs2);

            int offset = 0;

            // get the 12th bit offset of this immediate and shift it to the correct position
            offset |= ((int)(instruction & 0x80000000)) >> 22;

            // get the 11th bit offset of this immediate and shift it to the correct position.
            offset |= ((instruction & 0x80) << 3);

            // get the 10-5 bit offsets of this immediate and shift them to the correct position.
            offset |= ((instruction & 0x7E000000) >> 20);

            // get the 4-1 bit offsets of the immediate and shift them to the correct position.
            offset |= ((instruction & 0xF00) >> 7);

            paramList.Add(offset);

            return paramList;
        }
    }
}
