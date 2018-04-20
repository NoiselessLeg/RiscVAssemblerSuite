using Assembler.Common;
using System;
using System.Collections.Generic;

namespace Assembler.OutputProcessing
{
    /// <summary>
    /// Processes and disassembles an entire .JEF file into its appropriate parsed segments.
    /// </summary>
    public class JefFileProcessor
    {
        /// <summary>
        /// Breaks down a .JEF object file into .text, .data, and .extern segments.
        /// </summary>
        /// <param name="fileName">The file to process.</param>
        /// <param name="logger">A logger instance to use (future)</param>
        /// <returns>An instance of a fully disassembled RISC-V file for further processing</returns>
        public DisassembledFile ProcessJefFile(string fileName, ILogger logger)
        {
            JefFile jefFile = JefFile.ParseFile(fileName);

            var textParser = new TextSegmentParser();
            IEnumerable<DisassembledInstruction> instructions = textParser.ParseTextSegment(jefFile.TextElements);

            var dataSegment = new DataSegmentAccessor(jefFile.DataElements, jefFile.DataMetadata, jefFile.BaseDataAddress);
            var textSegment = new TextSegmentAccessor(instructions, jefFile.BaseTextAddress);

            return new DisassembledFile(dataSegment, textSegment, jefFile.ExternElements, jefFile.SymbolTable);
        }
    }
}
