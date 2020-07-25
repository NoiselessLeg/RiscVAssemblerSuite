using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Common
{
   public static class ArrayUtils
   {
      /// <summary>
      /// Returns a sub-array starting at a given index.
      /// </summary>
      /// <typeparam name="T">The type of array to operate on.</typeparam>
      /// <param name="data">The array to slice.</param>
      /// <param name="startingIdx">The index to where the subarray will be created from.</param>
      /// <param name="length">The length of the sub-array.</param>
      /// <returns>The slice of the array starting at the given index.</returns>
      public static T[] SubArray<T>(this T[] data, int startingIdx, int length)
      {
         T[] ret = new T[length];
         Array.Copy(data, startingIdx, ret, 0, length);
         return ret;
      }
   }
}
