namespace Assembler.OutputProcessing
{
    /// <summary>
    /// An enumeration describing the various instruction types.
    /// </summary>
    public enum InstructionType
    {
        Lui,
        Auipc,
        Jal,
        Jalr,
        Beq,
        Bne,
        Blt,
        Bge,
        Bltu,
        Bgeu,
        Lb,
        Lh,
        Lw,
        Lbu,
        Lhu,
        Sb,
        Sh,
        Sw,
        Addi,
        Slti,
        Sltiu,
        Xori,
        Ori,
        Andi,
        Slli,
        Srli,
        Srai,
        Add,
        Sub,
        Sll,
        Slt,
        Sltu,
        Xor,
        Srl,
        Sra,
        Or,
        And,
        Ecall,
        Ebreak
    }
}
