using Assembler.Common;

namespace Assembler.Disassembler.InstructionGenerators
{
    /// <summary>
    /// Defines a class that can print an instruction as text.
    /// </summary>
    interface IParameterStringifier
    {
        string GetFormattedInstruction(int currPgrmCtr, DisassembledInstruction inst, ReverseSymbolTable symTable);
    }
}
