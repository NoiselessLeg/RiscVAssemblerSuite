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
                        int numElements = ParserCommon.GetArraySize(asmLine.Text, fixedTokens[dataDeclarationIdx]);

                        int totalReservedSize = dataSize * numElements;
                        int paddingSize = ParserCommon.GetNumPaddingBytes(totalReservedSize, currAlignment);

                        AddTrivialDataElementsToFile(objFile, dataSize, asmLine.Text, fixedTokens[dataDeclarationIdx]);

                        // add as much padding as we need to reach the next alignment boundary.
                        for (int i = 0; i < paddingSize; ++i)
                        {
                            objFile.AddDataElement((byte)0);
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
        /// Adds either one or multiple trivially sized data elements to the object file.
        /// </summary>
        /// <param name="objFile">The object file to add to.</param>
        /// <param name="dataSize">The data size of the element to add.</param>
        /// <param name="fullText">The full text of the assembly line.</param>
        /// <param name="declarationToken">The token declaring the size of the data.</param>
        private void AddTrivialDataElementsToFile(BasicObjectFile objFile, int dataSize, string fullText, string declarationToken)
        {
            const int BYTE_DATA_SIZE = 1;
            const int SHORT_DATA_SIZE = 2;
            const int WORD_DATA_SIZE = 4;
            const int DWORD_DATA_SIZE = 8;

            switch (dataSize)
            {
                case BYTE_DATA_SIZE:
                {
                    AddByteElementToFile(objFile, fullText, declarationToken);
                    break;
                }

                case SHORT_DATA_SIZE:
                {
                    AddShortElementToFile(objFile, fullText, declarationToken);
                    break;
                }

                case WORD_DATA_SIZE:
                {
                    AddIntElementToFile(objFile, fullText, declarationToken);
                    break;
                }

                case DWORD_DATA_SIZE:
                {
                    AddLongElementToFile(objFile, fullText, declarationToken);
                    break;
                }

                default:
                {
                    throw new ArgumentException("Unknown data size passed to AddTrivialDataElementToObjectFile");
                }
            }
        }

        /// <summary>
        /// Adds a byte element to the object file.
        /// </summary>
        /// <param name="objFile">The object file to add to.</param>
        /// <param name="fullText">The full text of the assembly line.</param>
        /// <param name="declarationToken">The token specifying the declaration of the size parameter.</param>
        private void AddByteElementToFile(BasicObjectFile objFile, string fullText, string declarationToken)
        {
            // find the token directly after the size directive
            int substrBeginIdx = fullText.IndexOf(declarationToken) + declarationToken.Length;
            string arguments = fullText.Substring(substrBeginIdx);

            // split by commas.
            string[] tokenizedArgs = arguments.Split(new[] { ',' });
            tokenizedArgs = tokenizedArgs.Apply((str) => str.Trim()).ToArray();

            // iterate through each element in the "array".
            foreach (string token in tokenizedArgs)
            {
                // if it contains a ':' character, then this itself is an array of the initialized token.
                if (token.Contains(':'))
                {
                    string[] subtokens = token.Split(new[] { ':' }).Apply((str) => str.Trim()).ToArray();
                    if (subtokens.Length == 2)
                    {
                        int numElems = int.Parse(subtokens[1]);

                        byte byteElem = 0;
                        // first, try to get the value as a byte.
                        if (!byte.TryParse(subtokens[0], out byteElem))
                        {
                            // if we fail, then try parsing the character as a literal.
                            if (!StringUtils.TryParseCharacterLiteralAsByte(subtokens[0], out byteElem))
                            {
                                // as a fallback, see if we can resolve the string as a symbol.
                                Symbol sym = m_SymTbl.GetSymbol(subtokens[0]);
                                byteElem = (byte)sym.Address;
                            }
                        }

                        for (int i = 0; i < numElems; ++i)
                        {
                            objFile.AddDataElement(byteElem);
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Expected size parameter after ':' token.");
                    }
                }
                else
                {
                    // otherwise, it should just be an element (without any size modifiers).
                    // just parse it and add it.
                    byte byteElem = 0;
                    // first, try to get the value as a byte.
                    if (!byte.TryParse(token, out byteElem))
                    {
                        // if we fail, then try parsing the character as a literal.
                        if (!StringUtils.TryParseCharacterLiteralAsByte(token, out byteElem))
                        {
                            // as a fallback, see if we can resolve the string as a symbol.
                            Symbol sym = m_SymTbl.GetSymbol(token);
                            byteElem = (byte)sym.Address;
                        }
                    }

                    objFile.AddDataElement(byteElem);
                }
            }
        }

        /// <summary>
        /// Adds a half element to the object file.
        /// </summary>
        /// <param name="objFile">The object file to add to.</param>
        /// <param name="fullText">The full text of the assembly line.</param>
        /// <param name="declarationToken">The token specifying the declaration of the size parameter.</param>
        private void AddShortElementToFile(BasicObjectFile objFile, string fullText, string declarationToken)
        {
            // find the token directly after the size directive
            int substrBeginIdx = fullText.IndexOf(declarationToken) + declarationToken.Length;
            string arguments = fullText.Substring(substrBeginIdx);

            // split by commas.
            string[] tokenizedArgs = arguments.Split(new[] { ',' });
            tokenizedArgs = tokenizedArgs.Apply((str) => str.Trim()).ToArray();

            // iterate through each element in the "array".
            foreach (string token in tokenizedArgs)
            {
                // if it contains a ':' character, then this itself is an array of the initialized token.
                if (token.Contains(':'))
                {
                    string[] subtokens = token.Split(new[] { ':' }).Apply((str) => str.Trim()).ToArray();
                    if (subtokens.Length == 2)
                    {
                        int numElems = int.Parse(subtokens[1]);

                        // this syntax is wonky; we're trying to parse literal char elements
                        // as well as normal bytes here.
                        short elemToAdd = 0;
                        if (!short.TryParse(subtokens[0], out elemToAdd))
                        {
                            // see if we can resolve the string as a symbol.
                            Symbol sym = m_SymTbl.GetSymbol(subtokens[0]);
                            elemToAdd = (short)sym.Address;
                        }
                        for (int i = 0; i < numElems; ++i)
                        {
                            objFile.AddDataElement(elemToAdd);
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Expected size parameter after ':' token.");
                    }
                }
                else
                {
                    // otherwise, it should just be an element (without any size modifiers).
                    // just parse it and add it.
                    short elemToAdd = 0;
                    if (!short.TryParse(token, out elemToAdd))
                    {
                        // see if we can resolve the string as a symbol.
                        Symbol sym = m_SymTbl.GetSymbol(token);
                        elemToAdd = (short)sym.Address;
                    }

                    objFile.AddDataElement(elemToAdd);
                }
            }
        }

        /// <summary>
        /// Adds a word element to the object file.
        /// </summary>
        /// <param name="objFile">The object file to add to.</param>
        /// <param name="fullText">The full text of the assembly line.</param>
        /// <param name="declarationToken">The token specifying the declaration of the size parameter.</param>
        private void AddIntElementToFile(BasicObjectFile objFile, string fullText, string declarationToken)
        {
            // find the token directly after the size directive
            int substrBeginIdx = fullText.IndexOf(declarationToken) + declarationToken.Length;
            string arguments = fullText.Substring(substrBeginIdx);

            // split by commas.
            string[] tokenizedArgs = arguments.Split(new[] { ',' });
            tokenizedArgs = tokenizedArgs.Apply((str) => str.Trim()).ToArray();

            // iterate through each element in the "array".
            foreach (string token in tokenizedArgs)
            {
                // if it contains a ':' character, then this itself is an array of the initialized token.
                if (token.Contains(':'))
                {
                    string[] subtokens = token.Split(new[] { ':' }).Apply((str) => str.Trim()).ToArray();
                    if (subtokens.Length == 2)
                    {
                        int numElems = int.Parse(subtokens[1]);

                        // this syntax is wonky; we're trying to parse literal char elements
                        // as well as normal bytes here.
                        int elemToAdd = 0;
                        if (!int.TryParse(subtokens[0], out elemToAdd))
                        {
                            // see if we can resolve the string as a symbol.
                            Symbol sym = m_SymTbl.GetSymbol(subtokens[0]);
                            elemToAdd = sym.Address;
                        }
                        for (int i = 0; i < numElems; ++i)
                        {
                            objFile.AddDataElement(elemToAdd);
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Expected size parameter after ':' token.");
                    }
                }
                else
                {
                    // otherwise, it should just be an element (without any size modifiers).
                    // just parse it and add it.
                    int elemToAdd = 0;
                    if (!int.TryParse(token, out elemToAdd))
                    {
                        // see if we can resolve the string as a symbol.
                        Symbol sym = m_SymTbl.GetSymbol(token);
                        elemToAdd = sym.Address;
                    }
                    objFile.AddDataElement(elemToAdd);
                }
            }
        }

        /// <summary>
        /// Adds a dword element to the object file.
        /// </summary>
        /// <param name="objFile">The object file to add to.</param>
        /// <param name="fullText">The full text of the assembly line.</param>
        /// <param name="declarationToken">The token specifying the declaration of the size parameter.</param>
        private void AddLongElementToFile(BasicObjectFile objFile, string fullText, string declarationToken)
        {
            // find the token directly after the size directive
            int substrBeginIdx = fullText.IndexOf(declarationToken) + declarationToken.Length;
            string arguments = fullText.Substring(substrBeginIdx);

            // split by commas.
            string[] tokenizedArgs = arguments.Split(new[] { ',' });
            tokenizedArgs = tokenizedArgs.Apply((str) => str.Trim()).ToArray();

            // iterate through each element in the "array".
            foreach (string token in tokenizedArgs)
            {
                // if it contains a ':' character, then this itself is an array of the initialized token.
                if (token.Contains(':'))
                {
                    string[] subtokens = token.Split(new[] { ':' }).Apply((str) => str.Trim()).ToArray();
                    if (subtokens.Length == 2)
                    {
                        int numElems = int.Parse(subtokens[1]);

                        long elemToAdd = 0;
                        if (!long.TryParse(subtokens[0], out elemToAdd))
                        {
                            // see if we can resolve the string as a symbol.
                            Symbol sym = m_SymTbl.GetSymbol(subtokens[0]);
                            elemToAdd = sym.Address;
                        }

                        for (int i = 0; i < numElems; ++i)
                        {
                            objFile.AddDataElement(elemToAdd);
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Expected size parameter after ':' token.");
                    }
                }
                else
                {
                    // otherwise, it should just be an element (without any size modifiers).
                    // just parse it and add it.
                    long elemToAdd = 0;
                    if (!long.TryParse(token, out elemToAdd))
                    {
                        // see if we can resolve the string as a symbol.
                        Symbol sym = m_SymTbl.GetSymbol(token);
                        elemToAdd = sym.Address;
                    }
                    objFile.AddDataElement(elemToAdd);
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
