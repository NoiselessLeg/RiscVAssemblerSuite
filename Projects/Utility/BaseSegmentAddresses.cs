using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Common
{
    public static class BaseSegmentAddresses
    {
        public const int BASE_TEXT_ADDRESS = 0x00400000;
        public const int BASE_DATA_ADDRESS = 0x10010000;
    }
}
