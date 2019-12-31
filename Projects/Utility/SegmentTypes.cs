using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Common
{
    /// <summary>
    /// An enumeration of the various supported segment types.
    /// </summary>
    public enum SegmentType
    {
        Invalid,
        Data,
        Text,
        Extern,
        KText,
        KData
    }
}
