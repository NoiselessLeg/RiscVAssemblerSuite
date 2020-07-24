using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Simulation
{
   static class FloatUtils
   {
      /// <summary>
      /// Determines if two floating point values are bitwise equivalent.
      /// </summary>
      /// <param name="lhs">The left hand floating point value to compare.</param>
      /// <param name="rhs">The right hand floating point value to compare.</param>
      /// <returns>True if the bits of both values are equivalent, otherwise returns false.</returns>
      public static bool BinaryEquals(this float lhs, float rhs)
      {
         byte[] val1Bytes = BitConverter.GetBytes(lhs);
         byte[] val2Bytes = BitConverter.GetBytes(rhs);

         return Enumerable.SequenceEqual(val1Bytes, val2Bytes);
      }
   }
}
