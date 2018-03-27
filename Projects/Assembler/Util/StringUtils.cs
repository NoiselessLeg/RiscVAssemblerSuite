using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Util
{
    public static class StringUtils
    {
        /// <summary>
        /// Try to parse a literal character (i.e. a string formatted like 'A') as a byte value. If single quotes are not present, delegates
        /// to the default parser.
        /// </summary>
        /// <param name="str">The string to parse.</param>
        /// <returns>The equivalent Char value represented by the string.</returns>
        public static byte ParseCharacterLiteralAsByte(string str)
        {
            // see if the string is encased in single quotes.
            // if it is, see if we can parse anything in between them.
            byte ret = 0;
            if (str.Contains('\''))
            {
                char ch = '\0';
                if (char.TryParse(str.Substring(str.IndexOf('\''), str.LastIndexOf('\'') - str.IndexOf('\'')), out ch))
                {
                    ret = Convert.ToByte(ch);
                }
            }
            else
            {
                ret = byte.Parse(str);
            }

            return ret;
        }
        
        /// <summary>
         /// Try to parse a literal character (i.e. a string formatted like 'A'). If single quotes are not present, delegates
         /// to the default parser. Works like Parse; however, returns true/false instead of throwing an exception.
         /// </summary>
         /// <param name="str">The string to parse.</param>
         /// <param name="ch">The character to initialize with the parsed valueparam>
         /// <returns>True if the string could successfully be parsed as a character; otherwise returns false.</returns>
        public static bool TryParseCharacterLiteralAsByte(string str, out byte bt)
        {
            bool result = false;
            bt = 0;
            // see if the string is encased in single quotes.
            // if it is, see if we can parse anything in between them.
            if (str.Contains('\''))
            {
                char ch = '\0';

                string substr = str.Substring(str.IndexOf('\'') + 1, str.LastIndexOf('\'') - str.IndexOf('\'') - 1);
                if (char.TryParse(substr, out ch))
                {
                    bt = Convert.ToByte(ch);

                    result = true;
                }
            }
            else
            {
                result = byte.TryParse(str, out bt);
            }

            return result;
        }


        /// <summary>
        /// Parses a string as a generic value.
        /// </summary>
        /// <typeparam name="T">The type to parse the string as.</typeparam>
        /// <param name="str">The string to parse.</param>
        /// <returns>The parsed value from the string of type T.</returns>
        public static T Parse<T>(string str)
        {
            T retVal = default(T);
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter != null && converter.CanConvertFrom(typeof(string)))
                {
                    retVal = (T)converter.ConvertFromString(str);
                }
            }
            catch
            {
                retVal = default(T);
            }

            return retVal;
        }
    }
}
