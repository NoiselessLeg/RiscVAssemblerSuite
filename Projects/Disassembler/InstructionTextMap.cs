using Assembler.Common;
using Assembler.Disassembler.InstructionGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Disassembler
{
    /// <summary>
    /// Defines the mapping between a register name and the numeric CPU value.
    /// </summary>
    static class InstructionTextMap
    {
        static InstructionTextMap()
        {
            s_InstructionMap = new Dictionary<InstructionType, IParameterStringifier>()
            {
                { InstructionType.Lui, new UInstructionStringifier("lui") },
                { InstructionType.Auipc, new UInstructionStringifier("auipc") },
                { InstructionType.Jal, new JalInstructionStringifier("jal") },
                { InstructionType.Jalr, new JalrInstructionStringifier("jalr") },
                { InstructionType.Beq, new BranchInstructionStringifier("beq") },
                { InstructionType.Bne, new BranchInstructionStringifier("bne") },
                { InstructionType.Blt, new BranchInstructionStringifier("blt") },
                { InstructionType.Bge, new BranchInstructionStringifier("bge") },
                { InstructionType.Bltu, new BranchInstructionStringifier("bltu") },
                { InstructionType.Bgeu, new BranchInstructionStringifier("bgeu") },
                { InstructionType.Lb, new LdInstructionStringifier("lb") },
                { InstructionType.Lh, new LdInstructionStringifier("lh") },
                { InstructionType.Lw, new LdInstructionStringifier("lw") },
                { InstructionType.Lbu, new LdInstructionStringifier("lbu") },
                { InstructionType.Lhu, new LdInstructionStringifier("lhu") },
                { InstructionType.Sb, new StoreInstructionStringifier("sb") },
                { InstructionType.Sh, new StoreInstructionStringifier("sh") },
                { InstructionType.Sw, new StoreInstructionStringifier("sw") },
                { InstructionType.Addi, new ImmediateParamStringifier("addi") },
                { InstructionType.Slti, new ImmediateParamStringifier("slti") },
                { InstructionType.Sltiu, new ImmediateParamStringifier("sltiu") },
                { InstructionType.Xori, new ImmediateParamStringifier("xori") },
                { InstructionType.Ori, new ImmediateParamStringifier("ori") },
                { InstructionType.Andi, new ImmediateParamStringifier("andi") },
                { InstructionType.Slli, new ImmediateParamStringifier("slli") },
                { InstructionType.Srli, new ImmediateParamStringifier("srli") },
                { InstructionType.Srai, new ImmediateParamStringifier("srai") },
                { InstructionType.Add, new RInstructionStringifier("add") },
                { InstructionType.Sub, new RInstructionStringifier("sub") },
                { InstructionType.Sll, new RInstructionStringifier("sll") },
                { InstructionType.Slt, new RInstructionStringifier("slt") },
                { InstructionType.Sltu, new RInstructionStringifier("sltu") },
                { InstructionType.Xor, new RInstructionStringifier("xor") },
                { InstructionType.Srl, new RInstructionStringifier("srl") },
                { InstructionType.Sra, new RInstructionStringifier("sra") },
                { InstructionType.Or, new RInstructionStringifier("or") },
                { InstructionType.And, new RInstructionStringifier("and") },
                { InstructionType.Ecall, new EcallStringifier("ecall") },
            };
        }

        /// <summary>
        /// Returns the canonical name of a register used by the CPU.
        /// Throws an ArgumentException if the register is not valid.
        /// </summary>
        /// <param name="register">The numeric value of the register to look up.</param>
        /// <returns>The canonical name of the register to be used by the CPU.</returns>
        public static IParameterStringifier GetParameterStringifier(InstructionType instructionName)
        {
            IParameterStringifier stringifier = default(IParameterStringifier);
            if (!s_InstructionMap.TryGetValue(instructionName, out stringifier))
            {
                throw new ArgumentException(instructionName + " is not a valid RISC-V instruction.");
            }

            return stringifier;
        }

        private static readonly Dictionary<InstructionType, IParameterStringifier> s_InstructionMap;
    }
}
