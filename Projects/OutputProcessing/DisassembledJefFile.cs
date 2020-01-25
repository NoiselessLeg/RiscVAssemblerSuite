using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assembler.Common;
using Assembler.OutputProcessing.TextOutput;

namespace Assembler.OutputProcessing
{
   class DisassembledJefFile : DisassembledFileBase
   {
      public DisassembledJefFile(DataSegmentAccessor dataSegment, TextSegmentAccessor instructions,
                                 IEnumerable<byte> externSegment, ReverseSymbolTable symTable,
                                 IEnumerable<MetadataElement> metadata, SourceDebugData dbgData):
         base(dataSegment, instructions, externSegment, symTable, dbgData)
      {
         m_FileWriter = new JefAssemblyFileWriter(this);
         m_Metadata = metadata;
      }

      public IEnumerable<MetadataElement> Metadata => m_Metadata;

      public override IAssemblyFileWriter AssemblyTextFileWriter => m_FileWriter;

      private readonly IEnumerable<MetadataElement> m_Metadata;
      private readonly IAssemblyFileWriter m_FileWriter;
   }
}
