using Assembler.OutputProcessing.TextOutput.InstructionGenerators;
using System;
using System.Collections.Generic;

namespace Assembler.OutputProcessing.TextOutput
{
   /// <summary>
   /// Defines the mapping between an instruction type and the stringifier implementation of it.
   /// </summary>
   internal static class InstructionTextMap
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
                { InstructionType.Ebreak, new EbreakStringifier("ebreak") },

                { InstructionType.Mul, new RInstructionStringifier("mul") },
                { InstructionType.Mulh, new RInstructionStringifier("mulh") },
                { InstructionType.Mulhsu, new RInstructionStringifier("mulhsu") },
                { InstructionType.Mulhu, new RInstructionStringifier("mulhu") },
                { InstructionType.Div, new RInstructionStringifier("div") },
                { InstructionType.Divu, new RInstructionStringifier("divu") },
                { InstructionType.Rem, new RInstructionStringifier("rem") },
                { InstructionType.Remu, new RInstructionStringifier("remu") },
                
                { InstructionType.FaddS, new FltPtRInstructionStringifier("fadd.s") },
                { InstructionType.FsubS, new FltPtRInstructionStringifier("fsub.s") },
                { InstructionType.FmulS, new FltPtRInstructionStringifier("fmul.s") },
                { InstructionType.FdivS, new FltPtRInstructionStringifier("fdiv.s") },
                { InstructionType.FminS, new FltPtRInstructionStringifier("fmin.s") },
                { InstructionType.FmaxS, new FltPtRInstructionStringifier("fmax.s") },
                { InstructionType.FsqrtS, new FsqrtsStringifier("fsqrt.s") },
                { InstructionType.FeqS, new FltPtRInstructionStringifier("feq.s") },
                { InstructionType.FltS, new FltPtRInstructionStringifier("flt.s") },
                { InstructionType.FleS, new FltPtRInstructionStringifier("fle.s") },
                { InstructionType.FcvtSW, new FcvtswStringifier() },
                { InstructionType.FcvtWS, new FcvtwsStringifier() },
            };
      }

      /// <summary>
      /// Gets a stringifier implementation mapped to an instruction type. Throws
      /// an ArgumentException if the instruction type is not recognized.
      /// </summary>
      /// <param name="instructionName">The instruction type to find a stringifier for.</param>
      /// <returns>The stringifier implementation to use.</returns>
      public static IParameterStringifier GetParameterStringifier(InstructionType instructionName)
      {
         if (!s_InstructionMap.TryGetValue(instructionName, out IParameterStringifier stringifier))
         {
            throw new ArgumentException(instructionName + " is not a valid RISC-V instruction.");
         }

         return stringifier;
      }

      private static readonly Dictionary<InstructionType, IParameterStringifier> s_InstructionMap;
   }
}
