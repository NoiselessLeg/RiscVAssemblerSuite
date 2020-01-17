using Assembler.Common;
using Assembler.InstructionProcessing;
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
        /// <param name="procFactory">The instruction processor factory to retrieve instruction size estimator implementations from.</param>
        public TextSymbolBuilder(InstructionProcessorFactory procFac)
        {
            m_CurrTextAddress = CommonConstants.BASE_TEXT_ADDRESS;
            m_SizeEstimatorFac = procFac;
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
                var label = new Symbol(labelName, SegmentType.Text, m_CurrTextAddress, 4);
                symbolList.AddSymbol(label);

                // determine if there are any instructions on this line.
                string[] subTokens = tokens[0].Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                // if we have more than one subtoken, then there is more than just a label on this line.
                // increment the number of words in the segment (since we're assuming whatever is on the right-hand side
                // is an instruction) by however many bytes the instruction is
                if (subTokens.Length > 1)
                {
                    ParseUnlabeledLine(asmLine);
                }
            }
                
            // if this doesn't have a label, and is not empty or a comment,
            // then this is an instruction. increment the counter.
            else
            {
                ParseUnlabeledLine(asmLine);
            }
            
        }

        /// <summary>
        /// Parses an unlabeled line to calculate the appropriate address of the next element (if any).
        /// </summary>
        /// <param name="originalLine">The line data being parsed.</param>
        /// <returns>A boolean determining if anything of use was parsed. If this is false,
        /// the line should be examined to make sure a symbol was at least parsed. Otherwise,
        /// this could indicate that garbage was on the line.</returns>
        private void ParseUnlabeledLine(LineData originalLine)
        {
            string[] tokenizedStr = originalLine.Text.Split(new char[] { ',', ':', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            bool foundInstruction = false;

            string instructionToken = string.Empty;
            for (int i = 0; i < tokenizedStr.Length && !foundInstruction; ++i)
            {
                string token = tokenizedStr[i].Trim();
                // we found our instruction. build a string from this token
                // to the end of the array.
                if (m_SizeEstimatorFac.IsInstruction(token))
                {
                    foundInstruction = true;
                    instructionToken = token;
                }
            }

            if (foundInstruction)
            {
                // first, validate that the instruction is not the last token in the string.
                // try to parse the instruction parameters
                // get the substring starting at the index of the next character after the instruction
                string instSubstring = originalLine.Text.Substring(originalLine.Text.IndexOf(instructionToken) + instructionToken.Length);

                //split the substring at the comma to get the instruction parameters.
                string[] argTokens = instSubstring.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                // trim whitespace from the beginning and end of each token.
                argTokens = argTokens.Apply((str) => str.Trim()).ToArray();

                // find the parser for the instruction.
                IInstructionSizeEstimator parser = m_SizeEstimatorFac.GetEstimatorForInstruction(instructionToken);

                // beq instructions should (hopefully) not generate multiple instructions..
                int numGeneratedInstructions = parser.GetNumGeneratedInstructions(m_CurrTextAddress, argTokens);
                m_CurrTextAddress += (CommonConstants.BASE_INSTRUCTION_SIZE_BYTES * numGeneratedInstructions);
            }
        }

        private int m_CurrTextAddress;
        private readonly InstructionProcessorFactory m_SizeEstimatorFac;
    }
}
