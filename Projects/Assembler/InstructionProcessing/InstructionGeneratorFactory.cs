using Assembler.CodeGeneration;
using Assembler.SymbolTableConstruction;
using System;
using System.Collections.Generic;

namespace Assembler.InstructionProcessing
{
    class InstructionGeneratorFactory
    {
        /// <summary>
        /// Creates an instance of the InstructionParserFactory, and the BaseInstructionProcessor
        /// implementations.
        /// </summary>
        /// <param name="symbolTable">The filled-out symbol table that parsers can utilize for symbol lookup.</param>
        public InstructionGeneratorFactory(SymbolTable symbolTable)
        {
            //TODO: Add instructions here as they are generated!
            m_InstructionList = new Dictionary<string, BaseInstructionProcessor>()
            {
                //RV32I
                { "lui", new LuiInstructionParser() }, 
                { "auipc", new AuipcInstructionParser() },
                { "jal", new PlaceholderParser("jal") },
                { "jalr", new PlaceholderParser("jalr") },
                { "beq", new BeqInstructionParser(symbolTable) },
                { "beqz", new BeqzInstructionParser(symbolTable) },
                { "bne", new PlaceholderParser("bne") },
                { "blt", new PlaceholderParser("blt") },
                { "bge", new PlaceholderParser("bge") },
                { "bltu", new PlaceholderParser("bltu") },
                { "bgeu", new PlaceholderParser("bgeu") },
                { "lb", new PlaceholderParser("lb") },
                { "lh", new PlaceholderParser("lh") },
                { "lw", new PlaceholderParser("lw") },
                { "lbu", new PlaceholderParser("lbu") },
                { "lhu", new PlaceholderParser("lhu") },
                { "sb", new PlaceholderParser("sb") },
                { "sh", new PlaceholderParser("sh") },
                { "sw", new PlaceholderParser("sw") },
                { "addi", new AddImmediateInstructionParser() },
                { "slti", new PlaceholderParser("slti") },
                { "sltiu", new PlaceholderParser("sltiu") },
                { "xori", new PlaceholderParser("xori") },
                { "ori", new OrImmediateInstructionParser() },
                { "andi", new AndImmediateInstructionParser() },
                { "slli", new PlaceholderParser("slli") },
                { "srli", new PlaceholderParser("srli") },
                { "srai", new PlaceholderParser("srai") },
                { "add", new AddInstructionParser() },
                { "sub", new PlaceholderParser("sub") },
                { "sll", new PlaceholderParser("sll") },
                { "slt", new PlaceholderParser("slt") },
                { "sltu", new PlaceholderParser("sltu") },
                { "xor", new PlaceholderParser("xor") },
                { "srl", new PlaceholderParser("srl") },
                { "sra", new PlaceholderParser("sra") },
                { "or", new OrInstructionParser() },
                { "and", new AndInstructionParser() },
                { "nop", new NopInstructionParser() },

                //RV32M Integer multiply / divide
                { "mul", new PlaceholderParser("mul") },
                { "mulh", new PlaceholderParser("mulh") },
                { "mulhsu", new PlaceholderParser("mulhsu") },
                { "mulhu", new PlaceholderParser("mulhu") },
                { "div", new PlaceholderParser("div") },
                { "divu", new PlaceholderParser("divu") },
                { "rem", new PlaceholderParser("rem") },
                { "remu", new PlaceholderParser("remu") },

            };
        }

        /// <summary>
        /// Retrieves a code generator for a given instruction. The string should be
        /// checked to determine if it is a supported instruction by calling IsInstruction
        /// before passing it to this function; if the instruction cannot be found, an
        /// ArgumentException will be thrown.
        /// </summary>
        /// <param name="instruction">The instruction to retrieve a parser for.</param>
        /// <returns>The parser implementation for this instruction.</returns>
        public IInstructionGenerator GetParserForInstruction(string instruction)
        {
            BaseInstructionProcessor parserImpl = null;

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

        private readonly Dictionary<string, BaseInstructionProcessor> m_InstructionList;
    }
}
