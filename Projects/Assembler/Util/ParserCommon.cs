using System;
using System.Collections.Generic;
using System.Linq;

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
        /// Determines if a token declares a null or non-null terminated string.
        /// </summary>
        /// <param name="token">The string token to examine.</param>
        /// <returns>True if the token declares a string type.</returns>
        public static bool IsStringDeclaration(string token)
        {
            return token == ".ascii" || token == ".asciiz";
        }

        /// <summary>
        /// Determines if a token declares a variable space allocation
        /// </summary>
        /// <param name="token">The string token to examine.</param>
        /// <returns>True if the token declares a space allocation.</returns>
        public static bool IsSpaceDeclaration(string token)
        {
            return token == ".space";
        }

        /// <summary>
        /// Fetches a string that follows a string declaration.
        /// </summary>
        /// <param name="assemblyLine">The line of assembly to analyze.</param>
        /// <returns>The string that follows a string declaration.</returns>
        public static string GetStringData(string assemblyLine)
        {
            if (!assemblyLine.Contains("\""))
            {
                throw new ArgumentException("No string found in call to GetStringData.");
            }

            int firstIdx = assemblyLine.IndexOf('\"') + 1;
            int lastIdx = assemblyLine.LastIndexOf('\"');
            if (lastIdx - firstIdx <= 0)
            {
                throw new ArgumentException("Invalid or malformed string in call to GetStringData");
            }

            return assemblyLine.Substring(firstIdx, lastIdx - firstIdx);
        }

        /// <summary>
        /// Determines the size, in bytes, of a .ascii/.asciiz string, or a .space directive.
        /// </summary>
        /// <param name="dataTypeToken">The token describing the data type (e.g. ".ascii").</param>
        /// <param name="str">The token that immediately follows the data type, to examine.</param>
        /// <returns>The size of the string, or the size of the string + 1 if the data type is .asciiz (to account
        /// for the implicit 0 byte at the end).</returns>
        public static int DetermineNonTrivialDataLength(string dataTypeToken, string str)
        {
            int dataSize = 0;
            // strip the beginning and ending quotations.

            if (dataTypeToken == ".ascii")
            {
                dataSize = str.Length;
            }
            else if (dataTypeToken == ".asciiz")
            {
                dataSize = str.Length + 1;
            }
            else if (dataTypeToken == ".space")
            {
                // if this is a .space directive, then the token immediately following
                // should be how much space to reserve.
                dataSize = int.Parse(str);
            }
            else
            {
                throw new ArgumentException("Attempted to find string length of unknown data type " + dataTypeToken);
            }

            return dataSize;
        }

        /// <summary>
        /// Determines the size of a given data type.
        /// </summary>
        /// <param name="line">The line the symbol occured on. For error handling usage.</param>
        /// <param name="token">The declaration containing the type.</param>
        /// <returns>The size of the type, in bytes.</returns>
        public static int DetermineTrivialDataSize(string token)
        {
            int dataSize = 0;
            if (!s_DataTypeDictionary.TryGetValue(token, out dataSize))
            {
                throw new ArgumentException("Could not find data size with data type " + token);
            }

            return dataSize;
        }

        /// <summary>
        /// Returns true if the token is declaring a data type.
        /// </summary>
        /// <param name="token">The token to examine.</param>
        /// <returns>True if the token is a data declaration token, false otherwise.</returns>
        public static bool IsDataDeclaration(string token)
        {
            return IsTrivialDataType(token) ||
                    token == ".ascii" ||
                    token == ".asciiz" ||
                    token == ".space";
        }

        /// <summary>
        /// Gets an array of tokens, where each token is trimmed on the left/right for any whitespace.
        /// </summary>
        /// <param name="tokenizedLine">The original tokenized line (split by spaces).</param>
        /// <returns>A list of tokens with no whitespace on the right/left.</returns>
        public static IEnumerable<string> GetTrimmedTokenArray(string[] tokenizedLine)
        {
            IEnumerable<string> trimmedTokens = tokenizedLine.Apply((str) => str.Trim());
            var finalList = new List<string>();

            // remove any empty entries from the list.
            foreach (string token in trimmedTokens)
            {
                if (!string.IsNullOrEmpty(token))
                {
                    finalList.Add(token);
                }
            }

            return finalList;
        }

        /// <summary>
        /// Generates a substring of a line until the first comment.
        /// </summary>
        /// <param name="line">The line to examine and sanitize.</param>
        /// <returns>A string that contains no comments inline.</returns>
        public static string GetSanitizedString(string line)
        {
            string ret = string.Empty;
            // first, detect if our line contains a comment.
            if (line.Contains("#"))
            {
                // now get the index of the comment token.
                // we need to ensure it doesn't fall within a user string.
                if (line.Contains("\""))
                {
                    int commentIdx = line.IndexOf('#');
                    int firstQuoteIdx = line.IndexOf('\"');
                    int secondQuoteIdx = line.LastIndexOf('\"');

                    // check for any comments after the second quote.
                    if (firstQuoteIdx < commentIdx && commentIdx < secondQuoteIdx)
                    {
                        int secondCommentIdx = line.IndexOf('#', secondQuoteIdx);
                        if (secondCommentIdx > 0)
                        {
                            ret = line.Substring(0, secondCommentIdx);
                        }

                        // no comment found after the second quote, just use the whole line.
                        else
                        {
                            ret = line;
                        }
                    }

                    // the comment appears before or after the quotes; in which case,
                    // comment out the string, or grab everything since there's not a comment token that needs trimmed.
                    else
                    {
                        ret = line.Substring(0, line.IndexOf('#'));
                    }
                }
                else
                {
                    // otherwise, there's no string. just substring up to the comment token.
                    ret = line.Substring(0, line.IndexOf('#'));
                }
            }
            else
            {
                ret = line;
            }

            return ret;
        }

        /// <summary>
        /// Gets the size of an array or single element declaration.
        /// </summary>
        /// <param name="fullLine">The full line of assembly.</param>
        /// <param name="sizeDeclaration">The token specifying the data size (e.g. .byte, .word, etc.)</param>
        /// <returns>An integer representing how many elements to store contiguously starting at the base address of the line.</returns>
        public static int GetArraySize(string fullLine, string sizeDeclaration)
        {
            // find the token directly after the size directive
            int substrBeginIdx = fullLine.IndexOf(sizeDeclaration) + sizeDeclaration.Length;
            string arguments = fullLine.Substring(substrBeginIdx);

            // split by commas.
            string[] tokenizedArgs = arguments.Split(new[] { ',' });
            tokenizedArgs = tokenizedArgs.Apply((str) => str.Trim()).ToArray();

            // the array should at least have one element.
            int arrSize = 0;
            foreach (string token in tokenizedArgs)
            {
                // if it contains a ':' character, then this itself is an array of the initialized token.
                if (token.Contains(':'))
                {
                    string[] subtokens = token.Split(new[] { ':' }).Apply((str) => str.Trim()).ToArray();
                    if (subtokens.Length == 2)
                    {
                        arrSize += int.Parse(subtokens[1]);
                    }
                    else
                    {
                        throw new ArgumentException("Expected size parameter after ':' token.");
                    }
                }
                else
                {
                    ++arrSize;
                }
            }

            if (arrSize < 1)
            {
                throw new ArgumentException("Expected at least one value for declaration.");
            }

            return arrSize;
        }

        /// <summary>
        /// Handles processing of any preprocessor directive in the line, if any.
        /// </summary>
        /// <param name="trimmedLine">The line with leading/trailing whitespace removed.</param>
        /// <param name="lineNum">The current line number.</param>
        /// <param name="segType">The current segment type.</param>
        /// <param name="currAlignment">The current boundary alignment.</param>
        /// <returns>A structure representing the new alignment (if any), the new segment type (if any), and a boolean representing
        /// if any preprocessor directive was parsed at all</returns>
        public static LineParseResults HandlePreprocessorDeclarations(string trimmedLine, int lineNum, SegmentType segType, int currAlignment)
        {
            // tokenize the line;
            string[] tokens = trimmedLine.Split(' ');
            int alignment = currAlignment;
            SegmentType newSegType = segType;
            bool isDirective = false;

            // if we see something that's not a directive, and we're not in a valid
            // segment type, flag an exception.
            if (!SegmentTypeHelper.IsSegmentDeclarationToken(tokens[0]) &&
                !IsAlignmentDeclaration(tokens[0]) &&
                segType == SegmentType.Invalid)
            {
                throw new AssemblyException(lineNum, "Unexpected line \"" + trimmedLine + "\" found in non-segmented area.");
            }

            // otherwise, if we're looking for a segment definition, and we find one,
            // then set the current segment type to the segment type.
            else if (SegmentTypeHelper.IsSegmentDeclarationToken(tokens[0]))
            {
                newSegType = SegmentTypeHelper.GetSegmentType(tokens[0]);
                isDirective = true;
            }

            // if this segment is declaring a new alignment, get that and return to caller.
            else if (IsAlignmentDeclaration(tokens[0]))
            {
                if (segType == SegmentType.Text)
                {
                    throw new AssemblyException(lineNum, ".align declaration is forbidden in .text segment.");
                }

                // we'd expect there to be multiple tokens.
                if (tokens.Length > 1)
                {
                    // get the next token, and try to convert it to an alignment parameter.
                    int tokenizedAlign = 0;
                    if (int.TryParse(tokens[1], out tokenizedAlign))
                    {
                        alignment = ParserCommon.GetNewAlignment(tokenizedAlign);
                        isDirective = true;
                    }
                    else
                    {
                        throw new AssemblyException(lineNum, "Expected integer token after .align declaration.");
                    }
                }
                else
                {
                    throw new AssemblyException(lineNum, "Expected integer token after .align declaration.");
                }
            }

            return new LineParseResults(isDirective, alignment, newSegType);
        }

        private static readonly Dictionary<string, int> s_DataTypeDictionary;
    }
}
