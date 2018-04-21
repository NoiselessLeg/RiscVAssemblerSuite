using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.OutputProcessing
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
            m_SegmentSize = instructions.Count() * sizeof(int);
        }

        /// <summary>
        /// Determines if the program counter has exceeded the size of the .text segment.
        /// In effect, this indicates the program has dropped off the bottom.
        /// </summary>
        /// <param name="programCounter">The current 32-bit program counter value.</param>
        /// <returns>True if the end of the .text segment is reached according to the program counter; false otherwise.</returns>
        public bool EndOfFileReached(int programCounter)
        {
            return ((programCounter - m_StartingSegmentAddress) / sizeof(int)) > m_SegmentSize;
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
            int instructionIdx = (programCounter - m_StartingSegmentAddress) / sizeof(int);
            return m_Instructions[instructionIdx];
        }
        
        private readonly DisassembledInstruction[] m_Instructions;
        private readonly int m_StartingSegmentAddress;
        private readonly int m_SegmentSize;
    }
}
