using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.OutputProcessing.ParamDecoding
{
    class ShiftImmediateDecoder : IParameterDecoder
    {
        public IEnumerable<int> DecodeParameters(int instruction)
        {
            var paramList = new List<int>();
            int rd = (instruction & 0xF80) >> 7;
            paramList.Add(rd);

            int rs1 = (instruction & 0xF8000) >> 15;
            paramList.Add(rs1);

            int shamt = (instruction & 0x1F00000) >> 20;
            paramList.Add(shamt);

            return paramList;
        }
    }
}
