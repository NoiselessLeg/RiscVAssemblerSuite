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
      /// <summary>
      /// Generates the byte representation of an instruction from a line of assembly code.
      /// </summary>
      /// <param name="fileName">The source file name.</param>
      /// <param name="asmLine">The line to parse.</param>
      /// <param name="objFile">The object file that will be written to.</param>
      /// <param name="currAlignment">The current specified alignment of the file.</param>
      void GenerateCodeForSegment(string fileName, LineData asmLine, BasicObjectFile objFile, int currAlignment);
    }
}
