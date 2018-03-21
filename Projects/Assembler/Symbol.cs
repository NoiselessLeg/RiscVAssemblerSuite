using Assembler.Util;

namespace Assembler
{

    /// <summary>
    /// Represents a symbol in an assembly file.
    /// </summary>
    class Symbol
    {
        /// <summary>
        /// Creates a new instance of an assembly symbol.
        /// </summary>
        /// <param name="name">The name of the label.</param>
        /// <param name="foundMemType">Which segment type the label was found in</param>
        /// <param name="address">The relative address of the label in the .text/.data segment.</param>
        public Symbol(string name, SegmentType foundMemType, int address)
        {
            m_SegType = foundMemType;
            m_LabelName = name;
            m_Address = address;
        }

        /// <summary>
        /// Gets the type of memory that this symbol was found in (i.e.
        /// the .text segment or .data segment).
        /// </summary>
        public SegmentType SegmentType
        {
            get { return m_SegType; }
        }
        
        /// <summary>
        /// Gets the name of the symbol.
        /// </summary>
        public string LabelName
        {
            get { return m_LabelName; }
        }

        /// <summary>
        /// Gets the address this symbol was found in.
        /// </summary>
        public int Address
        {
            get { return m_Address; }
        }

        private readonly SegmentType m_SegType;
        private readonly string m_LabelName;
        private readonly int m_Address;
    }
}
