using Assembler.Common;
using Assembler.Util;
using System;
using System.Collections.Generic;
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
            string[] fixedTokens = ParserCommon.GetTrimmedTokenArray(tokens).ToArray();

            // a label should end with a ':' character and should be the first token.
            if (ParserCommon.ContainsLabel(fixedTokens[0]))
            {
                ParseLabeledLine(symbolList, asmLine, fixedTokens, alignment);
            }

            // if this doesn't have a label, and is not empty or a comment,
            // then this is a data element.
            else
            {
                ParseUnlabeledLine(asmLine, fixedTokens, alignment);
            }
            
        }

        /// <summary>
        /// Parses a labeled line for symbols, and calculates the appropriate address of the element (if any).
        /// </summary>
        /// <param name="symTable">The symbol table to add the label to.</param>
        /// <param name="originalLine">The line data being parsed.</param>
        /// <param name="tokens">The string array of space-separated tokens.</param>
        /// <param name="alignment">The current alignment</param>
        private void ParseLabeledLine(SymbolTable symTable, LineData originalLine, string[] tokens, int alignment)
        {
            string labelName = ParserCommon.ExtractLabel(tokens[0]);
            var label = new Symbol(labelName, SegmentType.Text, m_CurrDataAddress);
            symTable.AddSymbol(label);
            ParseUnlabeledLine(originalLine, tokens, alignment);
        }

        /// <summary>
        /// Parses an unlabeled line to calculate the appropriate address of the next element (if any).
        /// </summary>
        /// <param name="originalLine">The line data being parsed.</param>
        /// <param name="tokens">The string array of space-separated tokens.</param>
        /// <param name="alignment">The current alignment</param>
        private void ParseUnlabeledLine(LineData originalLine, string[] tokens, int alignment)
        {
            bool foundDataDeclaration = false;
            int dataDeclarationIdx = 0;


            // scan it for a data size (e.g. .asciiz, .word, etc)
            for (int i = 0; i < tokens.Length && !foundDataDeclaration; ++i)
            {
                if (ParserCommon.IsDataDeclaration(tokens[i]))
                {
                    foundDataDeclaration = true;
                    dataDeclarationIdx = i;
                }
            }

            // we found a data declaration; make sure that there's at least one value following it.
            if (foundDataDeclaration)
            {
                if (dataDeclarationIdx + 1 < tokens.Length)
                {
                    // if it is a trivial type, use our precomputed map to get the size.
                    if (ParserCommon.IsTrivialDataType(tokens[dataDeclarationIdx]))
                    {
                        int dataSize = ParserCommon.DetermineTrivialDataSize(originalLine.LineNum, tokens[dataDeclarationIdx]);
                        int paddingSize = ParserCommon.GetNumPaddingBytes(dataSize, alignment);
                        m_CurrDataAddress += (dataSize + paddingSize);
                    }

                    // otherwise, we'd expect there to be another token after the data type.
                    // see if we can figure out the string length
                    else if (ParserCommon.IsStringDeclaration(tokens[dataDeclarationIdx]))
                    {
                        // if this is a string declaration, then get the original string data
                        string dataStr = ParserCommon.GetStringData(originalLine.Text);

                        int dataSize = ParserCommon.DetermineNonTrivialDataLength(originalLine.LineNum,
                                                                                  tokens[dataDeclarationIdx], 
                                                                                  dataStr);

                        int paddingSize = ParserCommon.GetNumPaddingBytes(dataSize, alignment);
                        m_CurrDataAddress += (dataSize + paddingSize);
                    }

                    // otherwise, this must be a .space declaration. just get the size following it.
                    else
                    {
                        int dataSize = ParserCommon.DetermineNonTrivialDataLength(originalLine.LineNum,
                                                                                  tokens[dataDeclarationIdx],
                                                                                  tokens[dataDeclarationIdx + 1]);

                        int paddingSize = ParserCommon.GetNumPaddingBytes(dataSize, alignment);
                        m_CurrDataAddress += (dataSize + paddingSize);
                    }
                }
                else
                {
                    throw new AssemblyException(originalLine.LineNum, "Expected data value after token " + tokens[dataDeclarationIdx]);
                }
            }
        }

        
        
        private int m_CurrDataAddress;
    }
}
