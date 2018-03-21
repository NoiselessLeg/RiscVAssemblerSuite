using Assembler.Output;
using Assembler.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.CodeGeneration
{
    interface ISegmentCodeGenerator
    {
        void GenerateCodeForSegment(LineData asmLine, BasicObjectFile objFile, int currAlignment);
    }
}
