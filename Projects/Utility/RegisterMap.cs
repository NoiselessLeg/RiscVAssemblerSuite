﻿using System;
using System.Collections.Generic;

namespace Assembler.Common
{
   /// <summary>
   /// Defines the mapping between a register name and the numeric CPU value.
   /// </summary>
   public static class RegisterMap
   {
      static RegisterMap()
      {
         s_RegisterMap = new Dictionary<string, int>()
         {
               { "x0", 0 },
               { "zero", 0 },
               { "x1", 1 },
               { "ra", 1 },
               { "x2", 2 },
               { "sp", 2 },
               { "x3", 3 },
               { "gp", 3 },
               { "x4", 4 },
               { "tp", 4 },
               { "x5", 5 },
               { "t0", 5 },
               { "x6", 6 },
               { "t1", 6 },
               { "x7", 7 },
               { "t2", 7 },
               { "x8", 8 },
               { "s0", 8 },
               { "fp", 8 },
               { "x9", 9 },
               { "s1", 9 },
               { "x10", 10 },
               { "a0", 10 },
               { "x11", 11 },
               { "a1", 11 },
               { "x12", 12 },
               { "a2", 12 },
               { "x13", 13 },
               { "a3", 13 },
               { "x14", 14 },
               { "a4", 14 },
               { "x15", 15 },
               { "a5", 15 },
               { "x16", 16 },
               { "a6", 16 },
               { "x17", 17 },
               { "a7", 17 },
               { "x18", 18 },
               { "s2", 18 },
               { "x19", 19 },
               { "s3", 19 },
               { "x20", 20 },
               { "s4", 20 },
               { "x21", 21 },
               { "s5", 21 },
               { "x22", 22 },
               { "s6", 22 },
               { "x23", 23 },
               { "s7", 23 },
               { "x24", 24 },
               { "s8", 24 },
               { "x25", 25 },
               { "s9", 25 },
               { "x26", 26 },
               { "s10", 26 },
               { "x27", 27 },
               { "s11", 27 },
               { "x28", 28 },
               { "t3", 28 },
               { "x29", 29 },
               { "t4", 29 },
               { "x30", 30 },
               { "t5", 30 },
               { "x31", 31 },
               { "t6", 31 },
               { "pc", 32 }
         };

         s_FpRegisterMap = new Dictionary<string, int>()
         {
               { "f0", 0 },
               { "ft0", 0 },
               { "f1", 1 },
               { "ft1", 1 },
               { "f2", 2 },
               { "ft2", 2 },
               { "f3", 3 },
               { "ft3", 3 },
               { "f4", 4 },
               { "ft4", 4 },
               { "f5", 5 },
               { "ft5", 5 },
               { "f6", 6 },
               { "ft6", 6 },
               { "f7", 7 },
               { "ft7", 7 },
               { "f8", 8 },
               { "fs0", 8 },
               { "f9", 9 },
               { "fs1", 9 },
               { "f10", 10 },
               { "fa0", 10 },
               { "f11", 11 },
               { "fa1", 11 },
               { "f12", 12 },
               { "fa2", 12 },
               { "f13", 13 },
               { "fa3", 13 },
               { "f14", 14 },
               { "fa4", 14 },
               { "f15", 15 },
               { "fa5", 15 },
               { "f16", 16 },
               { "fa6", 16 },
               { "f17", 17 },
               { "fa7", 17 },
               { "f18", 18 },
               { "fs2", 18 },
               { "f19", 19 },
               { "fs3", 19 },
               { "f20", 20 },
               { "fs4", 20 },
               { "f21", 21 },
               { "fs5", 21 },
               { "f22", 22 },
               { "fs6", 22 },
               { "f23", 23 },
               { "fs7", 23 },
               { "f24", 24 },
               { "fs8", 24 },
               { "f25", 25 },
               { "fs9", 25 },
               { "f26", 26 },
               { "fs10", 26 },
               { "f27", 27 },
               { "fs11", 27 },
               { "f28", 28 },
               { "ft8", 28 },
               { "f29", 29 },
               { "ft9", 29 },
               { "f30", 30 },
               { "ft10", 30 },
               { "f31", 31 },
               { "ft11", 31 },
         };
      }

      /// <summary>
      /// Returns the numeric value of a register name used by the CPU.
      /// Throws an ArgumentException if the register is not valid.
      /// </summary>
      /// <param name="register">The name of the register to look up.</param>
      /// <returns>The numeric value of the register to be used by the CPU.</returns>
      public static int GetNumericRegisterValue(string register)
      {
         if (!s_RegisterMap.TryGetValue(register, out int numericReg))
         {
            throw new ArgumentException(register + " is not a valid RISC-V register.");
         }

         return numericReg;
      }

      /// <summary>
      /// Determines whether or not the passed string is a named register value (e.g. t0, t1, etc.)
      /// </summary>
      /// <param name="value">The string value to interpret</param>
      /// <returns>True if the register is a "named" register, otherwise returns false.</returns>
      public static bool IsNamedIntegerRegister(string value)
      {
         return s_RegisterMap.ContainsKey(value);
      }

      /// <summary>
      /// Returns the numeric value of a floating point register name used by the CPU.
      /// Throws an ArgumentException if the register is not valid.
      /// </summary>
      /// <param name="register">The name of the register to look up.</param>
      /// <returns>The numeric value of the register to be used by the CPU.</returns>
      public static int GetNumericFloatingPointRegisterValue(string register)
      {
         if (!s_FpRegisterMap.TryGetValue(register, out int numericReg))
         {
            throw new ArgumentException(register + " is not a valid RISC-V floating-point register.");
         }

         return numericReg;
      }

      /// <summary>
      /// Determines whether or not the passed string is a named floating point
      /// register value (e.g. f0, f1, etc.)
      /// </summary>
      /// <param name="value">The string value to interpret</param>
      /// <returns>True if the register is a "named" register, otherwise returns false.</returns>
      public static bool IsNamedFloatingPointRegister(string value)
      {
         return s_FpRegisterMap.ContainsKey(value);
      }

      private static readonly Dictionary<string, int> s_RegisterMap;
      private static readonly Dictionary<string, int> s_FpRegisterMap;
   }
}
