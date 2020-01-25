using Assembler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.OutputProcessing.FileReaders
{
   public class JefFileReader : ICompiledFileReader
   {
      public DisassembledFileBase ParseFile(string fileName, ILogger logger)
      {
         JefFile jefFile = JefFile.ParseFile(fileName);

         var textParser = new TextSegmentParser();
         IEnumerable<DisassembledInstruction> instructions = textParser.ParseTextSegment(jefFile.TextElements);

         int dataSegSize = 0;
         foreach (MetadataElement elem in jefFile.DataMetadata)
         {
            dataSegSize += elem.Size;
         }

         var dataSegment = new DataSegmentAccessor(jefFile.DataElements, jefFile.BaseDataAddress, dataSegSize);

         var textSegment = new TextSegmentAccessor(instructions, jefFile.BaseTextAddress);

         return new DisassembledJefFile(dataSegment, textSegment, jefFile.ExternElements, 
                                        jefFile.SymbolTable, jefFile.DataMetadata, jefFile.DebugData);
      }
   }
}
