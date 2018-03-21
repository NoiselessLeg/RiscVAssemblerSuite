using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Util
{
    class LineParseResults
    {
        public LineParseResults(int newAlignment, SegmentType newSegmentType)
        {
            m_NewAlignment = newAlignment;
            m_NewSegType = newSegmentType;
        }

        public SegmentType NewSegmentType
        {
            get { return m_NewSegType; }
        }

        public int NewAlignment
        {
            get { return m_NewAlignment; }
        }

        private readonly SegmentType m_NewSegType;
        private readonly int m_NewAlignment;
    }
}
