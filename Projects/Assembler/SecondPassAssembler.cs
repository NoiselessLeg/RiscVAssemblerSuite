using Assembler.Parsers;
using Assembler.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Assembler
{
    /// <summary>
    /// Class that generates the code.
    /// </summary>
    internal class SecondPassAssembler
    {
        /// <summary>
        /// Creates an instance of the second-pass assembler,
        /// which generates the code in the .text segment.
        /// </summary>
        /// <param name="symbolTable">The symbol table generated
        /// by the first-pass parser.</param>
        /// <param name="baseTextAddress">The base .text segment address.</param>
        public SecondPassAssembler(SymbolTable symbolTable, int baseTextAddress)
        {
            m_ParserFac = new InstructionParserFactory(symbolTable);
            m_TextAddress = baseTextAddress;
        }

        /// <summary>
        /// Reads the file, and generates code for all instructions in the file.
        /// </summary>
        /// <param name="reader">A StreamReader instance that will read the input assembly file.</param>
        /// <returns>An IEnumerable of integers representing binary instructions.</returns>
        public IEnumerable<int> GenerateCode(StreamReader reader)
        {
            var instructionList = new List<int>();
            // a list of all exceptions we encounter during parsing.
            // users can view them all at once instead of working through them piecemeal.
            var exceptionList = new List<AssemblyException>();
            int lineNum = 0;
            bool inTextSegment = false;
            while (!reader.EndOfStream)
            {
                try
                {
                    try
                    {
                        ++lineNum;
                        // trim the whitespace around any read-in line.
                        string line = reader.ReadLine().Trim();

                        // ignore blank lines. trim should remove all whitespace
                        // ignore comments
                        if (line.Any() && !ParserCommon.IsCommentedLine(line))
                        {
                            // if we're not in the .text segment, chew through lines until we're there.
                            if (!inTextSegment)
                            {
                                inTextSegment = IsTextSegmentDeclaration(line);
                            }
                            else
                            {
                                // scan to the first instruction.
                                // this could share the same line as a label, so split on ':' and ','
                                string[] tokenizedStr = line.Split(new char[] { ',', ':', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                bool foundInstruction = false;

                                var instructionParams = new List<string>();
                                string instructionToken = string.Empty;
                                for (int i = 0; i < tokenizedStr.Length && !foundInstruction; ++i)
                                {
                                    string token = tokenizedStr[i].Trim();

                                    // we found our instruction. build a string from this token
                                    // to the end of the array.
                                    if (m_ParserFac.IsInstruction(token))
                                    {
                                        foundInstruction = true;
                                        instructionToken = token;

                                        // read to the end of our string and append the operands of the instruction.
                                        for (int k = i + 1; k < tokenizedStr.Length; ++k)
                                        {
                                            instructionParams.Add(tokenizedStr[k].Trim());
                                        }
                                    }
                                }

                                if (foundInstruction)
                                {
                                    // find the parser for the instruction.
                                    IParser parser = m_ParserFac.GetParserForInstruction(instructionToken);

                                    // delegate to the parser to parse our instruction.
                                    IEnumerable<int> generatedInstructions = parser.ParseInstruction(m_TextAddress, instructionParams.ToArray());
                                    instructionList.AddRange(generatedInstructions);

                                    // increment our current instruction address by 4 * generatedInstructions.size()
                                    // since each instruction is 4 bytes.
                                    const int INSTRUCTION_SIZE_BYTES = 4;
                                    m_TextAddress += (generatedInstructions.Count() * INSTRUCTION_SIZE_BYTES);
                                }
                            }
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        throw new AssemblyException(lineNum, ex.Message);
                    }
                }
                catch (AssemblyException ex)
                {
                    exceptionList.Add(ex);
                }

            }

            // if any exceptions were encountered, throw an aggregate exception with
            // all of the encountered exceptions.
            if (exceptionList.Any())
            {
                throw new AggregateException(exceptionList);
            }

            return instructionList;
        }

        /// <summary>
        /// Determines if a line is declaring the start of a .text segment.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private bool IsTextSegmentDeclaration(string line)
        {
            return line == ".text";
        }

        private readonly InstructionParserFactory m_ParserFac;
        private int m_TextAddress;
    }
}
