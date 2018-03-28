namespace Assembler.Util
{
    /// <summary>
    /// Represents the results of the initial parse for any assembler directives.
    /// </summary>
    class LineParseResults
    {
        /// <summary>
        /// Creates a new instance of the LineParseResults struct.
        /// </summary>
        /// <param name="lineIsDirective">True if the line was an assembler directive and requires no further processing.</param>
        /// <param name="newAlignment">The new alignment (if a new alignment was declared), or the previous alignment.</param>
        /// <param name="newSegment">The new segment type (if a new segment was declared), or the previous segment.</param>
        public LineParseResults(bool lineIsDirective, int newAlignment, SegmentType newSegment)
        {
            m_NewAlignment = newAlignment;
            m_NewSegType = newSegment;
            m_IsAssemblerDirectiveLine = lineIsDirective;
        }

        /// <summary>
        /// Gets the new segment type if a directive changed it, or the previous segment.
        /// </summary>
        public SegmentType NewSegment
        {
            get { return m_NewSegType; }
        }

        /// <summary>
        /// Gets the new alignment if a directive changed it, or the previous alignment
        /// </summary>
        public int NewAlignment
        {
            get { return m_NewAlignment; }
        }

        /// <summary>
        /// Gets if the parsed line was an assembler directive.
        /// </summary>
        public bool IsLineAssemblerDirective
        {
            get { return m_IsAssemblerDirectiveLine; }
        }

        private readonly bool m_IsAssemblerDirectiveLine;
        private readonly int m_NewAlignment;
        private readonly SegmentType m_NewSegType;
    }
}
