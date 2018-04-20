using Assembler.Common;
using System;
using System.Collections.Generic;

namespace Assembler.OutputProcessing
{
    public class JefFileProcessor
    {
        public bool ProcessJefFile(string fileName, ILogger logger)
        {
            logger.Log(LogLevel.Info, "Invoking disassembler on file: " + fileName);
            JefFile jefFile = JefFile.ParseFile(fileName);

            var textParser = new TextSegmentParser();
            IEnumerable<DisassembledInstruction> instructions = textParser.ParseTextSegment(jefFile.TextElements);
            

            return true;
        }
    }
}
