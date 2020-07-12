using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Util
{
   public enum RoundingModes
   {
      Invalid = -1,
      RoundToNearestEvenMode = 0,
      RoundTowardsZero = 1,
      RoundDown = 2,
      RoundUp = 3,
      RoundToNearestMaxMagnitudeMode = 4,
      Dynamic = 7
   }

   public class FloatingPointUtils
   {
      public static RoundingModes ParseRoundingModeMnemonic(string mnemonic)
      {
         var retMode = RoundingModes.Invalid;
         string loweredVal = mnemonic.ToLower();

         if (loweredVal == "rne")
         {
            retMode = RoundingModes.RoundToNearestEvenMode;
         }
         else if (loweredVal == "rtz")
         {
            retMode = RoundingModes.RoundTowardsZero;
         }
         else if (loweredVal == "rdn")
         {
            retMode = RoundingModes.RoundDown;
         }
         else if (loweredVal == "rup")
         {
            retMode = RoundingModes.RoundUp;
         }
         else if (loweredVal == "rmm")
         {
            retMode = RoundingModes.RoundToNearestMaxMagnitudeMode;
         }
         else if (loweredVal == "dyn")
         {
            retMode = RoundingModes.Dynamic;
         }

         return retMode;
      }
   }
}
