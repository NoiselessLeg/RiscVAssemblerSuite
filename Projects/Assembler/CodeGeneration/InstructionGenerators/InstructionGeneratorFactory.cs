using System;
using System.Collections.Generic;

namespace Assembler.CodeGeneration.InstructionGenerators
{
    class InstructionGeneratorFactory
    {
        /// <summary>
        /// Creates an instance of the InstructionParserFactory, and the IParser
        /// implementations.
        /// </summary>
        /// <param name="symbolTable">The filled-out symbol table that parsers can utilize for symbol lookup.</param>
        public InstructionGeneratorFactory(SymbolTable symbolTable)
        {
            //TODO: Add instructions here as they are generated!
            m_InstructionList = new Dictionary<string, IParser>()
            {
                //RV32I
                { "lui", new LuiInstructionParser() }, 
                { "auipc", new AuipcInstructionParser() },
                { "jal", new NopInstructionParser() },
                { "jalr", new NopInstructionParser() },
                { "beq", new BeqInstructionParser(symbolTable) },
                { "beqz", new BeqzInstructionParser(symbolTable) },
                { "bne", new NopInstructionParser() },
                { "blt", new NopInstructionParser() },
                { "bge", new NopInstructionParser() },
                { "bltu", new NopInstructionParser() },
                { "bgeu", new NopInstructionParser() },
                { "lb", new NopInstructionParser() },
                { "lh", new NopInstructionParser() },
                { "lw", new NopInstructionParser() },
                { "lbu", new NopInstructionParser() },
                { "lhu", new NopInstructionParser() },
                { "sb", new NopInstructionParser() },
                { "sh", new NopInstructionParser() },
                { "sw", new NopInstructionParser() },
                { "addi", new AddImmediateInstructionParser() },
                { "slti", new NopInstructionParser() },
                { "sltiu", new NopInstructionParser() },
                { "xori", new NopInstructionParser() },
                { "ori", new NopInstructionParser() },
                { "andi", new AndImmediateInstructionParser() },
                { "slli", new NopInstructionParser() },
                { "srli", new NopInstructionParser() },
                { "srai", new NopInstructionParser() },
                { "add", new AddInstructionParser() },
                { "sub", new NopInstructionParser() },
                { "sll", new NopInstructionParser() },
                { "slt", new NopInstructionParser() },
                { "sltu", new NopInstructionParser() },
                { "xor", new NopInstructionParser() },
                { "srl", new NopInstructionParser() },
                { "sra", new NopInstructionParser() },
                { "or", new NopInstructionParser() },
                { "and", new AndInstructionParser() },
                { "nop", new NopInstructionParser() },

                //RV32M Integer multiply / divide
                { "mul", new NopInstructionParser() },
                { "mulh", new NopInstructionParser() },
                { "mulhsu", new NopInstructionParser() },
                { "mulhu", new NopInstructionParser() },
                { "div", new NopInstructionParser() },
                { "divu", new NopInstructionParser() },
                { "rem", new NopInstructionParser() },
                { "remu", new NopInstructionParser() },

            };
        }

        /// <summary>
        /// Retrieves a parser for a given instruction. The string should be
        /// checked to determine if it is a supported instruction by calling IsInstruction
        /// before passing it to this function; if the instruction cannot be found, an
        /// ArgumentException will be thrown.
        /// </summary>
        /// <param name="instruction">The instruction to retrieve a parser for.</param>
        /// <returns>The parser implementation for this instruction.</returns>
        public IParser GetParserForInstruction(string instruction)
        {
            IParser parserImpl = null;

            // precondition should that IsInstruction is called prior to retriving a parser.
            // if we can't find a parser, throw an exception.
            if (!m_InstructionList.TryGetValue(instruction, out parserImpl))
            {
                throw new ArgumentException("Cannot find parser for unknown instruction " + instruction);
            }

            return parserImpl;
        }

        /// <summary>
        /// Determines if a token is in the list of supported instructions.
        /// </summary>
        /// <param name="token">The token to examine.</param>
        /// <returns>True if the token is a known/supported instruction; otherwise returns false.</returns>
        public bool IsInstruction(string token)
        {
            return m_InstructionList.ContainsKey(token);
        }

        private readonly Dictionary<string, IParser> m_InstructionList;
    }
}
