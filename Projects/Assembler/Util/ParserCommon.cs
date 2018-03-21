using System.Collections.Generic;

namespace Assembler.Util
{
    static class ParserCommon
    {
        static ParserCommon()
        {
            s_DataTypeDictionary = new Dictionary<string, int>()
            {
                // NOTE: I have no idea what these actually are, yet.
                // these can and most likely will change.
                { ".byte", 1 },
                { ".half", 2 },
                { ".word", 4 },
                { ".dword", 8 }
            };
        }

        /// <summary>
        /// Returns if the line is an assembly comment.
        /// </summary>
        /// <param name="line">The line to examine.</param>
        /// <returns>True if the line is commented out.</returns>
        public static bool IsCommentedLine(string line)
        {
            return line.StartsWith("#");
        }

        /// <summary>
        /// Determines if a line of an assembly program contains a label.
        /// </summary>
        /// <param name="line">The line to examine.</param>
        /// <returns>True if a label name is in the line; returns false otherwise.</returns>
        public static bool ContainsLabel(string line)
        {
            return line.Contains(":");
        }

        /// <summary>
        /// Extracts the name of a label from a line.
        /// </summary>
        /// <param name="line">The line to examine.</param>
        /// <returns>The name of a label.</returns>
        public static string ExtractLabel(string line)
        {
            string[] tokens = line.Split(':');
            return tokens[0];
        }

        /// <summary>
        /// Determines if a line is an alignment declaration.
        /// </summary>
        /// <param name="token">The token to examine.</param>
        /// <returns>True if the token is an alignment declaration.</returns>
        public static bool IsAlignmentDeclaration(string token)
        {
            return token == ".align";
        }

        /// <summary>
        /// From an alignment declaration, gets the new desired alignment.
        /// </summary>
        /// <param name="tokenizedAlignment">The user-specified alignment parameter.</param>
        /// <returns>The alignment to use until the next specified alignment parameter.</returns>
        public static int GetNewAlignment(int tokenizedAlignment)
        {
            return 1 << tokenizedAlignment;
        }

        /// <summary>
        /// Gets the number of bytes needed to pad to the next alignment boundary.
        /// </summary>
        /// <param name="byteOffset">How many bytes of the current alignment were used.</param>
        /// <param name="currAlignment">The current alignment size, in bytes.</param>
        /// <returns>How many padding bytes are needed to reach the next byte alignment boundary.</returns>
        public static int GetNumPaddingBytes(int byteOffset, int currAlignment)
        {
            return (-byteOffset & (currAlignment - 1));
        }

        /// <summary>
        /// Determines if a data size is one of the basic types
        /// (e.g. .word, .byte). This returns false for .ascii/.asciiz types
        /// as their sizes are dependent on string length.
        /// </summary>
        /// <param name="token">The token to examine.</param>
        /// <returns>If the data size is trivial, this returns true. Otherwise returns false.</returns>
        public static bool IsTrivialDataType(string token)
        {
            return s_DataTypeDictionary.ContainsKey(token);
        }

        /// <summary>
        /// Determines the size, in bytes, of a .ascii/.asciiz string, or a .space directive.
        /// </summary>
        /// <param name="line">The line number the data type is found on.</param>
        /// <param name="dataTypeToken">The token describing the data type (e.g. ".ascii").</param>
        /// <param name="str">The token that immediately follows the data type, to examine.</param>
        /// <returns>The size of the string, or the size of the string + 1 if the data type is .asciiz (to account
        /// for the implicit 0 byte at the end).</returns>
        public static int DetermineNonTrivialDataLength(int incrementingLineNum, string dataTypeToken, string str)
        {
            int dataSize = 0;
            // strip the beginning and ending quotations.
            string fixedString = str.Substring(1, str.LastIndexOf('\"') - 1);

            if (dataTypeToken == ".ascii")
            {
                dataSize = fixedString.Length;
            }
            else if (dataTypeToken == ".asciiz")
            {
                dataSize = fixedString.Length + 1;
            }
            else if (dataTypeToken == ".space")
            {
                // if this is a .space directive, then the token immediately following
                // should be how much space to reserve.
                dataSize = int.Parse(str);
            }
            else
            {
                throw new AssemblyException(incrementingLineNum, "Attempted to find string length of unknown data type " + dataTypeToken);
            }

            return dataSize;
        }

        /// <summary>
        /// Determines the size of a given data type.
        /// </summary>
        /// <param name="line">The line the symbol occured on. For error handling usage.</param>
        /// <param name="token">The declaration containing the type.</param>
        /// <returns>The size of the type, in bytes.</returns>
        public static int DetermineTrivialDataSize(int line, string token)
        {
            int dataSize = 0;
            if (!s_DataTypeDictionary.TryGetValue(token, out dataSize))
            {
                throw new AssemblyException(line, "Could not find data size with data type " + token);
            }

            return dataSize;
        }

        private static readonly Dictionary<string, int> s_DataTypeDictionary;
    }
}
