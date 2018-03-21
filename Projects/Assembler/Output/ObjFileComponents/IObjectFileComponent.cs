using System.IO;

namespace Assembler.Output.ObjFileComponents
{
    /// <summary>
    /// Defines a class that represents a sub-object within an abstract .obj file.
    /// </summary>
    interface IObjectFileComponent
    {
        /// <summary>
        /// Writes the bitwise representation of this object to the Stream.
        /// </summary>
        /// <param name="outputStream">The output Stream object to write to.</param>
        void WriteDataToFile(Stream outputStream);
    }
}
