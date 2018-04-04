using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Util
{
    class InstructionElement
    {
        public InstructionElement(string instructionName, IEnumerable<string> args)
        {
            m_InsName = instructionName;
            m_Arguments = args;
        }

        public string InstructionName
        {
            get { return m_InsName; }
        }

        public IEnumerable<string> Arguments
        {
            get { return m_Arguments; }
        }

        private readonly string m_InsName;
        private readonly IEnumerable<string> m_Arguments;
    }
}
