using Assembler.SymbolTableConstruction.SymbolBuilders;
using Assembler.Util;
using System.Collections.Generic;

namespace Assembler.SymbolTableConstruction
{

    /// <summary>
    /// Utility class for operations dealing with various executable segment types.
    /// </summary>
    class SegmentSymbolParserFactory
    {

        /// <summary>
        /// Creates a mapping to segment types to their declarations.
        /// Add future supported segment types here.
        /// </summary>
        public SegmentSymbolParserFactory()
        {
            m_ParserTable = new Dictionary<SegmentType, ISymbolTableBuilder>()
            {
                { SegmentType.Data, new DataSymbolBuilder() },
                { SegmentType.Text, new TextSymbolBuilder() }
            };
        }

        
        
        /// <summary>
        /// Retrieves a segment parser implementation for a specific segment type.
        /// </summary>
        /// <param name="lineNum">The line number that is currently being parsed. Displayed if an error occurs.</param>
        /// <param name="segType">The segment type to retrieve a parser for.</param>
        /// <returns>An appropriate segment parser for the given segment. If one cannot be found, this throws an AssemblyException.</returns>
        public ISymbolTableBuilder GetParserForSegment(int lineNum, SegmentType segType)
        {
            ISymbolTableBuilder parser = null;
            if (!m_ParserTable.TryGetValue(segType, out parser))
            {
                throw new AssemblyException(lineNum, "Could not retrieve segment parser for segment type: " + segType);
            }

            return parser;
        }

        private readonly Dictionary<SegmentType, ISymbolTableBuilder >m_ParserTable;
    }
}
