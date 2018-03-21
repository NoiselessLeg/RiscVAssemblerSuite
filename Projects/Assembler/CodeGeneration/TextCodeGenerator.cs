﻿using Assembler.Common;
using Assembler.CodeGeneration.InstructionGenerators;
using Assembler.Output;
using Assembler.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Assembler.CodeGeneration
{
    /// <summary>
    /// Class that generates the code.
    /// </summary>
    class TextCodeGenerator : ISegmentCodeGenerator
    {
        /// <summary>
        /// Creates an instance of the second-pass assembler,
        /// which generates the code in the .text segment.
        /// </summary>
        /// <param name="symbolTable">The symbol table generated by the first-pass parser.</param>
        public TextCodeGenerator(SymbolTable symbolTable)
        {
            m_ParserFac = new InstructionGeneratorFactory(symbolTable);
            m_CurrTextAddress = CommonConstants.BASE_TEXT_ADDRESS;

            // precalculate the nop instruction byte, since this should always be the same.
            IEnumerable<int> nopVals = new NopInstructionParser().ParseInstruction(0, new string[] { });
            System.Diagnostics.Debug.Assert(nopVals.Count() == 1);
            m_PrecalculatedNopInstruction = nopVals.ElementAt(0);
        }

        /// <summary>
        /// Reads the file, and generates code for all instructions in the file.
        /// </summary>
        /// <param name="reader">A StreamReader instance that will read the input assembly file.</param>
        /// <returns>An IEnumerable of integers representing binary instructions.</returns>
        public void GenerateCodeForSegment(LineData asmLine, BasicObjectFile objFile, int currAlignment)
        {
            // scan to the first instruction.
            // this could share the same line as a label, so split on ':' and ','
            string[] tokenizedStr = asmLine.Text.Split(new char[] { ',', ':', ' ' }, StringSplitOptions.RemoveEmptyEntries);
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

                // first, calculate the address of the next theoretical instruction while factoring in padding.
                int paddingSize = ParserCommon.GetNumPaddingBytes(CommonConstants.BASE_INSTRUCTION_SIZE_BYTES, currAlignment);

                // padding should always be a multiple of the instruction size.
                System.Diagnostics.Debug.Assert(paddingSize % CommonConstants.BASE_INSTRUCTION_SIZE_BYTES == 0);
                int nextInstructionAddress = m_CurrTextAddress + CommonConstants.BASE_INSTRUCTION_SIZE_BYTES + paddingSize;

                // beq instructions should (hopefully) not generate multiple instructions..
                IEnumerable<int> generatedInstructions = parser.ParseInstruction(nextInstructionAddress, instructionParams.ToArray());

                foreach (int generatedInstruction in generatedInstructions)
                {
                    objFile.AddInstruction(generatedInstruction);
                    m_CurrTextAddress += CommonConstants.BASE_INSTRUCTION_SIZE_BYTES;

                    // if padding is required at this point, insert a NOP instruction here.
                    for (int i = 0; i < paddingSize; i += CommonConstants.BASE_INSTRUCTION_SIZE_BYTES)
                    {
                        objFile.AddInstruction(m_PrecalculatedNopInstruction);
                        m_CurrTextAddress += CommonConstants.BASE_INSTRUCTION_SIZE_BYTES;
                    }

                }
               
            }
        }

        private readonly InstructionGeneratorFactory m_ParserFac;
        private readonly int m_PrecalculatedNopInstruction;
        private int m_CurrTextAddress;
    }
}