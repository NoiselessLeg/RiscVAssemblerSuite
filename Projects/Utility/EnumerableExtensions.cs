using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Common
{
   public static class EnumerableExtensions
   {
      /// <summary>
      /// Projects a sequence of elements into a new form if they meet a selection criteria.
      /// </summary>
      /// <typeparam name="TValue">The type of elements stored in the original IEnumerable instance.</typeparam>
      /// <typeparam name="TReturn">The type parameter of the IEnumerable to be returned.</typeparam>
      /// <param name="enumerable">The enumerable to perform this operation on.</param>
      /// <param name="elemToSelect">Specifies the member of TValue to select and add to the returned IEnumerable.</param>
      /// <param name="selectionCriteria">The criteria used to determine if the member should be projected into the new IEnumerable.</param>
      /// <returns>An IEnumerable of projected elements that met the selection criteria.</returns>
      public static IEnumerable<TReturn> SelectIf<TValue, TReturn>(this IEnumerable<TValue> enumerable, 
                                                                   Func<TValue, TReturn> elemToSelect,
                                                                   Func<TValue, bool> selectionCriteria)
      {
         foreach (TValue elem in enumerable)
         {
            if (selectionCriteria(elem))
            {
               yield return elemToSelect(elem);
            }
         }
      }
   }
}
