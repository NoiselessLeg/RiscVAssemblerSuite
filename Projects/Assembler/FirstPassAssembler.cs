using Assembler.FirstPassSegmentParsing;
using Assembler.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assembler
{
    /// <summary>
    /// Class that builds the symbol table
    /// </summary>
    internal class FirstPassAssembler
    {
        /// <summary>
        /// Creates an instance of the first pass assembler, which determines
        /// the locations of labels and generates a symbol table.
        /// </summary>
        /// <param name="baseTextAddress">The base .text segment address</param>
        /// <param name="baseDataAddress">The base .data segment address</param>
        public FirstPassAssembler(int baseTextAddress, int baseDataAddress)
        {
            m_SegmentHelper = new SegmentTypeHelper(baseTextAddress, baseDataAddress);
        }

        /// <summary>
        /// Reads the file, finds all of the symbols that need an address attached to them,
        /// and maps them to their address.
        /// </summary>
        /// <param name="reader">A StreamReader instance that will read the input assembly file.</param>
        /// <returns>A SymbolTable consisting of Labels, which in turn contain the names of symbols, their addresses,
        /// and what segment they were found in.</returns>
        public SymbolTable GenerateSymbolTable(StreamReader reader)
        {
            var symbolTable = new SymbolTable();
            SegmentType currSegmentType = SegmentType.Invalid;

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

                    // ignore blank lines. trim should remove all whitespace
                    if (line.Any() && !ParserCommon.IsCommentedLine(line))
                    {
                        // tokenize the line;
                        string[] tokens = line.Split(' ');

                        // if we see a non-segment declaration token,
                        // and we're looking for one, flag it.
                        if (!SegmentTypeHelper.IsSegmentDeclarationToken(tokens[0]) &&
                            currSegmentType == SegmentType.Invalid)
                        {
                            throw new AssemblyException(lineNum, "Unexpected line \"" + line + "\" found in non-segmented area.");
                        }

                        // otherwise, if we're looking for a segment definition, and we find one,
                        // then set the current segment type to the segment type.
                        else if (SegmentTypeHelper.IsSegmentDeclarationToken(tokens[0]) &&
                                currSegmentType == SegmentType.Invalid)
                        {
                            currSegmentType = SegmentTypeHelper.GetSegmentType(tokens[0]);
                        }

                        // segment parsers should set the current segment type when they reach the end
                        // of their processing. so the above should only be ran on the first couple of assembly lines.

                        ISegmentParser segParser = m_SegmentHelper.GetParserForSegment(lineNum, currSegmentType);
                        var asmLine = new LineData(line, lineNum);
                        currSegmentType = segParser.ParseLineInSegment(asmLine, symbolTable);
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

            return symbolTable;
        }

        private readonly SegmentTypeHelper m_SegmentHelper;
    }
   
}
