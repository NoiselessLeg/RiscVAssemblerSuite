using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Common
{
    /// <summary>
    /// Provides a simulated interface for accessing instructions at a specified address.
    /// </summary>
    public class TextSegmentAccessor
    {
        /// <summary>
        /// Creates an instance of the TextSegmentAccessor.
        /// </summary>
        /// <param name="instructions">An IEnumerable of disassembled instructions.</param>
        /// <param name="startingSegmentAddress">The address that the .text segment will start at during runtime.</param>
        public TextSegmentAccessor(IEnumerable<DisassembledInstruction> instructions, int startingSegmentAddress)
        {
            m_Instructions = instructions.ToArray();
            m_StartingSegmentAddress = startingSegmentAddress;
        }
        
        /// <summary>
        /// Gets a raw IEnumerable of all of the disassembled instructions in the .text segment.
        /// </summary>
        public IEnumerable<DisassembledInstruction> RawInstructions
        {
            get { return m_Instructions; }
        }

        /// <summary>
        /// Gets the runtime starting address of this segment.
        /// </summary>
        public int StartingSegmentAddress
        {
            get { return m_StartingSegmentAddress; }
        }

        /// <summary>
        /// Fetches the instruction at the provided program counter address.
        /// </summary>
        /// <param name="programCounter">The current program counter value.</param>
        /// <returns>An instruction located at the provided address in the .text segment.</returns>
        public DisassembledInstruction FetchInstruction(int programCounter)
        {
            int instructionIdx = programCounter - m_StartingSegmentAddress;
            return m_Instructions[instructionIdx];
        }
        
        private readonly DisassembledInstruction[] m_Instructions;
        private readonly int m_StartingSegmentAddress;
    }
}
