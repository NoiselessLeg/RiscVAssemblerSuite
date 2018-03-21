namespace Assembler.Util
{
    /// <summary>
    /// Represents a line of text in a .asm file.
    /// </summary>
    class LineData
    {
        /// <summary>
        /// Creates an instance of a LineData object.
        /// </summary>
        /// <param name="text">The text in the line.</param>
        /// <param name="lineNum">The line number.</param>
        public LineData(string text, int lineNum)
        {
            m_Text = text;
            m_LineNum = lineNum;
        }

        /// <summary>
        /// Gets the text associated with the assembly line.
        /// </summary>
        public string Text
        {
            get { return m_Text; }
        }

        /// <summary>
        /// Gets the 1-based line number.
        /// </summary>
        public int LineNum
        {
            get { return m_LineNum; }
        }

        private readonly string m_Text;
        private readonly int m_LineNum;
    }
}
