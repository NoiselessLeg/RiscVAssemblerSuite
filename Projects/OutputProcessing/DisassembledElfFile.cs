using Assembler.Common;
using Assembler.OutputProcessing.TextOutput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.OutputProcessing
{
   class DisassembledElfFile : DisassembledFileBase
   {
      public DisassembledElfFile(DataSegmentAccessor dataSegment, TextSegmentAccessor instructions,
                                 IEnumerable<byte> externSegment, ReverseSymbolTable symTable) :
         base(dataSegment, instructions, externSegment, symTable)
      {
         m_FileWriter = new ElfAssemblyFileWriter(this);
      }

      public override IAssemblyFileWriter AssemblyTextFileWriter => m_FileWriter;
      private readonly IAssemblyFileWriter m_FileWriter;
   }
}
