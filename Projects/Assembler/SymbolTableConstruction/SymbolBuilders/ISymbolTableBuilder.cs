using Assembler.Common;
using Assembler.Util;

namespace Assembler.SymbolTableConstruction.SymbolBuilders
{
    /// <summary>
    /// Defines a class that can parse a line from a segment of assembly code.
    /// </summary>
    internal interface ISymbolTableBuilder
    {
        /// <summary>
        /// Parses a segment of assembly code, and adds any found symbols to the symbol table with their resolved address.
        /// </summary>
        /// <param name="asmLine">The line in the assembly code to parse.</param>
        /// <param name="symbolList">The SymbolTable instance that will have new symbols added to it.</param>
        /// <param name="alignment">The current alignment.</param>
        void ParseSymbolsInLine(LineData asmLine, SymbolTable symbolList, int alignment);
    }
}
