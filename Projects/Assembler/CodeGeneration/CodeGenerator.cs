using Assembler.Common;
using Assembler.InstructionProcessing;
using Assembler.Output;
using Assembler.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.CodeGeneration
{
    /// <summary>
    /// Class that generates code for an assembly file.
    /// </summary>
    class CodeGenerator
    {
        /// <summary>
        /// Creates a new instance of a CodeGenerator.
        /// </summary>
        /// <param name="logger">The logging implementation to use for logging warnings/information.</param>
        /// <param name="symTable">The symbol table to use to resolve references with.</param>
        /// <param name="procFactory">The instruction processor factory to retrieve code generator implementations from.</param>
        public CodeGenerator(ILogger logger, SymbolTable symTable, InstructionProcessorFactory procFac)
        {
            m_CodeGenFac = new CodeGeneratorFactory(logger, symTable, procFac);
        }

        /// <summary>
        /// Generates code for the given assembly file.
        /// </summary>
        /// <param name="reader">A StreamReader instance that will read the input assembly file.</param>
        /// <param name="objFile">The basic object file that will be written to.</param>
        public void GenerateCode(StreamReader reader, BasicObjectFile objFile)
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

                        // if our segment type is valid, then we're processing actual data versus an assembler directive.
                        if (!directiveResults.IsLineAssemblerDirective &&
                            currSegmentType != SegmentType.Invalid)
                        {
                            if (!TryHandlingLinkageDeclaration(line, lineNum, objFile))
                            {
                                ISegmentCodeGenerator codeGen = m_CodeGenFac.GetCodeGeneratorForSegment(currSegmentType);
                                var asmLine = new LineData(line, lineNum);
                                try
                                {
                                    codeGen.GenerateCodeForSegment(asmLine, objFile, currAlignment);
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
        /// <param name="objFile">The basic object file that will be written to.</param>
        /// <returns>Returns true if a linkage directive was processed in this line. Otherwise, returns false.</returns>
        private bool TryHandlingLinkageDeclaration(string trimmedLine, int lineNum, BasicObjectFile objFile)
        {
            // tokenize the line;
            string[] tokens = trimmedLine.Split(' ');
            bool isLinkageDec = false;

            if (IsLinkageDeclaration(tokens[0]))
            {
                isLinkageDec = true;

                // we expect three tokens,
                if (tokens[0] == ".extern")
                {
                    int declarationSize = 0;
                    if (tokens.Length != 3)
                    {
                        throw new AssemblyException(lineNum, "Expected symbol name and byte size declaration after .extern token.");
                    }
                    else if (!int.TryParse(tokens[2], out declarationSize))
                    {
                        throw new AssemblyException(lineNum, ".extern requires a non-negative 32-bit integer size.");
                    }
                    else if (declarationSize < 0)
                    {
                        throw new AssemblyException(lineNum, ".extern requires a non-negative 32-bit integer size.");
                    }
                    else
                    {
                        objFile.AddExternElement(declarationSize);
                    }
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

        private readonly CodeGeneratorFactory m_CodeGenFac;
    }
}
