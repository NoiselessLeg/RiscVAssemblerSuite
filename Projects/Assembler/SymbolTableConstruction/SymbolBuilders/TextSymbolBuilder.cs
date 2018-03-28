using Assembler.Common;
using Assembler.Util;
using System;
using System.Linq;

namespace Assembler.SymbolTableConstruction.SymbolBuilders
{
    /// <summary>
    /// Parses a line from the .text segment for symbols, and calculates the appropriate address
    /// of the next instruction.
    /// </summary>
    class TextSymbolBuilder : ISymbolTableBuilder
    {
        /// <summary>
        /// Creates a new instance of a text segment parser
        /// TODO: determine if signedness/unsignedness has any tangible effect
        /// on code generation.
        /// </summary>
        public TextSymbolBuilder()
        {
            m_CurrTextAddress = CommonConstants.BASE_TEXT_ADDRESS;
        }

        /// <summary>
        /// Reads a line in a .text segment of a program, and adds any found symbols to the
        /// symbol table.
        /// </summary>
        /// <param name="asmLine">The line of assembly code to parse.</param>
        /// <param name="symbolList">The list of symbols that will be added to.</param>
        /// <param name="alignment">Unused. Alignment is always on word boundaries in the text segment.</param>
        public void ParseSymbolsInLine(LineData asmLine, SymbolTable symbolList, int alignment)
        {
            string[] tokens = asmLine.Text.Split(' ');
            // a label should end with a ':' character.
            // this is OK if there's trash and no real assembly at this point,
            // as the second pass code generator will flag it.
            // we're just here to get symbols and addresses.
            if (ParserCommon.ContainsLabel(tokens[0]))
            {
                string labelName = ParserCommon.ExtractLabel(tokens[0]);
                var label = new Symbol(labelName, SegmentType.Text, m_CurrTextAddress);
                symbolList.AddSymbol(label);

                // determine if there are any instructions on this line.
                string[] subTokens = tokens[0].Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                // if we have more than one subtoken, then there is more than just a label on this line.
                // increment the number of words in the segment (since we're assuming whatever is on the right-hand side
                // is an instruction) by however many bytes the instruction is
                if (subTokens.Length > 1)
                {
                    m_CurrTextAddress += CommonConstants.BASE_INSTRUCTION_SIZE_BYTES;
                }
            }
                
            // if this doesn't have a label, and is not empty or a comment,
            // then this is an instruction. increment the counter.
            else
            {
                m_CurrTextAddress += CommonConstants.BASE_INSTRUCTION_SIZE_BYTES;
            }
            
        }
        
        private int m_CurrTextAddress;
    }
}
