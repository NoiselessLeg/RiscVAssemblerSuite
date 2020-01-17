using Assembler.Common;
using Assembler.OutputProcessing;

namespace Assembler.Disassembler.InstructionGenerators
{
    /// <summary>
    /// Defines a class that can print an instruction as text.
    /// </summary>
    interface IParameterStringifier
    {
        /// <summary>
        /// Formats and stringifies an instruction as well as its parameters.
        /// </summary>
        /// <param name="currPgrmCtr">The value that the program counter would theoretically be at
        /// upon encountering this instruction.</param>
        /// <param name="inst">The disassembled instruction to stringify.</param>
        /// <param name="symTable">A reverse symbol table used to map addresses back to label names.</param>
        /// <returns>A string representing the instruction and its parameters that can be written to a text file.</returns>
        string GetFormattedInstruction(int currPgrmCtr, DisassembledInstruction inst, ReverseSymbolTable symTable);
    }
}
