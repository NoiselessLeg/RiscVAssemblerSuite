using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assembler.Output;
using Assembler.Util;

namespace Assembler.CodeGeneration
{
    class DataCodeGenerator : ISegmentCodeGenerator
    {
        public void GenerateCodeForSegment(LineData asmLine, BasicObjectFile objFile, int currAlignment)
        {
            string[] tokens = asmLine.Text.Split(' ');
            bool foundDataDeclaration = false;
            int dataDeclarationIdx = 0;
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
                        int dataSize = ParserCommon.DetermineTrivialDataSize(asmLine.LineNum, tokens[dataDeclarationIdx]);
                        int paddingSize = ParserCommon.GetNumPaddingBytes(dataSize, currAlignment);

                        AddTrivialDataElementToObjectFile(objFile, dataSize, tokens[dataDeclarationIdx + 1]);
                    }

                    // otherwise, we'd expect there to be another token after the data type.
                    // see if we can figure out the string length
                    else if (ParserCommon.IsStringDeclaration(tokens[dataDeclarationIdx]))
                    {
                        // if this is a string declaration, then get the original string data
                        string dataStr = ParserCommon.GetStringData(asmLine.Text);

                        // add the string data to the object file.
                        AddNonTrivialDataElementToObjectFile(objFile, tokens[dataDeclarationIdx], dataStr, asmLine.LineNum);

                        int dataSize = ParserCommon.DetermineNonTrivialDataLength(asmLine.LineNum,
                                                                                  tokens[dataDeclarationIdx],
                                                                                  dataStr);

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
                        int dataSize = ParserCommon.DetermineNonTrivialDataLength(asmLine.LineNum,
                                                                                  tokens[dataDeclarationIdx],
                                                                                  tokens[dataDeclarationIdx + 1]);

                        int paddingSize = ParserCommon.GetNumPaddingBytes(dataSize, currAlignment);

                        // fill the space and padding with zeroes.
                        for (int i = 0; i < dataSize + paddingSize; ++i)
                        {
                            objFile.AddDataElement((byte)0);
                        }
                    }
                }
                else
                {
                    throw new AssemblyException(asmLine.LineNum, "Expected data value after token " + tokens[dataDeclarationIdx]);
                }
            }

            else
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
                        throw new ArgumentException("Expected 8-bit value, received " + elemValue);
                    }

                    objFile.AddDataElement(val);
                    break;
                }

                case SHORT_DATA_SIZE:
                {
                    short val = 0;
                    if (!short.TryParse(elemValue, out val))
                    {
                        throw new ArgumentException("Expected 16-bit value, received " + elemValue);
                    }

                    objFile.AddDataElement(val);
                    break;
                }

                case WORD_DATA_SIZE:
                {
                    int val = 0;
                    if (!int.TryParse(elemValue, out val))
                    {
                        throw new ArgumentException("Expected 32-bit value, received " + elemValue);
                    }

                    objFile.AddDataElement(val);
                    break;
                }

                case DWORD_DATA_SIZE:
                {
                    long val = 0;
                    if (!long.TryParse(elemValue, out val))
                    {
                        throw new ArgumentException("Expected 64-bit value, received " + elemValue);
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
        private void AddNonTrivialDataElementToObjectFile(BasicObjectFile objFile, string dataType, string elemValue, int lineNum)
        {
            if (dataType == ".ascii")
            {
                string fixedString = elemValue.Substring(1, elemValue.LastIndexOf('\"') - 1);
                objFile.AddAsciiString(fixedString);
            }

            else if (dataType == ".asciiz")
            {
                string fixedString = elemValue.Substring(1, elemValue.LastIndexOf('\"') - 1);
                objFile.AddNullTerminatedAsciiString(fixedString);
            }

            else if (dataType == ".space")
            {
                int numReservedSpace = ParserCommon.DetermineNonTrivialDataLength(lineNum, dataType, elemValue);
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
    }
}
