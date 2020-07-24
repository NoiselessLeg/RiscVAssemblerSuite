using System;
using System.Collections.Generic;

namespace Assembler.OutputProcessing.ParamDecoding
{
   internal static class ParamDecoderTable
   {
      static ParamDecoderTable()
      {
         s_DecoderTable = new Dictionary<InstructionType, IParameterDecoder>()
         {
               { InstructionType.Lui, new UpperImmediateDecoder() },
               { InstructionType.Auipc, new UpperImmediateDecoder() },
               { InstructionType.Jal, new JalDecoder() },
               { InstructionType.Jalr, new ImmediateInstructionDecoder() },
               { InstructionType.Beq, new BranchDecoder() },
               { InstructionType.Bne, new BranchDecoder() },
               { InstructionType.Blt, new BranchDecoder() },
               { InstructionType.Bge, new BranchDecoder() },
               { InstructionType.Bltu, new BranchDecoder() },
               { InstructionType.Bgeu, new BranchDecoder() },
               { InstructionType.Lb, new ImmediateInstructionDecoder() },
               { InstructionType.Lh, new ImmediateInstructionDecoder() },
               { InstructionType.Lw, new ImmediateInstructionDecoder() },
               { InstructionType.Lbu, new ImmediateInstructionDecoder() },
               { InstructionType.Lhu, new ImmediateInstructionDecoder() },
               { InstructionType.Sb, new StoreDecoder() },
               { InstructionType.Sh, new StoreDecoder() },
               { InstructionType.Sw, new StoreDecoder() },
               { InstructionType.Addi, new ImmediateInstructionDecoder() },
               { InstructionType.Slti, new ImmediateInstructionDecoder() },
               { InstructionType.Sltiu, new ImmediateInstructionDecoder() },
               { InstructionType.Xori, new ImmediateInstructionDecoder() },
               { InstructionType.Ori, new ImmediateInstructionDecoder() },
               { InstructionType.Andi, new ImmediateInstructionDecoder() },
               { InstructionType.Slli, new ShiftImmediateDecoder() },
               { InstructionType.Srli, new ShiftImmediateDecoder() },
               { InstructionType.Srai, new ShiftImmediateDecoder() },
               { InstructionType.Add, new RegisterInstructionDecoder() },
               { InstructionType.Sub, new RegisterInstructionDecoder() },
               { InstructionType.Sll, new RegisterInstructionDecoder() },
               { InstructionType.Slt, new RegisterInstructionDecoder() },
               { InstructionType.Sltu, new RegisterInstructionDecoder() },
               { InstructionType.Xor, new RegisterInstructionDecoder() },
               { InstructionType.Srl, new RegisterInstructionDecoder() },
               { InstructionType.Sra, new RegisterInstructionDecoder() },
               { InstructionType.Or, new RegisterInstructionDecoder() },
               { InstructionType.And, new RegisterInstructionDecoder() },

               { InstructionType.Mul, new RegisterInstructionDecoder() },
               { InstructionType.Mulh, new RegisterInstructionDecoder() },
               { InstructionType.Mulhsu, new RegisterInstructionDecoder() },
               { InstructionType.Mulhu, new RegisterInstructionDecoder() },
               { InstructionType.Div, new RegisterInstructionDecoder() },
               { InstructionType.Divu, new RegisterInstructionDecoder() },
               { InstructionType.Rem, new RegisterInstructionDecoder() },
               { InstructionType.Remu, new RegisterInstructionDecoder() },

               { InstructionType.Ecall, new EcallDecoder() },
               { InstructionType.Ebreak, new EbreakDecoder() },

               { InstructionType.FaddS, new RegisterInstructionDecoder() },
               { InstructionType.FsubS, new RegisterInstructionDecoder() },
               { InstructionType.FmulS, new RegisterInstructionDecoder() },
               { InstructionType.FdivS, new RegisterInstructionDecoder() },
               { InstructionType.FeqS, new RegisterInstructionDecoder() },
               { InstructionType.FltS, new RegisterInstructionDecoder() },
               { InstructionType.FleS, new RegisterInstructionDecoder() },

               { InstructionType.FsqrtS, new TwoRegisterFloatDecoder() },
               { InstructionType.FcvtSW, new TwoRegisterFloatDecoder() },
               { InstructionType.FcvtWS, new TwoRegisterFloatDecoder() },
         };
      }

      public static IParameterDecoder GetDecoderFor(InstructionType instructionType)
      {
         if (!s_DecoderTable.TryGetValue(instructionType, out IParameterDecoder decoder))
         {
            throw new ArgumentException("No decoder available for instruction type " + instructionType);
         }

         return decoder;
      }

      private static readonly Dictionary<InstructionType, IParameterDecoder> s_DecoderTable;
   }
}
