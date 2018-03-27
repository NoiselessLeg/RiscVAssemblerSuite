using System;
using System.Linq;
using Assembler.Output;
using Assembler.Util;

namespace Assembler.CodeGeneration
{
    /// <summary>
    /// Generates code for a .data segment line of an assembly file.
    /// </summary>
    class DataCodeGenerator : ISegmentCodeGenerator
    {
        public DataCodeGenerator(SymbolTable symTable)
        {
            m_SymTbl = symTable;
        }

        /// <summary>
        /// Generates the byte representation of an instruction from a line of assembly code.
        /// </summary>
        /// <param name="asmLine">The line to parse.</param>
        /// <param name="objFile">The object file that will be written to.</param>
        /// <param name="currAlignment">The current specified alignment of the file.</param>
        public void GenerateCodeForSegment(LineData asmLine, BasicObjectFile objFile, int currAlignment)
        {
            string[] tokens = asmLine.Text.Split(' ', '\t');
            string[] fixedTokens = ParserCommon.GetTrimmedTokenArray(tokens).ToArray();
            bool foundDataDeclaration = false;
            int dataDeclarationIdx = 0;
            for (int i = 0; i < fixedTokens.Length && !foundDataDeclaration; ++i)
            {
                if (ParserCommon.IsDataDeclaration(fixedTokens[i]))
                {
                    foundDataDeclaration = true;
                    dataDeclarationIdx = i;
                }
            }

            // we found a data declaration; make sure that there's at least one value following it.
            if (foundDataDeclaration)
            {
                if (dataDeclarationIdx + 1 < fixedTokens.Length)
                {
                    // if it is a trivial type, use our precomputed map to get the size.
                    if (ParserCommon.IsTrivialDataType(fixedTokens[dataDeclarationIdx]))
                    {

                        int dataSize = ParserCommon.DetermineTrivialDataSize(fixedTokens[dataDeclarationIdx]);
                        int paddingSize = ParserCommon.GetNumPaddingBytes(dataSize, currAlignment);

                        AddTrivialDataElementToObjectFile(objFile, dataSize, fixedTokens[dataDeclarationIdx + 1]);

                        // add as much padding as we need to reach the next alignment boundary.
                        for (int i = 0; i < paddingSize; ++i)
                        {
                            objFile.AddDataElement((byte)0);
                        }

                        // we expect one token after this word.
                        if (fixedTokens.Length > dataDeclarationIdx + 2)
                        {
                            throw new AssemblyException(asmLine.LineNum, "Unknown token \"" + fixedTokens[dataDeclarationIdx + 2] + "\" found.");
                        }
                    }
                    
                    // see if we can figure out the string length
                    else if (ParserCommon.IsStringDeclaration(fixedTokens[dataDeclarationIdx]))
                    {
                        // if this is a string declaration, then get the original string data
                        string dataStr = ParserCommon.GetStringData(asmLine.Text);

                        // add the string data to the object file.
                        AddNonTrivialDataElementToObjectFile(objFile, fixedTokens[dataDeclarationIdx], dataStr);

                        int dataSize = ParserCommon.DetermineNonTrivialDataLength(fixedTokens[dataDeclarationIdx], dataStr);

                        int paddingSize = ParserCommon.GetNumPaddingBytes(dataSize, currAlignment);

                        // add as much padding as we need to reach the next alignment boundary.
                        for (int i = 0; i < paddingSize; ++i)
                        {
                            objFile.AddDataElement((byte)0);
                        }
                    }

                    // otherwise, this must be a .space declaration. just get the size following it.
                    else
                    {
                        int dataSize = ParserCommon.DetermineNonTrivialDataLength(fixedTokens[dataDeclarationIdx], fixedTokens[dataDeclarationIdx + 1]);
                        int paddingSize = ParserCommon.GetNumPaddingBytes(dataSize, currAlignment);

                        // fill the space and padding with zeroes.
                        for (int i = 0; i < dataSize + paddingSize; ++i)
                        {
                            objFile.AddDataElement((byte)0);
                        }

                        // we expect one token after this word.
                        // otherwise, it may be garbage that we should detect.
                        if (fixedTokens.Length > dataDeclarationIdx + 2)
                        {
                            throw new AssemblyException(asmLine.LineNum, "Unknown token \"" + fixedTokens[dataDeclarationIdx + 2] + "\" found.");
                        }
                    }
                }
                else
                {
                    throw new AssemblyException(asmLine.LineNum, "Expected data value after token " + fixedTokens[dataDeclarationIdx]);
                }
            }

            // check to see if this is just a label.
            // otherwise, it is probably garbage that we should throw.
            else if (!ParserCommon.ContainsLabel(asmLine.Text))
            {
                throw new AssemblyException(asmLine.LineNum, "Unable to ascertain data type from line " + asmLine.Text);
            }
        }

        /// <summary>
        /// Adds a trivially sized element to the object file.
        /// </summary>
        /// <param name="objFile">The object file to add to.</param>
        /// <param name="dataSize">The data size of the element to add.</param>
        /// <param name="elemValue">The value to add, as a string.</param>
        private void AddTrivialDataElementToObjectFile(BasicObjectFile objFile, int dataSize, string elemValue)
        {
            const int BYTE_DATA_SIZE = 1;
            const int SHORT_DATA_SIZE = 2;
            const int WORD_DATA_SIZE = 4;
            const int DWORD_DATA_SIZE = 8;

            switch (dataSize)
            {
                case BYTE_DATA_SIZE:
                {
                    byte val = 0;
                    if (!byte.TryParse(elemValue, out val))
                    {
                        // see if we can resolve the string as a symbol.
                        Symbol sym = m_SymTbl.GetSymbol(elemValue);
                        val = (byte) sym.Address;
                    }

                    objFile.AddDataElement(val);
                    break;
                }

                case SHORT_DATA_SIZE:
                {
                    short val = 0;
                    if (!short.TryParse(elemValue, out val))
                    {
                        // see if we can resolve the string as a symbol.
                        Symbol sym = m_SymTbl.GetSymbol(elemValue);
                        val = (short)sym.Address;
                    }

                    objFile.AddDataElement(val);
                    break;
                }

                case WORD_DATA_SIZE:
                {
                    int val = 0;
                    if (!int.TryParse(elemValue, out val))
                    {
                        // see if we can resolve the string as a symbol.
                        Symbol sym = m_SymTbl.GetSymbol(elemValue);
                        val = sym.Address;
                    }

                    objFile.AddDataElement(val);
                    break;
                }

                case DWORD_DATA_SIZE:
                {
                    long val = 0;
                    if (!long.TryParse(elemValue, out val))
                    {
                        // see if we can resolve the string as a symbol.
                        Symbol sym = m_SymTbl.GetSymbol(elemValue);
                        val = sym.Address;
                    }

                    objFile.AddDataElement(val);
                    break;
                }

                default:
                {
                    throw new ArgumentException("Unknown data size passed to AddTrivialDataElementToObjectFile");
                }
            }
        }

        /// <summary>
        /// Adds a non-trivial data size element to the object file.
        /// </summary>
        /// <param name="objFile">The object file to add the element to.</param>
        /// <param name="dataType">The string token declaring the data type</param>
        /// <param name="elemValue">The string</param>
        /// <param name="lineNum"></param>
        private void AddNonTrivialDataElementToObjectFile(BasicObjectFile objFile, string dataType, string elemValue)
        {
            if (dataType == ".ascii")
            {
                objFile.AddAsciiString(elemValue);
            }

            else if (dataType == ".asciiz")
            {
                objFile.AddNullTerminatedAsciiString(elemValue);
            }

            else if (dataType == ".space")
            {
                int numReservedSpace = ParserCommon.DetermineNonTrivialDataLength(dataType, elemValue);
                for (int i = 0; i < numReservedSpace; ++i)
                {
                    objFile.AddDataElement((byte)0);
                }
            }
            else
            {
                throw new ArgumentException("Unknown data type " + dataType + " passed as non-trivial data type.");
            }
        }

        private readonly SymbolTable m_SymTbl;
    }
}
