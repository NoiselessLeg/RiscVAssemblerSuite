using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Common
{
    public static class IntExtensions
    {
        /// <summary>
        /// Tries to parse a 16-bit integer from a string. This method should handle hexadecimal values
        /// as well as normal values.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <param name="result">The parsed integer, if the string was valid. If invalid, this
        /// will be the default integer value.</param>
        /// <returns>True if the conversion was successful; otherwise returns false.</returns>
        public static bool TryParseEx(string value, out short result)
        {
            bool canConvert = true;
            try
            {
                var converter = new Int16Converter();
                result = (short)converter.ConvertFromString(value);
            }
            catch (Exception)
            {
                result = default(short);
                canConvert = false;
            }

            return canConvert;
        }

        /// <summary>
        /// Tries to parse a 32-bit integer from a string. This method should handle hexadecimal values
        /// as well as normal values.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <param name="result">The parsed integer, if the string was valid. If invalid, this
        /// will be the default integer value.</param>
        /// <returns>True if the conversion was successful; otherwise returns false.</returns>
        public static bool TryParseEx(string value, out int result)
        {
            bool canConvert = true;
            try
            {
                var converter = new Int32Converter();
                result = (int)converter.ConvertFromString(value);
            }
            catch (Exception)
            {
                result = default(int);
                canConvert = false;
            }

            return canConvert;
        }

        /// <summary>
        /// Tries to parse a 32-bit unsigned integer from a string. This method should handle hexadecimal values
        /// as well as normal values.
        /// </summary>
        /// <param name="value">The string value to parse.</param>
        /// <param name="result">The parsed integer, if the string was valid. If invalid, this
        /// will be the default integer value.</param>
        /// <returns>True if the conversion was successful; otherwise returns false.</returns>
        public static bool TryParseEx(string value, out uint result)
        {
            bool canConvert = true;
            try
            {
                var converter = new UInt32Converter();
                result = (uint)converter.ConvertFromString(value);
            }
            catch (Exception)
            {
                result = default(uint);
                canConvert = false;
            }

            return canConvert;
        }
    }
}
