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

                    // get a substring up until the commented line, ignoring those in user quotes.
                    line = ParserCommon.GetSanitizedString(line);

                    // ignore blank lines. trim should remove all whitespace
                    if (line.Any())
                    {
                        LineParseResults directiveResults = HandlePreprocessorDeclarations(line, lineNum, currSegmentType, currAlignment);
                        currAlignment = directiveResults.NewAlignment;
                        currSegmentType = directiveResults.NewSegment;

                        // further processing is needed, and the current segment type is the desired segment type.
                        if (!directiveResults.IsLineAssemblerDirective &&
                            currSegmentType == desiredSegment)
                        {
                            ISymbolTableBuilder segParser = m_SymbolBuilderFac.GetParserForSegment(lineNum, currSegmentType);
                            var asmLine = new LineData(line, lineNum);
                            try
                            {
                                segParser.ParseSymbolsInLine(asmLine, symTable, currAlignment);
                            }
                            catch (Exception ex)
                            {
                                throw new AssemblyException(lineNum, ex.Message);
                            }
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

        /// <summary>
        /// Populates an existing symbol table with symbols parsed from the whole file.
        /// </summary>
        /// <param name="reader">A StreamReader instance that will read the input assembly file.</param>
        /// <param name="symTable">The SymbolTable instance to populate.</param>
        public void GenerateSymbolTable(StreamReader reader, SymbolTable symTable)
        {
            SegmentType currSegmentType = SegmentType.Invalid;
            int currAlignment = CommonConstants.DEFAULT_ALIGNMENT;
            int currDataAlignment = currAlignment;

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
                    
                    // get a substring up until the commented line, ignoring those in user quotes.
                    line = ParserCommon.GetSanitizedString(line);

                    // ignore blank lines. trim should remove all whitespace
                    if (line.Any())
                    {
                        LineParseResults directiveResults = HandlePreprocessorDeclarations(line, lineNum, currSegmentType, currAlignment);
                        currAlignment = directiveResults.NewAlignment;
                        currSegmentType = directiveResults.NewSegment;

                        // further processing is needed
                        if (!directiveResults.IsLineAssemblerDirective)
                        {
                            ISymbolTableBuilder segParser = m_SymbolBuilderFac.GetParserForSegment(lineNum, currSegmentType);
                            var asmLine = new LineData(line, lineNum);
                            try
                            {
                                segParser.ParseSymbolsInLine(asmLine, symTable, currAlignment);
                            }
                            catch (AssemblyException)
                            {
                                throw;
                            }
                            catch (Exception ex)
                            {
                                throw new AssemblyException(lineNum, ex.Message);
                            }
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

        /// <summary>
        /// Handles processing of any preprocessor directive in the line, if any.
        /// </summary>
        /// <param name="trimmedLine">The line with leading/trailing whitespace removed.</param>
        /// <param name="lineNum">The current line number.</param>
        /// <param name="segType">The current segment type.</param>
        /// <param name="currAlignment">The current boundary alignment.</param>
        /// <returns>A structure representing the new alignment (if any), the new segment type (if any), and a boolean representing
        /// if any preprocessor directive was parsed at all</returns>
        private LineParseResults HandlePreprocessorDeclarations(string trimmedLine, int lineNum, SegmentType segType, int currAlignment)
        {
            // tokenize the line;
            string[] tokens = trimmedLine.Split(' ');
            int alignment = currAlignment;
            SegmentType newSegType = segType;
            bool isDirective = false;

            // if we see something that's not a directive, and we're not in a valid
            // segment type, flag an exception.
            if (!SegmentTypeHelper.IsSegmentDeclarationToken(tokens[0]) &&
                !ParserCommon.IsAlignmentDeclaration(tokens[0]) &&
                segType == SegmentType.Invalid)
            {
                throw new AssemblyException(lineNum, "Unexpected line \"" + trimmedLine + "\" found in non-segmented area.");
            }

            // otherwise, if we're looking for a segment definition, and we find one,
            // then set the current segment type to the segment type.
            else if (SegmentTypeHelper.IsSegmentDeclarationToken(tokens[0]))
            {
                newSegType = SegmentTypeHelper.GetSegmentType(tokens[0]);
                isDirective = true;
            }

            // if this segment is declaring a new alignment, get that and return to caller.
            else if (ParserCommon.IsAlignmentDeclaration(tokens[0]))
            {
                if (segType == SegmentType.Text)
                {
                    throw new AssemblyException(lineNum, ".align declaration is forbidden in .text segment.");
                }

                // we'd expect there to be multiple tokens.
                if (tokens.Length > 1)
                {
                    // get the next token, and try to convert it to an alignment parameter.
                    int tokenizedAlign = 0;
                    if (int.TryParse(tokens[1], out tokenizedAlign))
                    {
                        alignment = ParserCommon.GetNewAlignment(tokenizedAlign);
                        isDirective = true;
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

            return new LineParseResults(isDirective, alignment, newSegType);
        }

        private readonly SegmentSymbolParserFactory m_SymbolBuilderFac;
    }

}
