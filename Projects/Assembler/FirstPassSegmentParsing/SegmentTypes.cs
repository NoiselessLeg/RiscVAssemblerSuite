using System.Collections.Generic;

namespace Assembler.FirstPassSegmentParsing
{
    /// <summary>
    /// An enumeration of the various supported segment types.
    /// </summary>
    public enum SegmentType
    {
        Invalid,
        Data,
        Text
    }

    /// <summary>
    /// Utility class for operations dealing with various executable segment types.
    /// </summary>
    class SegmentTypeHelper
    {

        /// <summary>
        /// Creates a mapping to segment types to their declarations.
        /// Add future supported segment types here.
        /// </summary>
        /// <param name="baseTextAddress">The base address that the .text segment begins at.</param>
        /// <param name="baseDataAddress">The base address that the .data segment begins at.</param>
        public SegmentTypeHelper(int baseTextAddress, int baseDataAddress)
        {

            m_ParserTable = new Dictionary<SegmentType, ISegmentParser>()
            {
                { SegmentType.Data, new DataSegmentParser(baseDataAddress) },
                { SegmentType.Text, new TextSegmentParser(baseTextAddress) }
            };
        }

        /// <summary>
        /// Creates the segment types database.
        /// </summary>
        static SegmentTypeHelper()
        {
            s_SegmentTypes = new Dictionary<string, SegmentType>()
            {
                { ".data", SegmentType.Data },
                { ".text", SegmentType.Text }
            };
        }

        /// <summary>
        /// Determines if a segment declaration is mapped to a currently supported
        /// segment type.
        /// </summary>
        /// <param name="token">The string token to examine.</param>
        /// <returns>True if the token declares the start of a new segment type.</returns>
        public static bool IsSegmentDeclarationToken(string token)
        {
            return s_SegmentTypes.ContainsKey(token);
        }

        /// <summary>
        /// Gets the segment type associated with a specific token.
        /// </summary>
        /// <param name="type">The token to examine.</param>
        /// <returns>The machine segment type associated with the token if it is a 
        /// segment declaration, otherwise returns SegmentType.Invalid.</returns>
        public static SegmentType GetSegmentType(string type)
        {
            SegmentType segmentType = SegmentType.Invalid;

            if (!s_SegmentTypes.TryGetValue(type, out segmentType))
            {
                segmentType = SegmentType.Invalid;
            }

            return segmentType;
        }
        
        /// <summary>
        /// Retrieves a segment parser implementation for a specific segment type.
        /// </summary>
        /// <param name="lineNum">The line number that is currently being parsed. Displayed if an error occurs.</param>
        /// <param name="segType">The segment type to retrieve a parser for.</param>
        /// <returns>An appropriate segment parser for the given segment. If one cannot be found, this throws an AssemblyException.</returns>
        public ISegmentParser GetParserForSegment(int lineNum, SegmentType segType)
        {
            ISegmentParser parser = null;
            if (!m_ParserTable.TryGetValue(segType, out parser))
            {
                throw new AssemblyException(lineNum, "Could not retrieve segment parser for segment type: " + segType);
            }

            return parser;
        }

        private static readonly Dictionary<string, SegmentType> s_SegmentTypes;
        private readonly Dictionary<SegmentType, ISegmentParser >m_ParserTable;
    }
}
