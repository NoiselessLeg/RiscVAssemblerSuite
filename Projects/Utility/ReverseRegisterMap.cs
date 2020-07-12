using System;
using System.Collections.Generic;

namespace Assembler.Common
{
   /// <summary>
   /// Defines the mapping between a numeric register value and its canonical name.
   /// </summary>
   public static class ReverseRegisterMap
   {
      static ReverseRegisterMap()
      {
         s_RegisterMap = new Dictionary<int, string>()
         {
               { 0, "x0" },
               { 1, "ra" },
               { 2, "sp" },
               { 3, "gp" },
               { 4, "tp" },
               { 5, "t0" },
               { 6, "t1" },
               { 7, "t2" },
               { 8, "fp" },
               { 9, "s1" },
               { 10, "a0" },
               { 11, "a1" },
               { 12, "a2" },
               { 13, "a3" },
               { 14, "a4" },
               { 15, "a5" },
               { 16, "a6" },
               { 17, "a7" },
               { 18, "s2" },
               { 19, "s3" },
               { 20, "s4" },
               { 21, "s5" },
               { 22, "s6" },
               { 23, "s7" },
               { 24, "s8" },
               { 25, "s9" },
               { 26, "s10" },
               { 27, "s11" },
               { 28, "t3" },
               { 29, "t4" },
               { 30, "t5" },
               { 31, "t6" },
               { 32, "pc" }
         };

         s_FpRegisterMap = new Dictionary<int, string>()
         {
               { 0, "f0" },
               { 1, "f1" },
               { 2, "f2" },
               { 3, "f3" },
               { 4, "f4" },
               { 5, "f5" },
               { 6, "f6" },
               { 7, "f7" },
               { 8, "f8" },
               { 9, "f9" },
               { 10, "f10" },
               { 11, "f11" },
               { 12, "f12" },
               { 13, "f13" },
               { 14, "f14" },
               { 15, "f15" },
               { 16, "f16" },
               { 17, "f17" },
               { 18, "f18" },
               { 19, "f19" },
               { 20, "f20" },
               { 21, "f21" },
               { 22, "f22" },
               { 23, "f23" },
               { 24, "f24" },
               { 25, "f25" },
               { 26, "f26" },
               { 27, "f27" },
               { 28, "f28" },
               { 29, "f29" },
               { 30, "f30" },
               { 31, "f31" }
         };
      }

      /// <summary>
      /// Returns the canonical name of a register used by the CPU.
      /// Throws an ArgumentException if the register is out of range.
      /// </summary>
      /// <param name="register">The numeric value of the register to look up.</param>
      /// <returns>The canonical name of the register to be used by the CPU.</returns>
      public static string GetStringifiedRegisterValue(int register)
      {
         string regName = string.Empty;
         if (!s_RegisterMap.TryGetValue(register, out regName))
         {
            throw new ArgumentException(register + " is not a valid RISC-V register.");
         }

         return regName;
      }

      /// <summary>
      /// Returns the canonical name of a register used by the CPU.
      /// Throws an ArgumentException if the register is out of range.
      /// </summary>
      /// <param name="register">The numeric value of the register to look up.</param>
      /// <returns>The canonical name of the register to be used by the CPU.</returns>
      public static string GetStringifiedFloatingPtRegisterValue(int register)
      {
         string regName = string.Empty;
         if (!s_FpRegisterMap.TryGetValue(register, out regName))
         {
            throw new ArgumentException(register + " is not a valid RISC-V floating point register.");
         }

         return regName;
      }

      private static readonly Dictionary<int, string> s_RegisterMap;
      private static readonly Dictionary<int, string> s_FpRegisterMap;
   }
}
