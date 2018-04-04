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
                        LineParseResults directiveResults = ParserCommon.HandlePreprocessorDeclarations(line, lineNum, currSegmentType, currAlignment);
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
                        LineParseResults directiveResults = ParserCommon.HandlePreprocessorDeclarations(line, lineNum, currSegmentType, currAlignment);
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


        private readonly ILogger m_Logger;
        private readonly SegmentSymbolParserFactory m_SymbolBuilderFac;
    }

}
