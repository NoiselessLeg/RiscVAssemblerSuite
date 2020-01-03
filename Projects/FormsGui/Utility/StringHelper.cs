using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.FormsGui.Utility
{
   public static class StringHelper
   {
      /// <summary>
      /// Removes all instances of a specified character from a string.
      /// </summary>
      /// <param name="str">The string to perform the operation on.</param>
      /// <param name="c">The character to be removed from the string.</param>
      /// <returns>A new string value with any instance of the specified character removed.</returns>
      public static string RemoveInstancesOf(this string str, char c)
      {
         string retVal = str;

         int nextIdxToRemove = retVal.IndexOf(c);
         while (nextIdxToRemove >= 0)
         {
            int nextValidCharIdx = nextIdxToRemove + 1;
            retVal = retVal.Substring(0, nextIdxToRemove) + retVal.Substring(nextValidCharIdx, retVal.Length - nextValidCharIdx);
            nextIdxToRemove = retVal.IndexOf(c);
         }

         return retVal;
      }


      /// <summary>
      /// Removes all instances of a specified substring from a string.
      /// </summary>
      /// <param name="str">The string to perform the operation on.</param>
      /// <param name="removeStr">The substring to be removed from the string.</param>
      /// <returns>A new string value with any instance of the specified substring removed.</returns>
      public static string RemoveInstancesOf(this string str, string removeStr)
      {
         string retVal = str;

         int nextIdxToRemove = retVal.IndexOf(removeStr);
         while (nextIdxToRemove >= 0)
         {
            int nextValidCharIdx = nextIdxToRemove + 1;
            retVal = retVal.Substring(0, nextIdxToRemove) + retVal.Substring(nextValidCharIdx, retVal.Length - nextValidCharIdx);
            nextIdxToRemove = retVal.IndexOf(removeStr);
         }

         return retVal;
      }
   }
}
