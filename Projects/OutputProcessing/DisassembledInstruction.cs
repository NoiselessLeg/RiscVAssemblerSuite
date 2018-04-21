
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Common
{
    /// <summary>
    /// Represents a disassembled instruction from a file.
    /// </summary>
    public class DisassembledInstruction
    {
        public DisassembledInstruction(InstructionType type, IEnumerable<int> instParams)
        {
            m_Type = type;
            m_Params = instParams;
        }

        /// <summary>
        /// Gets the instruction type.
        /// </summary>
        public InstructionType InstructionType
        {
            get { return m_Type; }
        }

        /// <summary>
        /// Gets the parameters associated with the instruction.
        /// </summary>
        public IEnumerable<int> Parameters
        {
            get { return m_Params; }
        }

        private readonly InstructionType m_Type;
        private readonly IEnumerable<int> m_Params;
    }
}
