using Assembler.Common;
using Assembler.Util;
using System.Linq;

namespace Assembler.SymbolTableConstruction.SymbolBuilders
{
    class DataSymbolBuilder : ISymbolTableBuilder
    {
        /// <summary>
        /// Creates a DataSegmentParser instance.
        /// TODO: determine if signedness/unsignedness has any tangible effect
        /// on code generation.
        /// </summary>
        public DataSymbolBuilder()
        {
            m_CurrDataAddress = CommonConstants.BASE_DATA_ADDRESS;
        }

        /// <summary>
        /// Reads a denoted .data segment line of an assembly program for symbols
        /// </summary>
        /// <param name="reader">The reader used to read the file.</param>
        /// <param name="symbolList">The list of symbols that will be added to.</param>
        /// <param name="startingLine">The line that the .data segment starts on. Will be incremented</param>
        public void ParseSymbolsInLine(LineData asmLine, SymbolTable symbolList, int alignment)
        {
            string[] tokens = asmLine.Text.Split(' ');

            // a label should end with a ':' character and should be the first token.
            if (ParserCommon.ContainsLabel(tokens[0]))
            {
                ParseLabeledLine(symbolList, asmLine.LineNum, tokens, alignment);
            }

            // if this doesn't have a label, and is not empty or a comment,
            // then this is a data element.
            else
            {
                ParseUnlabeledLine(asmLine.LineNum, tokens, alignment);
            }
            
        }

        /// <summary>
        /// Parses a labeled line for symbols, and calculates the appropriate address of the element (if any).
        /// </summary>
        /// <param name="symTable">The symbol table to add the label to.</param>
        /// <param name="lineNum">The one-based index of the line that is being parsed.</param>
        /// <param name="tokens">The string array of tokens.</param>
        /// <param name="alignment">The current alignment</param>
        private void ParseLabeledLine(SymbolTable symTable, int lineNum, string[] tokens, int alignment)
        {
            string labelName = ParserCommon.ExtractLabel(tokens[0]);
            var label = new Symbol(labelName, SegmentType.Text, m_CurrDataAddress);
            symTable.AddSymbol(label);
            ParseUnlabeledLine(lineNum, tokens, alignment);
        }

        /// <summary>
        /// Parses an unlabeled line to calculate the appropriate address of an element (if any).
        /// </summary>
        /// <param name="lineNum">The one-based index of the line that is being parsed.</param>
        /// <param name="tokens">The string array of tokens.</param>
        /// <param name="alignment">The current alignment</param>
        private void ParseUnlabeledLine(int lineNum, string[] tokens, int alignment)
        {
            // take the line that we read originally, and split it by the ':' character.
            // scan it for a data size (e.g. .asciiz, .word, etc)
            for (uint i = 0; i < tokens.Length; ++i)
            {
                if (tokens[i].StartsWith("."))
                {
                    // if it is a trivial type, use our precomputed map to get the size.
                    // otherwise, determine the string length.
                    if (ParserCommon.IsTrivialDataType(tokens[i]))
                    {
                        int dataSize = ParserCommon.DetermineTrivialDataSize(lineNum, tokens[i]);
                        int paddingSize = ParserCommon.GetNumPaddingBytes(dataSize, alignment);
                        m_CurrDataAddress += (dataSize + paddingSize);
                        break;
                    }

                    // otherwise, we'd expect there to be another token after the data type.
                    // see if we can figure out the string length
                    else if (i < tokens.Length - 1)
                    {
                        int dataSize = ParserCommon.DetermineNonTrivialDataLength(lineNum, tokens[i], tokens[i + 1]);
                        int paddingSize = ParserCommon.GetNumPaddingBytes(dataSize, alignment);
                        m_CurrDataAddress += (dataSize + paddingSize);
                        break;
                    }

                    else
                    {
                        throw new AssemblyException(lineNum, "Unable to ascertain data type from " + tokens[i] + " token.");
                    }
                }
            }
        }
        
        private int m_CurrDataAddress;
    }
}
