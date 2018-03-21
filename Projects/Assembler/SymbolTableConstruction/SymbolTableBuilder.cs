using Assembler.Common;
using Assembler.SymbolTableConstruction.SymbolBuilders;
using Assembler.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assembler.SymbolTableConstruction
{
    /// <summary>
    /// Class that builds the symbol table
    /// </summary>
    class SymbolTableBuilder
    {
        /// <summary>
        /// Creates an instance of the first pass assembler, which determines
        /// the locations of labels and generates a symbol table.
        /// </summary>
        public SymbolTableBuilder()
        {
            m_SymbolBuilderFac = new SegmentSymbolParserFactory();
        }

        /// <summary>
        /// Populates an existing symbol table with symbols parsed from the desired segment.
        /// </summary>
        /// <param name="reader">A StreamReader instance that will read the input assembly file.</param>
        /// <param name="desiredSegment">The program segment to parse symbols from.</param>
        /// <param name="symTable">The SymbolTable instance to populate.</param>
        public void GenerateSymbolTableForSegment(StreamReader reader, SegmentType desiredSegment, SymbolTable symTable)
        {
            SegmentType currSegmentType = SegmentType.Invalid;
            int currAlignment = CommonConstants.DEFAULT_ALIGNMENT;

            // a list of all exceptions we encounter during parsing.
            // users can view them all at once instead of working through them piecemeal.
            var exceptionList = new List<AssemblyException>();
            int lineNum = 0;
            while (!reader.EndOfStream)
            {
                try
                {
                    ++lineNum;
                    // trim the whitespace from any read-in line.
                    string line = reader.ReadLine().Trim();

                    // get a substring up until the commented line.
                    if (line.Contains('#'))
                    {
                        line = line.Substring(0, line.IndexOf('#'));
                    }

                    // ignore blank lines. trim should remove all whitespace
                    if (line.Any())
                    {
                        // tokenize the line;
                        string[] tokens = line.Split(' ');

                        // if we see something that's not a directive, and we're not in a valid
                        // segment type, flag an exception.
                        if (!SegmentTypeHelper.IsSegmentDeclarationToken(tokens[0]) &&
                            !ParserCommon.IsAlignmentDeclaration(tokens[0]) &&
                            currSegmentType == SegmentType.Invalid)
                        {
                            throw new AssemblyException(lineNum, "Unexpected line \"" + line + "\" found in non-segmented area.");
                        }

                        // otherwise, if we're looking for a segment definition, and we find one,
                        // then set the current segment type to the segment type.
                        else if (SegmentTypeHelper.IsSegmentDeclarationToken(tokens[0]))
                        {
                            currSegmentType = SegmentTypeHelper.GetSegmentType(tokens[0]);
                        }

                        // if this segment is declaring a new alignment, get that and return to caller.
                        else if (ParserCommon.IsAlignmentDeclaration(tokens[0]))
                        {
                            // we'd expect there to be multiple tokens.
                            if (tokens.Length > 1)
                            {
                                // get the next token, and try to convert it to an alignment parameter.
                                int tokenizedAlign = 0;
                                if (int.TryParse(tokens[1], out tokenizedAlign))
                                {
                                    currAlignment = ParserCommon.GetNewAlignment(tokenizedAlign);
                                }
                                else
                                {
                                    throw new AssemblyException(lineNum, "Expected integer token after .align declaration.");
                                }
                            }
                            else
                            {
                                throw new AssemblyException(lineNum, "Expected integer token after .align declaration.");
                            }
                        }

                        else if (currSegmentType == desiredSegment)
                        {
                            ISymbolTableBuilder segParser = m_SymbolBuilderFac.GetParserForSegment(lineNum, currSegmentType);
                            var asmLine = new LineData(line, lineNum);
                            segParser.ParseSymbolsInLine(asmLine, symTable, currAlignment);
                        }
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

            // reset the StreamReader to the beginning position.
            reader.Seek(0, SeekOrigin.Begin);
        }

        private readonly SegmentSymbolParserFactory m_SymbolBuilderFac;
    }

}
