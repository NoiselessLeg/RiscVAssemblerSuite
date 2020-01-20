using Assembler.Common;
using Assembler.ELF_Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.OutputProcessing.FileReaders
{
   public class ElfFileReader : ICompiledFileReader
   {
      public DisassembledFileBase ParseFile(string fileName, ILogger logger)
      {
         var elfFileParser = new ELF_Reader(fileName);
         var decompiledFile = new ELF_CompiledFile(elfFileParser);

         var dataSection = new DataSegmentAccessor(decompiledFile.DataBytes, decompiledFile.DataSegmentStartingAddress, decompiledFile.DataSegmentSize);
         
         var textSegmentParser = new TextSegmentParser();
         IEnumerable<DisassembledInstruction> rawTextSegment = textSegmentParser.ParseTextSegment(decompiledFile.RawInstructions);
         var textSection = new TextSegmentAccessor(rawTextSegment, decompiledFile.TextSegmentStartingAddress);

         //TODO: fix when we have better sectioning logic.
         var dummyExtern = new List<byte>();

         return new DisassembledElfFile(dataSection, textSection, dummyExtern, decompiledFile.SymbolTable);
      }
   }
}
