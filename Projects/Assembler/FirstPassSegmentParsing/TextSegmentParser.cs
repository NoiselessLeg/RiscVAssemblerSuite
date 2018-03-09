using Assembler.FirstPassSegmentParsing.InstructionByteSizes;
using Assembler.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assembler.FirstPassSegmentParsing
{
    class TextSegmentParser : ISegmentParser
    {
        /// <summary>
        /// Creates a new instance of a text segment parser
        /// TODO: determine if signedness/unsignedness has any tangible effect
        /// on code generation.
        /// </summary>
        public TextSegmentParser(int baseTextAddress)
        {
            m_CurrTextAddress = baseTextAddress;
        }

        /// <summary>
        /// Reads a line in a .text segment of a program, and adds any found symbols to the
        /// symbol table.
        /// </summary>
        /// <param name="asmLine">The line of assembly code to parse.</param>
        /// <param name="symbolList">The list of symbols that will be added to.</param>
        /// <returns>If the parser is not finished parsing the segment, returns the same segment type. If another
        /// segment declaration was found while parsing, returns the new segment type. Returns Invalid if the EOF was hit, or an
        /// invalid/unsupported segment was found.</returns>
        public SegmentType ParseLineInSegment(LineData asmLine, SymbolTable symbolList)
        {
            SegmentType currSegmentType = SegmentType.Text;

            if (asmLine.Text.Any() && !ParserCommon.IsCommentedLine(asmLine.Text))
            {
                string[] tokens = asmLine.Text.Split(' ');

                // if this line contains a declaration of a new segment, return to the caller.
                if (SegmentTypeHelper.IsSegmentDeclarationToken(tokens[0]))
                {
                    // get the
                    currSegmentType = SegmentTypeHelper.GetSegmentType(tokens[0]);
                }
                else
                {
                    // a label should end with a ':' character.
                    // this is OK if there's trash and no real assembly at this point,
                    // as the second pass code generator will flag it.
                    // we're just here to get symbols and addresses.
                    if (ParserCommon.ContainsLabel(tokens[0]))
                    {
                        string labelName = ParserCommon.ExtractLabel(tokens[0]);
                        var label = new Label(labelName, SegmentType.Text, m_CurrTextAddress);
                        symbolList.AddSymbol(label);

                        // determine if there are any instructions on this line.
                        string[] subTokens = tokens[0].Split(':');

                        // if we have more than one subtoken, then there is more than just a label on this line.
                        // increment the number of words in the segment (since we're assuming whatever is on the right-hand side
                        // is an instruction) by however many bytes the instruction is
                        if (subTokens.Length > 1)
                        {
                            m_CurrTextAddress += GetNumInstructionBytes(asmLine.LineNum, asmLine.Text);
                        }
                    }

                    //TODO: need to find some way to use linkage modifiers (e.g. .global, etc.)

                    // if this doesn't have a label, and is not empty or a comment,
                    // then this is an instruction. increment the counter.
                    else
                    {
                        m_CurrTextAddress += GetNumInstructionBytes(asmLine.LineNum, asmLine.Text);
                    }
                }
            }
            
            return currSegmentType;
        }

        /// <summary>
        /// Parses an instruction and determines how many bytes it may take up.
        /// TODO: lots of this functionality is copied from the SecondPassParser.
        /// Can we refactor this at some point?
        /// </summary>
        /// <returns>The size of the instruction, in bytes.</returns>
        private int GetNumInstructionBytes(int lineNum, string trimmedLine)
        {
            // scan to the first instruction.
            // this could share the same line as a label, so split on ':' and ','
            string[] tokenizedStr = trimmedLine.Split(new char[] { ',', ':', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            bool foundInstruction = false;

            var instructionParams = new List<string>();
            string instructionToken = string.Empty;
            for (int i = 0; i < tokenizedStr.Length && !foundInstruction; ++i)
            {
                string token = tokenizedStr[i].Trim();

                // we found our instruction. build a string from this token
                // to the end of the array.
                if (IsInstruction(token))
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
                // delegate to the measure implementation to figure out how many bytes the instruction
                // takes up in the .text segment.
                IInstructionByteMeasure measure = GetInstructionSizeMeasure(instructionToken);
                return measure.GetNumInstructionBytes(instructionParams.ToArray());
            }
            else
            {
                // otherwise, this was not an instruction, but garbage.
                throw new AssemblyException(lineNum, "Expected instruction; received \"" + trimmedLine + '\"');
            }
        }

        //**********************************************
        // TODO: everything below here needs implmented to eventually support variable-size instructions.
        // We REALLY need some way to synchronize what our second pass parser produces instruction-wise,
        // to what byte size we think our instructions are producing here. Desync could be really
        // bad and lead to miscalculated relative offsets. This is more of a workaround for the time being
        // to get something working at least...
        //**********************************************
        
        /// <summary>
        /// Determines if this token is an instruction.
        /// </summary>
        /// <param name="instructionName">The token to examine.</param>
        /// <returns>True if the token is a recognized instruction.</returns>
        private bool IsInstruction(string instructionName)
        {
            return true;
        }

        /// <summary>
        /// Gets the instruction byte size measure implementation for a particular instruction.
        /// </summary>
        /// <param name="instructionName">The instruction to fetch the byte size of.</param>
        /// <returns>Currently only returns the basic instruction byte measure, which in turn always returns
        /// the standard instruction size (4 bytes).</returns>
        private IInstructionByteMeasure GetInstructionSizeMeasure(string instructionName)
        {
            return new BaseInstructionByteMeasure();
        }
        
        private int m_CurrTextAddress;
    }
}
