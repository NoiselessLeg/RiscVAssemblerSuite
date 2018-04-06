using Assembler.Common;
using Assembler.InstructionProcessing;
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
        /// <param name="logger">The logging implementation to use.</param>
        /// <param name="procFactory">The instruction processor factory to retrieve instruction size estimator implementations from.</param>
        public SymbolTableBuilder(ILogger logger, InstructionProcessorFactory procFac)
        {
            m_Logger = logger;
            m_SymbolBuilderFac = new SegmentSymbolParserFactory(procFac);
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
                        LineParseResults directiveResults = ParserCommon.HandleAssemblerDirective(line, lineNum, currSegmentType, currAlignment);
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
                        LineParseResults directiveResults = ParserCommon.HandleAssemblerDirective(line, lineNum, currSegmentType, currAlignment);
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
        /// Tries to handle any linkage declarations 
        /// </summary>
        /// <param name="trimmedLine">The line with leading/trailing whitespace removed.</param>
        /// <param name="lineNum">The current line number.</param>
        /// <param name="segType">The current segment type.</param>
        /// <param name="currAlignment">The current boundary alignment.</param>
        /// <returns>A structure representing the new alignment (if any), the new segment type (if any), and a boolean representing
        /// if any preprocessor directive was parsed at all</returns>
        private bool TryHandlingLinkageDeclaration(string trimmedLine, int lineNum, SegmentType segType, int currAlignment)
        {
            // tokenize the line;
            string[] tokens = trimmedLine.Split(' ');
            int alignment = currAlignment;
            SegmentType newSegType = segType;
            bool isLinkageDec = false;

            if (IsLinkageDeclaration(tokens[0]))
            {
                isLinkageDec = true;

                // we expect three tokens,
                if (tokens[0] == ".extern" && tokens.Length != 3)
                {
                }

            }

            return isLinkageDec;
        }

        /// <summary>
        /// Determines if a token is a linkage token (e.g. ".extern").
        /// </summary>
        /// <param name="token">The token to examine.</param>
        /// <returns>True if the token is a linkage declaration; otherwise returns false.</returns>
        private bool IsLinkageDeclaration(string token)
        {
            bool isLinkageDec = false;
            if (token == ".extern" || token == ".globl")
            {
                isLinkageDec = true;
            }

            return isLinkageDec;
        }

        private readonly ILogger m_Logger;
        private readonly SegmentSymbolParserFactory m_SymbolBuilderFac;
    }

}
