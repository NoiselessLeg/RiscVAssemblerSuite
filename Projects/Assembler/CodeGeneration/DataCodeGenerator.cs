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
                if (IsDataDeclaration(tokens[i]))
                {
                    foundDataDeclaration = true;
                    dataDeclarationIdx = i;
                }
            }

            if (foundDataDeclaration)
            {
                // if it is a trivial type, use our precomputed map to get the size.
                // otherwise, determine the string length.
                if (ParserCommon.IsTrivialDataType(tokens[dataDeclarationIdx]))
                {
                    // we'd expect there to be another token after the data size declaration.
                    if (tokens.Length > dataDeclarationIdx)
                    {
                        int dataSize = ParserCommon.DetermineTrivialDataSize(asmLine.LineNum, tokens[dataDeclarationIdx]);
                        AddTrivialDataElementToObjectFile(objFile, dataSize, tokens[dataDeclarationIdx + 1]);
                        int paddingSize = ParserCommon.GetNumPaddingBytes(dataSize, currAlignment);

                        // if our padding size is greater than 0, add padding bytes to meet the next alignment boundary.
                        for (int i = 0; i < paddingSize; ++i)
                        {
                            objFile.AddDataElement((byte)0);
                        }
                    }
                }

                // otherwise, we'd expect there to be another token after the data type.
                // see if we can figure out the string length
                else
                {
                    // we'd expect there to be another token after the data size declaration.
                    if (tokens.Length > dataDeclarationIdx)
                    {
                        int dataSize = ParserCommon.DetermineNonTrivialDataLength(asmLine.LineNum, 
                                                                                  tokens[dataDeclarationIdx], 
                                                                                  tokens[dataDeclarationIdx + 1]);

                        int paddingSize = ParserCommon.GetNumPaddingBytes(dataSize, alignment);
                    }
                }
            }

            else
            {
                throw new AssemblyException(asmLine.LineNum, "Unable to ascertain data type from line " + asmLine.Text);
            }
        }

        private bool IsDataDeclaration(string token)
        {
            return  ParserCommon.IsTrivialDataType(token) ||
                    token == ".ascii" ||
                    token == ".asciiz" ||
                    token == ".space";
        }

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


    }
}
