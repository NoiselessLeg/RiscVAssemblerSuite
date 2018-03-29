using Assembler.CodeGeneration;
using Assembler.SymbolTableConstruction;
using System;
using System.Collections.Generic;

namespace Assembler.InstructionProcessing
{
    /// <summary>
    /// Factory that allows both the symbol table generator and code generator
    /// to retrieve implementations of size estimators and code generators for
    /// a given instruction.
    /// </summary>
    class InstructionProcessorFactory
    {
        /// <summary>
        /// Creates an instance of the ProcessorFactory, and the BaseInstructionProcessor
        /// implementations.
        /// </summary>
        /// <param name="symbolTable">The filled-out symbol table that Processors can utilize for symbol lookup.</param>
        public InstructionProcessorFactory(SymbolTable symbolTable)
        {
            //TODO: Add instructions here as they are generated!
            m_InstructionList = new Dictionary<string, BaseInstructionProcessor>()
            {
                //RV32I
                { "lui", new LuiProcessor() },
                { "auipc", new AuipcProcessor() },
                { "jal", new PlaceholderProcessor("jal") },
                { "jalr", new PlaceholderProcessor("jalr") },
                { "beq", new BeqProcessor(symbolTable) },
                { "beqz", new BeqzProcessor(symbolTable) },
                { "bne", new PlaceholderProcessor("bne") },
                { "blt", new PlaceholderProcessor("blt") },
                { "bge", new PlaceholderProcessor("bge") },
                { "bltu", new PlaceholderProcessor("bltu") },
                { "bgeu", new PlaceholderProcessor("bgeu") },
                { "lb", new PlaceholderProcessor("lb") },
                { "lh", new PlaceholderProcessor("lh") },
                { "lw", new PlaceholderProcessor("lw") },
                { "lbu", new PlaceholderProcessor("lbu") },
                { "lhu", new PlaceholderProcessor("lhu") },
                { "sb", new PlaceholderProcessor("sb") },
                { "sh", new PlaceholderProcessor("sh") },
                { "sw", new PlaceholderProcessor("sw") },
                { "addi", new AddiProcessor() },
                { "slti", new PlaceholderProcessor("slti") },
                { "sltiu", new PlaceholderProcessor("sltiu") },
                { "xori", new PlaceholderProcessor("xori") },
                { "ori", new OriProcessor() },
                { "andi", new AndiProcessor() },
                { "slli", new PlaceholderProcessor("slli") },
                { "srli", new PlaceholderProcessor("srli") },
                { "srai", new PlaceholderProcessor("srai") },
                { "add", new AddProcessor() },
                { "sub", new PlaceholderProcessor("sub") },
                { "sll", new PlaceholderProcessor("sll") },
                { "slt", new PlaceholderProcessor("slt") },
                { "sltu", new PlaceholderProcessor("sltu") },
                { "xor", new PlaceholderProcessor("xor") },
                { "srl", new PlaceholderProcessor("srl") },
                { "sra", new PlaceholderProcessor("sra") },
                { "or", new OrProcessor() },
                { "and", new AndProcessor() },
                { "nop", new NopProcessor() },
                { "li", new LiProcessor() },
                { "mv", new PlaceholderProcessor("mv") },

                //RV32M Integer multiply / divide
                { "mul", new PlaceholderProcessor("mul") },
                { "mulh", new PlaceholderProcessor("mulh") },
                { "mulhsu", new PlaceholderProcessor("mulhsu") },
                { "mulhu", new PlaceholderProcessor("mulhu") },
                { "div", new PlaceholderProcessor("div") },
                { "divu", new PlaceholderProcessor("divu") },
                { "rem", new PlaceholderProcessor("rem") },
                { "remu", new PlaceholderProcessor("remu") },

            };
        }

        /// <summary>
        /// Retrieves a code generator for a given instruction. The string should be
        /// checked to determine if it is a supported instruction by calling IsInstruction
        /// before passing it to this function; if the instruction cannot be found, an
        /// ArgumentException will be thrown.
        /// </summary>
        /// <param name="instruction">The instruction to retrieve an instruction generator for.</param>
        /// <returns>The processor implementation for this instruction.</returns>
        public IInstructionGenerator GetProcessorForInstruction(string instruction)
        {
            return RetrieveProcessorForInstruction(instruction);
        }

        /// <summary>
        /// Retrieves a size estimator for a given instruction. The string should be
        /// checked to determine if it is a supported instruction by calling IsInstruction
        /// before passing it to this function; if the instruction cannot be found, an
        /// ArgumentException will be thrown.
        /// </summary>
        /// <param name="instruction">The instruction to retrieve a size estimator for.</param>
        /// <returns>The size estimation implementation for this instruction.</returns>
        public IInstructionSizeEstimator GetEstimatorForInstruction(string instruction)
        {
            return RetrieveProcessorForInstruction(instruction);
        }

        /// <summary>
        /// Base implementation for retrieving an instruction processor. This suits both the needs
        /// of the symbol table generator and the code generator, but each public function returns an instance of
        /// the interface that the parser in questions requires. This is common between the two, as the base class
        /// implements both of these interfaces.
        /// </summary>
        /// <param name="instruction">The instruction to retrieve a size estimator for.</param>
        /// <returns>The instruction processor for the given instruction.</returns>
        private BaseInstructionProcessor RetrieveProcessorForInstruction(string instruction)
        {
            BaseInstructionProcessor processorImpl = null;

            // precondition should that IsInstruction is called prior to retriving a Processor.
            // if we can't find a Processor, throw an exception.
            if (!m_InstructionList.TryGetValue(instruction, out processorImpl))
            {
                throw new ArgumentException("Cannot find processor for unknown instruction " + instruction);
            }

            return processorImpl;
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
