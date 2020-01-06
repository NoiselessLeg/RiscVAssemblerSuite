using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.Utility
{
   public static class AssemblerKeywords
   {
      static AssemblerKeywords()
      {
         //TODO: Add instructions here as they are generated!
         m_RiscVKeywords = new List<string>()
            {
                //RV32I
                "lui",
                "auipc",
                "jal",
                "jalr",
                "beq",
                "beqz",
                "bne",
                "bnez",
                "blt",
                "bltz",
                "bge",
                "bgez",
                "bltu",
                "bgeu",
                "lb",
                "lh", 
                "lw", 
                "lbu", 
                "lhu", 
                "sb",
                "sh", 
                "sw",
                "addi", 
                "slti", 
                "sltiu", 
                "xori", 
                "ori",
                "andi",
                "slli",
                "srli",
                "srai",
                "add",
                "sub", 
                "sll", 
                "slt",
                "sltu",
                "xor", 
                "srl",
                "sra",
                "or",
                "and",
                "not",

                // these are pseudo instructions
                "nop",
                "li",
                "la",
                "mv", 
                "j",

                //RV32M Integer multiply / divide
                "mul", 
                "mulh",
                "mulhsu",
                "mulhu",
                "div",
                "divu",
                "rem", 
                "remu", 

                // for system calls
                "ecall"
            };
      }

      public static bool IsKeyword(string keyword)
      {
         return m_RiscVKeywords.IndexOf(keyword) >= 0;
      }

      private static List<string> m_RiscVKeywords;
   }
}
