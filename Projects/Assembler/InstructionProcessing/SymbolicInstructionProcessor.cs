using Assembler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.InstructionProcessing
{
    /// <summary>
    /// Defines a class that accepts a symbol as an argument.
    /// </summary>
    abstract class SymbolicInstructionProcessor : BaseInstructionProcessor
    {
        /// <summary>
        /// Creates an instance of the SymbolicInstructionProcessor.
        /// </summary>
        /// <param name="symTbl">The SymbolTable instance used to look up symbols with.</param>
        public SymbolicInstructionProcessor(SymbolTable symTbl)
        {
            m_SymbolTable = symTbl;
        }

        /// <summary>
        /// Delegates to an abstract method to determine how many instructions are returned.
        /// This is because at the time this method is called, instructions that take a symbol as an argument
        /// cannot be guaranteed to have loaded that symbol into the symbol table by the time
        /// the calculation is being performed. Therefore, it may lead to subtle difficult-to-detect bugs
        /// if the default implementation is called (where the instructions are essentially generated assuming
        /// symbols are loaded). We force implementors to specialize a method so that we avoid this.
        /// </summary>
        /// <param name="address">The address in the .text segment of the instruction being parsed.</param>
        /// <param name="instructionArgs">The parameters of the instruction</param>
        /// <returns>An integer representing how many instructions will be generated for a line of assembly.</returns>
        public override int GetNumGeneratedInstructions(int address, string[] instructionArgs)
        {
            return GetNumOfInstructionsForSymbolicInstruction(address, instructionArgs);
        }

        /// <summary>
        /// Gets the SymbolTable instance used to lookup symbols with.
        /// </summary>
        protected SymbolTable SymbolTable
        {
            get { return m_SymbolTable; }
        }

        /// <summary>
        /// Explicitly forces implementors to define a specific implementation per-instruction that calculates
        /// the number of instructions generated for an instruction that accepts a symbol as a parameter.
        /// Implementors should take care to NOT necessarily rely on the SymbolTable as part of this calculation,
        /// as it is not guaranteed that all symbols will have been loaded prior to this calculation being performed.
        /// </summary>
        /// <param name="address">The address in the .text segment of the instruction being parsed.</param>
        /// <param name="args">The parameters of the instruction</param>
        /// <returns>An integer representing how many instructions will be generated for a line of assembly.</returns>
        protected abstract int GetNumOfInstructionsForSymbolicInstruction(int address, string[] args);

        private readonly SymbolTable m_SymbolTable;
    }
}
