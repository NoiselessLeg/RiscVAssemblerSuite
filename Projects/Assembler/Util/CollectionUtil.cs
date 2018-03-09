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
    }
}
