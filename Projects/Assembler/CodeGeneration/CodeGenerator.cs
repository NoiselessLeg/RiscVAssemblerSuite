using Assembler.Common;
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
    class CodeGenerator
    {
        public CodeGenerator(SymbolTable symTable)
        {
            m_CodeGenFac = new CodeGeneratorFactory(symTable);
        }

        /// <summary>
        /// Populates an existing symbol table with symbols parsed from the desired segment.
        /// </summary>
        /// <param name="reader">A StreamReader instance that will read the input assembly file.</param>
        /// <param name="desiredSegment">The program segment to parse symbols from.</param>
        /// <param name="symTable">The SymbolTable instance to populate.</param>
        public void GenerateCode(StreamReader reader, SymbolTable symTable, BasicObjectFile objFile)
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
                        
                        // otherwise, if we're looking for a segment definition, and we find one,
                        // then set the current segment type to the segment type.
                        if (SegmentTypeHelper.IsSegmentDeclarationToken(tokens[0]))
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
                                int alignmentParam = 0;
                                if (int.TryParse(tokens[1], out alignmentParam))
                                {
                                    currAlignment = ParserCommon.GetNewAlignment(alignmentParam);
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

                        // if our segment type is valid, then we're processing actual data versus an assembler directive.
                        else if (currSegmentType != SegmentType.Invalid)
                        {
                            ISegmentCodeGenerator codeGen = m_CodeGenFac.GetCodeGeneratorForSegment(currSegmentType);
                            var asmLine = new LineData(line, lineNum);
                            codeGen.GenerateCodeForSegment(asmLine, objFile, currAlignment);
                        }

                        // otherwise, tthis isn't a directive, and we're seeing stuff that should be under a specified
                        // segment type. stop doing what we're doing.
                        else
                        {
                            throw new AssemblyException(lineNum, "Unexpected line \"" + line + "\" found in non-segmented area.");
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

        private readonly CodeGeneratorFactory m_CodeGenFac;
    }
}
