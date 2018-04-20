using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Common
{
    /// <summary>
    /// Defines the various possible RISC-V data types as a byte code.
    /// </summary>
    public enum ObjectTypeCode : byte
    {
        Byte = 0x01,
        Half = 0x02,
        Word = 0x03,
        Dword = 0x04,
        String = 0x05
    }
}
