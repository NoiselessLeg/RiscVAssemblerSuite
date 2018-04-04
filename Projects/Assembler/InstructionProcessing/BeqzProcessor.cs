﻿using Assembler.Common;
using Assembler.Util;
using System;
using System.Collections.Generic;

namespace Assembler.InstructionProcessing
{
    /// <summary>
    /// Psuedo-instruction for beq x0, rs, label
    /// </summary>
    class BeqzProcessor : BaseInstructionProcessor
    {
        public BeqzProcessor(SymbolTable symTbl)
        {
            m_SymTable = symTbl;
        }

        public override IEnumerable<int> GenerateCodeForInstruction(int address, string[] instructionArgs)
        {
            if (instructionArgs.Length != 2)
            {
                throw new ArgumentException("Invalid number of arguments provided. Expected 2, received " + instructionArgs.Length + '.');
            }

            var beqParser = new BeqProcessor(m_SymTable);
            return beqParser.GenerateCodeForInstruction(address, new string[] { "x0", instructionArgs[0], instructionArgs[1] });
        }

        private readonly SymbolTable m_SymTable;
    }
}