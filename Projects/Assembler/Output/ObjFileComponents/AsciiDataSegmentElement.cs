using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Output.ObjFileComponents
{
    class AsciiDataSegmentElement : IObjectFileComponent
    {
        /// <summary>
        /// Creates an instance of the data element with the provided value.
        /// </summary>
        /// <param name="elem">The value of the element to store in the object file.</param>
        public AsciiDataSegmentElement(string str)
        {
            m_Elem = str;
        }

        /// <summary>
        /// Gets the size of the object file element, in bytes.
        /// </summary>
        public virtual int Size { get { return m_Elem.Length; } }

        /// <summary>
        /// Writes the bitwise representation of this object to the Stream.
        /// </summary>
        /// <param name="outputStream">The output Stream object to write to.</param>
        public virtual void WriteDataToFile(Stream outputStream)
        {
            outputStream.Write(Encoding.ASCII.GetBytes(m_Elem), 0, Encoding.ASCII.GetByteCount(m_Elem));
        }

        private readonly string m_Elem;
    }
}
