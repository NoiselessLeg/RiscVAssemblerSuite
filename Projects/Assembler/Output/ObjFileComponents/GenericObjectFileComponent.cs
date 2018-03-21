

using System;
using System.IO;

// Note this file is auto-generated.
// Changes to the .cs file may be overwritten upon save.
// Make changes to the .tt file.


namespace Assembler.Output.ObjFileComponents
{
    /// <summary>
    /// Represents a Int16 as a data element in a .obj file.
    /// </summary>
    public class Int16DataElement : IObjectFileComponent
    {
        /// <summary>
        /// Creates an instance of the data element with the provided value.
        /// </summary>
        /// <param name="elem">The value of the element to store in the object file.</param>
        public Int16DataElement(Int16 elem)
        {
            m_Elem = elem;
        }

        /// <summary>
        /// Writes the bitwise representation of this object to the Stream.
        /// </summary>
        /// <param name="outputStream">The output Stream object to write to.</param>
        public void WriteDataToFile(Stream outputStream)
        {
            byte[] objBytes = ToByteArray(m_Elem);
            outputStream.Write(objBytes, 0, objBytes.Length);
        }

        /// <summary>
        /// Gets the provided Int16 as a byte array.
        /// </summary>
        /// <param name="param">The value to convert to bytes.</param>
        private static byte[] ToByteArray(Int16 param)
        {
            return BitConverter.GetBytes(param);
        }

        private readonly Int16 m_Elem;
    }

    /// <summary>
    /// Represents a UInt16 as a data element in a .obj file.
    /// </summary>
    public class UInt16DataElement : IObjectFileComponent
    {
        /// <summary>
        /// Creates an instance of the data element with the provided value.
        /// </summary>
        /// <param name="elem">The value of the element to store in the object file.</param>
        public UInt16DataElement(UInt16 elem)
        {
            m_Elem = elem;
        }

        /// <summary>
        /// Writes the bitwise representation of this object to the Stream.
        /// </summary>
        /// <param name="outputStream">The output Stream object to write to.</param>
        public void WriteDataToFile(Stream outputStream)
        {
            byte[] objBytes = ToByteArray(m_Elem);
            outputStream.Write(objBytes, 0, objBytes.Length);
        }

        /// <summary>
        /// Gets the provided UInt16 as a byte array.
        /// </summary>
        /// <param name="param">The value to convert to bytes.</param>
        private static byte[] ToByteArray(UInt16 param)
        {
            return BitConverter.GetBytes(param);
        }

        private readonly UInt16 m_Elem;
    }

    /// <summary>
    /// Represents a Int32 as a data element in a .obj file.
    /// </summary>
    public class Int32DataElement : IObjectFileComponent
    {
        /// <summary>
        /// Creates an instance of the data element with the provided value.
        /// </summary>
        /// <param name="elem">The value of the element to store in the object file.</param>
        public Int32DataElement(Int32 elem)
        {
            m_Elem = elem;
        }

        /// <summary>
        /// Writes the bitwise representation of this object to the Stream.
        /// </summary>
        /// <param name="outputStream">The output Stream object to write to.</param>
        public void WriteDataToFile(Stream outputStream)
        {
            byte[] objBytes = ToByteArray(m_Elem);
            outputStream.Write(objBytes, 0, objBytes.Length);
        }

        /// <summary>
        /// Gets the provided Int32 as a byte array.
        /// </summary>
        /// <param name="param">The value to convert to bytes.</param>
        private static byte[] ToByteArray(Int32 param)
        {
            return BitConverter.GetBytes(param);
        }

        private readonly Int32 m_Elem;
    }

    /// <summary>
    /// Represents a UInt32 as a data element in a .obj file.
    /// </summary>
    public class UInt32DataElement : IObjectFileComponent
    {
        /// <summary>
        /// Creates an instance of the data element with the provided value.
        /// </summary>
        /// <param name="elem">The value of the element to store in the object file.</param>
        public UInt32DataElement(UInt32 elem)
        {
            m_Elem = elem;
        }

        /// <summary>
        /// Writes the bitwise representation of this object to the Stream.
        /// </summary>
        /// <param name="outputStream">The output Stream object to write to.</param>
        public void WriteDataToFile(Stream outputStream)
        {
            byte[] objBytes = ToByteArray(m_Elem);
            outputStream.Write(objBytes, 0, objBytes.Length);
        }

        /// <summary>
        /// Gets the provided UInt32 as a byte array.
        /// </summary>
        /// <param name="param">The value to convert to bytes.</param>
        private static byte[] ToByteArray(UInt32 param)
        {
            return BitConverter.GetBytes(param);
        }

        private readonly UInt32 m_Elem;
    }

    /// <summary>
    /// Represents a Int64 as a data element in a .obj file.
    /// </summary>
    public class Int64DataElement : IObjectFileComponent
    {
        /// <summary>
        /// Creates an instance of the data element with the provided value.
        /// </summary>
        /// <param name="elem">The value of the element to store in the object file.</param>
        public Int64DataElement(Int64 elem)
        {
            m_Elem = elem;
        }

        /// <summary>
        /// Writes the bitwise representation of this object to the Stream.
        /// </summary>
        /// <param name="outputStream">The output Stream object to write to.</param>
        public void WriteDataToFile(Stream outputStream)
        {
            byte[] objBytes = ToByteArray(m_Elem);
            outputStream.Write(objBytes, 0, objBytes.Length);
        }

        /// <summary>
        /// Gets the provided Int64 as a byte array.
        /// </summary>
        /// <param name="param">The value to convert to bytes.</param>
        private static byte[] ToByteArray(Int64 param)
        {
            return BitConverter.GetBytes(param);
        }

        private readonly Int64 m_Elem;
    }

    /// <summary>
    /// Represents a UInt64 as a data element in a .obj file.
    /// </summary>
    public class UInt64DataElement : IObjectFileComponent
    {
        /// <summary>
        /// Creates an instance of the data element with the provided value.
        /// </summary>
        /// <param name="elem">The value of the element to store in the object file.</param>
        public UInt64DataElement(UInt64 elem)
        {
            m_Elem = elem;
        }

        /// <summary>
        /// Writes the bitwise representation of this object to the Stream.
        /// </summary>
        /// <param name="outputStream">The output Stream object to write to.</param>
        public void WriteDataToFile(Stream outputStream)
        {
            byte[] objBytes = ToByteArray(m_Elem);
            outputStream.Write(objBytes, 0, objBytes.Length);
        }

        /// <summary>
        /// Gets the provided UInt64 as a byte array.
        /// </summary>
        /// <param name="param">The value to convert to bytes.</param>
        private static byte[] ToByteArray(UInt64 param)
        {
            return BitConverter.GetBytes(param);
        }

        private readonly UInt64 m_Elem;
    }

}
