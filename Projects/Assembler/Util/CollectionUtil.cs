using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Util
{
    static class CollectionUtil
    {
        /// <summary>
        /// Returns the first element in the IEnumerable that meets the criteria specified by searchFunc.
        /// </summary>
        /// <typeparam name="T">The type of IEnumerable.</typeparam>
        /// <param name="enumerable">The collection to search through.</param>
        /// <param name="searchFunc">The function used to search through the enumerable.</param>
        /// <returns>The first element in the IEnumerable that matches the provided criteria, or the default value for that element type.</returns>
        public static T Find<T>(this IEnumerable<T> enumerable, Func<T, bool> searchFunc)
        {
            foreach (T val in enumerable)
            {
                if (searchFunc(val))
                {
                    return val;
                }
            }

            return default(T);
        }

        /// <summary>
        /// Applies a function across all elements of the enumerable.
        /// </summary>
        /// <typeparam name="T">The type of elements stored in this enumerable.</typeparam>
        /// <param name="enumerable">The IEnumerable object to operate on.</param>
        /// <param name="func">The function to apply to each element. Must return that type of element.</param>
        /// <returns>An IEnumerable instance with the function applied to each element of the original IEnumerable.</returns>
        public static IEnumerable<T> Apply<T>(this IEnumerable<T> enumerable, Func<T, T> func)
        {
            var fixedElems = new List<T>();
            foreach (T elem in enumerable)
            {
                fixedElems.Add(func(elem));
            }

            return fixedElems;
        }
    }
}
