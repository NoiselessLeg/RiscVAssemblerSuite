using Assembler.Util;
using System;
using System.Collections.Generic;

namespace Assembler.Parsers
{
    /// <summary>
    /// Psuedo-instruction for beq x0, rs, label
    /// </summary>
    class BeqzInstructionParser : IParser
    {
        public BeqzInstructionParser(SymbolTable symTbl)
        {
            m_SymTable = symTbl;
        }

        public IEnumerable<int> ParseInstruction(int currentTextAddress, string[] instructionArgs)
        {
            if (instructionArgs.Length != 2)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 2, received " + instructionArgs.Length + '.');
            }

            var beqParser = new BeqInstructionParser(m_SymTable);
            return beqParser.ParseInstruction(currentTextAddress, new string[] { "x0", instructionArgs[0], instructionArgs[1] });
        }

        private readonly SymbolTable m_SymTable;
    }
}
