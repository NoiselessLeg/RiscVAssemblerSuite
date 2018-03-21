using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Output.ObjFileComponents
{
    class AsciizDataSegmentElement : AsciiDataSegmentElement
    {
        /// <summary>
        /// Creates an instance of the data element with the provided value.
        /// </summary>
        /// <param name="elem">The value of the element to store in the object file.</param>
        public AsciizDataSegmentElement(string str):
            base(str)
        {
        }

        /// <summary>
        /// Writes the bitwise representation of this object to the Stream.
        /// </summary>
        /// <param name="outputStream">The output Stream object to write to.</param>
        public override void WriteDataToFile(Stream outputStream)
        {
            // write the string, then null terminate it.
            base.WriteDataToFile(outputStream);
            outputStream.Write(new[] { (byte) 0 }, 0, 1);
        }
    }
}
