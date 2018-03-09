

namespace Assembler.Util
{
    static class ParserCommon
    {
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
    }
}
