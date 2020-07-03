using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Common
{
   public static class StringBuilderUtil
   {
      /// <summary>
      /// Removes the last-appended character from this StringBuilder instance.
      /// If no characters are in the builder, this will throw an InvalidOperationException.
      /// </summary>
      /// <param name="sb"></param>
      public static void PopCharacter(this StringBuilder sb)
      {
         if (sb.Length == 0)
         {
            throw new InvalidOperationException("No characters available to pop.");
         }

         --sb.Length;
      }
   }
}
