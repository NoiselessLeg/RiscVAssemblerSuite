using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Common
{
   public static class FloatExtensions
   {

      /// <summary>
      /// Tries to parse a 32-bit single precision float from a string. This method should handle hexadecimal values
      /// as well as normal values.
      /// </summary>
      /// <param name="value">The string value to parse.</param>
      /// <param name="result">The parsed float value, if the string was valid. If invalid, this
      /// will be the default float value value.</param>
      /// <returns>True if the conversion was successful; otherwise returns false.</returns>
      public static bool TryParseEx(string value, out float result)
      {
         bool canConvert = true;
         try
         {
            var converter = new SingleConverter();
            result = (float)converter.ConvertFromString(value);
         }
         catch (Exception)
         {
            result = default(float);
            canConvert = false;
         }

         return canConvert;
      }
   }
}
