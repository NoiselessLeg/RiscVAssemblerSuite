namespace Assembler.Output.OutputWriters
{
    /// <summary>
    /// Defines a class that generates a type of object file.
    /// </summary>
    interface IObjectFileWriter
    {
        /// <summary>
        /// Outputs all data in the BasicObjectFile to the specified format.
        /// </summary>
        /// <param name="fileName">The file path to generate the output at.</param>
        /// <param name="file">The data that will be written to the file.</param>
        void WriteObjectFile(string fileName, BasicObjectFile file);
    }
}
