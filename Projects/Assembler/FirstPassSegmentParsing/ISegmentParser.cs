using Assembler.Util;

namespace Assembler.FirstPassSegmentParsing
{
    /// <summary>
    /// Defines a class that can parse a line from a segment of assembly code.
    /// </summary>
    internal interface ISegmentParser
    {
        /// <summary>
        /// Parses a segment of assembly code, and adds any found symbols to the symbol table with their resolved address.
        /// </summary>
        /// <param name="asmLine">The line in the assembly code to parse.</param>
        /// <param name="symbolList">The SymbolTable instance that will have new symbols added to it.</param>
        /// <returns>If the parser is not finished parsing the segment, returns the same segment type. If another
        /// segment declaration was found while parsing, returns the new segment type. Returns Invalid if the EOF was hit, or an
        /// invalid segment was found.</returns>
        SegmentType ParseLineInSegment(LineData asmLine, SymbolTable symbolList);
    }
}
