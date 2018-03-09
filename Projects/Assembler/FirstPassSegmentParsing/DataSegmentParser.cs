using Assembler.Util;
using System.Collections.Generic;
using System.Linq;

namespace Assembler.FirstPassSegmentParsing
{
    class DataSegmentParser : ISegmentParser
    {
        /// <summary>
        /// Creates a DataSegmentParser instance.
        /// TODO: determine if signedness/unsignedness has any tangible effect
        /// on code generation.
        /// </summary>
        public DataSegmentParser(int baseDataAddress)
        {
            m_DataTypeDictionary = new Dictionary<string, int>()
            {
                // NOTE: I have no idea what these actually are, yet.
                // these can and most likely will change.
                { ".byte", 1 },
                { ".half", 2 },
                { ".word", 4 },
                { ".dword", 8 }
            };
            
            m_CurrDataAddress = baseDataAddress;
        }

        /// <summary>
        /// Reads a denoted .data segment line of an assembly program.
        /// </summary>
        /// <param name="reader">The reader used to read the file.</param>
        /// <param name="symbolList">The list of symbols that will be added to.</param>
        /// <param name="startingLine">The line that the .data segment starts on. Will be incremented</param>
        /// <returns>If the parser is not finished parsing the segment, returns the same segment type. If another
        /// segment declaration was found while parsing, returns the new segment type. Returns Invalid if the EOF was hit, or an
        /// invalid segment was found.</returns>
        public SegmentType ParseLineInSegment(LineData asmLine, SymbolTable symbolList)
        {
            SegmentType currSegmentType = SegmentType.Data;

            if (asmLine.Text.Any() && !ParserCommon.IsCommentedLine(asmLine.Text))
            {
                string[] tokens = asmLine.Text.Split(' ');

                // if this line contains a declaration of a new segment, return to the caller.
                if (SegmentTypeHelper.IsSegmentDeclarationToken(tokens[0]))
                {
                    // get the new segment type, and make sure it's not the same as before.
                    currSegmentType = SegmentTypeHelper.GetSegmentType(tokens[0]);
                }
                else
                {
                    // a label should end with a ':' character.
                    if (ParserCommon.ContainsLabel(tokens[0]))
                    {
                        string labelName = ParserCommon.ExtractLabel(tokens[0]);
                        var label = new Label(labelName, SegmentType.Text, m_CurrDataAddress);
                        symbolList.AddSymbol(label);

                        // take the line that we read originally, and split it by the ':' character.
                        // scan it for a data size (e.g. .asciiz, .word, etc)
                        for (uint i = 1; i < tokens.Length; ++i)
                        {
                            if (tokens[i].StartsWith("."))
                            {
                                // if it is a trivial type, use our precomputed map to get the size.
                                // otherwise, determine the variable length.
                                if (IsTrivialDataType(tokens[i]))
                                {
                                    int dataSize = DetermineTrivialDataSize(asmLine.LineNum, tokens[i]);
                                    m_CurrDataAddress += dataSize;
                                    break;
                                }

                                // otherwise, we'd expect there to be another token after the data type.
                                // see if we can figure out the string length
                                else if (i < tokens.Length - 1)
                                {
                                    int dataSize = DetermineStringLength(asmLine.LineNum, tokens[i], tokens[i + 1]);
                                    m_CurrDataAddress += dataSize;
                                    break;
                                }

                                else
                                {
                                    throw new AssemblyException(asmLine.LineNum, "Unable to ascertain data type from " + tokens[i] + " token.");
                                }
                            }
                        }
                    }

                    // if this doesn't have a label, and is not empty or a comment,
                    // then this is a data element.
                    else
                    {
                        // take the line that we read originally, and split it by the ':' character.
                        // scan it for a data size (e.g. .asciiz, .word, etc)
                        for (uint i = 0; i < tokens.Length; ++i)
                        {
                            if (tokens[i].StartsWith("."))
                            {
                                // if it is a trivial type, use our precomputed map to get the size.
                                // otherwise, determine the string length.
                                if (IsTrivialDataType(tokens[i]))
                                {
                                    int dataSize = DetermineTrivialDataSize(asmLine.LineNum, tokens[i]);
                                    m_CurrDataAddress += dataSize;
                                    break;
                                }

                                // otherwise, we'd expect there to be another token after the data type.
                                // see if we can figure out the string length
                                else if (i < tokens.Length - 1)
                                {
                                    int dataSize = DetermineStringLength(asmLine.LineNum, tokens[i], tokens[i + 1]);
                                    m_CurrDataAddress += dataSize;
                                    break;
                                }

                                else
                                {
                                    throw new AssemblyException(asmLine.LineNum, "Unable to ascertain data type from " + tokens[i] + " token.");
                                }
                            }
                        }
                    }
                }
            }
            

            // return the amount of lines we read.
            return currSegmentType;
        }

        /// <summary>
        /// Determines if a data size is one of the basic types
        /// (e.g. .word, .byte). This returns false for .ascii/.asciiz types
        /// as their sizes are dependent on string length.
        /// </summary>
        /// <param name="token">The token to examine.</param>
        /// <returns>If the data size is trivial, this returns true. Otherwise returns false.</returns>
        private bool IsTrivialDataType(string token)
        {
            return m_DataTypeDictionary.ContainsKey(token);
        }

        /// <summary>
        /// Determines the size, in bytes, of a .ascii or .asciiz string.
        /// </summary>
        /// <param name="line">The line number the data type is found on.</param>
        /// <param name="dataTypeToken">The token describing the data type (e.g. ".ascii").</param>
        /// <param name="str">The token that immediately follows the data type, to examine.</param>
        /// <returns>The size of the string, or the size of the string + 1 if the data type is .asciiz (to account
        /// for the implicit 0 byte at the end).</returns>
        private int DetermineStringLength(int incrementingLineNum, string dataTypeToken, string str)
        {
            int dataSize = 0;
            // strip the beginning and ending quotations.
            string fixedString = str.Substring(1, str.LastIndexOf('\"') - 1);

            if (dataTypeToken == ".ascii")
            {
                dataSize = fixedString.Length;
            }
            else if (dataTypeToken == ".asciiz")
            {
                dataSize = fixedString.Length + 1;
            }
            else if (dataTypeToken == ".space")
            {
                // if this is a .space directive, then the token immediately following
                // should be how much space to reserve.
                dataSize = int.Parse(str);
            }
            else
            {
                throw new AssemblyException(incrementingLineNum, "Attempted to find string length of non-string data " + dataTypeToken);
            }

            return dataSize;
        }

        /// <summary>
        /// Determines the size of a given data type.
        /// </summary>
        /// <param name="line">The line the symbol occured on. For error handling usage.</param>
        /// <param name="token">The declaration containing the type.</param>
        /// <returns>The size of the type, in bytes.</returns>
        private int DetermineTrivialDataSize(int line, string token)
        {
            int dataSize = 0;
            if (!m_DataTypeDictionary.TryGetValue(token, out dataSize))
            {
                throw new AssemblyException(line, "Could not find data size with data type " + token);
            }

            return dataSize;
        }

        private readonly Dictionary<string, int> m_DataTypeDictionary;

        private int m_CurrDataAddress;
    }
}
